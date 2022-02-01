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
			Main.npcFrameCount[npc.type] = 6;
			NPCID.Sets.TrailingMode[npc.type] = 7;
			NPCID.Sets.TrailCacheLength[npc.type] = 16;
		}

		public override void SetDefaults()
		{
			npc.width = 68;
			npc.height = 74;
			npc.damage = 25;
			npc.lifeMax = 1280;
			npc.defense = 20;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = Item.buyPrice(0, 2, 50, 0);
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
			npc.noGravity = true;
			npc.boss = true;
			npc.noTileCollide = true;
			npc.netAlways = true;
			npc.lavaImmune = true;
			music = MusicID.Boss5;
			npc.buffImmune[BuffID.Poisoned] = true;
		}

		const float RADIUS = 150, MIN_DIST_FROM_PLAYER = 64;
		Vector2 lastWarpPosition;
		void WarpAroundPlayer()
        {
			var player = Main.player[npc.target];
			while (true)
			{
				Vector2 trial = player.Center + Main.rand.NextVector2Circular(RADIUS, RADIUS) - npc.frame.Size() / 2;
				Vector2 fixedTrial = trial + (trial - player.Center).SafeNormalize(Vector2.UnitX) * MIN_DIST_FROM_PLAYER - Vector2.UnitY * (npc.height / 2);
				Vector2 trialTile = new Vector2(fixedTrial.X / 16f, fixedTrial.Y / 16f);
				var tile = Main.tile[(int)trialTile.X, (int)trialTile.Y];
				if (!tile.active() && player.DistanceSQ(fixedTrial) > 4096)
				{
					npc.Teleport(fixedTrial , 1);
					lastWarpPosition = fixedTrial;
					break;
				}
			}
		}
        void WAPAwayFromLastPos(float minDist)
        {
            var player = Main.player[npc.target];
            minDist = (float)Math.Pow(minDist, 2);
            while (true)
            {
                Vector2 trial = player.Center + Main.rand.NextVector2Circular(RADIUS, RADIUS) - npc.frame.Size() / 2;
				Vector2 fixedTrial = trial + (trial - player.Center).SafeNormalize(Vector2.UnitX) * MIN_DIST_FROM_PLAYER - Vector2.UnitY * (npc.height / 2);
				if ((fixedTrial - lastWarpPosition).LengthSquared() > minDist)
                {
					Vector2 trialTile = new Vector2(fixedTrial.X / 16f, fixedTrial.Y / 16f);
					var tile = Main.tile[(int)trialTile.X, (int)trialTile.Y];
					if (!tile.active() && player.DistanceSQ(fixedTrial) > 4096)
                    {
						npc.Teleport(fixedTrial, 1);
                        lastWarpPosition = fixedTrial;
                        break;
                    }
                }
            }
        }
		const float DISTANCE_FROM_LAST_POS = 64;
		void ProcessMovement()
		{
			var player = Main.player[npc.target];
			switch (state)
			{
				case 0:
					if (npc.frameCounter == 0)
					{
						WarpAroundPlayer();
					}
					break;
				case 1:
					if (npc.frameCounter % 60 == 0)
                    {
						WAPAwayFromLastPos(DISTANCE_FROM_LAST_POS);
                    }
					break;
			}
			npc.velocity.Y = (float)Math.Sin(npc.frameCounter/4f);
        }

        int state = 0;
		const float SPIRIT_FLARE_SPEED = 2.5f;
		public override void AI()
		{
			npc.TargetClosest();
			ProcessMovement();
			switch (state)
            {
				case 0:
					if (npc.frameCounter % 60 == 0)
                    {
						for (int i = 0; i < 4; i++)
                        {
							Projectile.NewProjectile(npc.Center, Vector2.UnitX.RotatedBy(MathHelper.PiOver2 * i) * SPIRIT_FLARE_SPEED, ModContent.ProjectileType<SpiritFlare.SpiritFlare>(), 24, 6);
						}
                    }

					if (npc.frameCounter++ >= 180)
                    {
						npc.frameCounter = 0;
						state = 1;
                    }
					break;
				case 1:
					npc.immortal = true;
					npc.defense = 9999999;

					npc.color = Color.Lerp(Color.White, Color.Black, (float)Math.Sin(npc.frameCounter / 30));
					if (npc.frameCounter % 120 == 0)
					{
						for (int i = 0; i < 4; i++)
						{
							Projectile.NewProjectile(npc.Center, Vector2.UnitX.RotatedBy(MathHelper.PiOver2 * i) * SPIRIT_FLARE_SPEED - npc.velocity, ModContent.ProjectileType<SpiritFlare.SpiritFlare>(), 24, 6);
						}
					}

					if (npc.frameCounter++ >= 600)
                    {
                        npc.immortal = false;

						npc.defense = 20;
						npc.frameCounter = 0;
						state = 0;
						npc.color = Color.White;
                    }
					break;
            }
        }
	}
}
