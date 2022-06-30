using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace SealTheHeavens.NPCs.GrandSpiritBattlemage
{
	public class GrandSpiritBattlemage : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grand Spirit Battlemage");
			Main.npcFrameCount[NPC.type] = 6;
			NPCID.Sets.TrailingMode[NPC.type] = 7;
			NPCID.Sets.TrailCacheLength[NPC.type] = 16;
		}

		public override void SetDefaults()
		{
			NPC.width = 68;
			NPC.height = 74;
			NPC.damage = 25;
			NPC.lifeMax = 1280;
			NPC.defense = 20;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = Item.buyPrice(0, 2, 50, 0);
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
			NPC.boss = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			NPC.lavaImmune = true;
			Music = MusicID.Boss5;
			NPC.buffImmune[BuffID.Poisoned] = true;
		}

		const float RADIUS = 150, MIN_DIST_FROM_PLAYER = 64;
		Vector2 lastWarpPosition;
		void WarpAroundPlayer()
        {
			var player = Main.player[NPC.target];
			while (true)
			{
				Vector2 trial = player.Center + Main.rand.NextVector2Circular(RADIUS, RADIUS) - NPC.frame.Size() / 2;
				Vector2 fixedTrial = trial + (trial - player.Center).SafeNormalize(Vector2.UnitX) * MIN_DIST_FROM_PLAYER - Vector2.UnitY * (NPC.height / 2);
				Vector2 trialTile = new Vector2(fixedTrial.X / 16f, fixedTrial.Y / 16f);
				var tile = Main.tile[(int)trialTile.X, (int)trialTile.Y];
				if (!tile.Active() && player.DistanceSQ(fixedTrial) > 4096)
				{
					NPC.Teleport(fixedTrial , 1);
					lastWarpPosition = fixedTrial;
					break;
				}
			}
		}
        void WAPAwayFromLastPos(float minDist)
        {
            var player = Main.player[NPC.target];
            minDist = (float)Math.Pow(minDist, 2);
            while (true)
            {
                Vector2 trial = player.Center + Main.rand.NextVector2Circular(RADIUS, RADIUS) - NPC.frame.Size() / 2;
				Vector2 fixedTrial = trial + (trial - player.Center).SafeNormalize(Vector2.UnitX) * MIN_DIST_FROM_PLAYER - Vector2.UnitY * (NPC.height / 2);
				if ((fixedTrial - lastWarpPosition).LengthSquared() > minDist)
                {
					Vector2 trialTile = new Vector2(fixedTrial.X / 16f, fixedTrial.Y / 16f);
					var tile = Main.tile[(int)trialTile.X, (int)trialTile.Y];
					if (!tile.active() && player.DistanceSQ(fixedTrial) > 4096)
                    {
						NPC.Teleport(fixedTrial, 1);
                        lastWarpPosition = fixedTrial;
                        break;
                    }
                }
            }
        }
		const float DISTANCE_FROM_LAST_POS = 64;
		void ProcessMovement()
		{
			var player = Main.player[NPC.target];
			switch (state)
			{
				case 0:
					if (NPC.frameCounter == 0)
					{
						WarpAroundPlayer();
					}
					break;
				case 1:
					if (NPC.frameCounter % 60 == 0)
                    {
						WAPAwayFromLastPos(DISTANCE_FROM_LAST_POS);
                    }
					break;
			}
			NPC.velocity.Y = (float)Math.Sin(NPC.frameCounter/4f);
        }

        int state = 0;
		const float SPIRIT_FLARE_SPEED = 2.5f;
		public override void AI()
		{
			NPC.TargetClosest();
			ProcessMovement();
			switch (state)
            {
				case 0:
					if (NPC.frameCounter % 60 == 0)
                    {
						for (int i = 0; i < 4; i++)
                        {
							Projectile.NewProjectile(NPC.Center, Vector2.UnitX.RotatedBy(MathHelper.PiOver2 * i) * SPIRIT_FLARE_SPEED, ModContent.ProjectileType<SpiritFlare.SpiritFlare>(), 24, 6);
						}
                    }

					if (NPC.frameCounter++ >= 180)
                    {
						NPC.frameCounter = 0;
						state = 1;
                    }
					break;
				case 1:
					NPC.immortal = true;
					NPC.defense = 9999999;

					NPC.color = Color.Lerp(Color.White, Color.Black, (float)Math.Sin(NPC.frameCounter / 30));
					if (NPC.frameCounter % 120 == 0)
					{
						for (int i = 0; i < 4; i++)
						{
							Projectile.NewProjectile(NPC.Center, Vector2.UnitX.RotatedBy(MathHelper.PiOver2 * i) * SPIRIT_FLARE_SPEED - NPC.velocity, ModContent.ProjectileType<SpiritFlare.SpiritFlare>(), 24, 6);
						}
					}

					if (NPC.frameCounter++ >= 600)
                    {
                        NPC.immortal = false;

						NPC.defense = 20;
						NPC.frameCounter = 0;
						state = 0;
						NPC.color = Color.White;
                    }
					break;
            }
        }
	}
}
