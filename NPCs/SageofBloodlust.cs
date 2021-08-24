using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SealTheHeavens.Items;
using SealTheHeavens.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace SealTheHeavens.NPCs
{
	public class SageofBloodlust : ModNPC
	{
		private int frame = 0;
		private double counting;

		private static int hellLayer => Main.maxTilesY - 200;

		private const int sphereRadius = 300;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sage of Bloodlust");
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.aiStyle = 22;
			npc.lifeMax = 1500;
			npc.damage = 20;
			npc.defense = 20;
			npc.knockBackResist = 1f;
			npc.width = 42;
			npc.height = 60;
			npc.aiStyle = -1;
			npc.value = Item.buyPrice(0, 2, 50, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit49;
			npc.DeathSound = SoundID.NPCDeath59;
		}
		public override void FindFrame(int FrameHeight)
		{

			if (frame == 0)
			{

				counting += 1.0;
				if (counting < 8.0)
				{

					npc.frame.Y = FrameHeight * 0;
				}
				else if (counting < 16)
				{
					npc.frame.Y = FrameHeight * 1;
				}
				else if (counting < 24)
				{
					npc.frame.Y = FrameHeight * 2;
				}
				else
				{
					counting = 0.0;
				}
			}
		}

		public float Timer {
			get => npc.ai[0];
			set => npc.ai[0] = value;
		}

		public override void AI() {
				// Other code...
				Timer++;
				if (Timer > 120) {
					float Speed = 25f;
	                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
	                int damage = Main.expertMode ? 10 : 20;
	                int type = mod.ProjectileType("ZodiacCultistProj");
	                Main.PlaySound(2, (int) npc.position.X, (int) npc.position.Y, 9);
	                float rotation = 0f;
	                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y,(float)((Math.Cos(rotation) * Speed)*-1),(float)((Math.Sin(rotation) * Speed)*-1), type, damage, 0f, 0);
					rotation = 0.5f;
	                num54 = Projectile.NewProjectile(vector8.X, vector8.Y,(float)((Math.Cos(rotation) * Speed)*-1),(float)((Math.Sin(rotation) * Speed)*-1), type, damage, 0f, 0);
					rotation = 5.7f;
	                num54 = Projectile.NewProjectile(vector8.X, vector8.Y,(float)((Math.Cos(rotation) * Speed)*-1),(float)((Math.Sin(rotation) * Speed)*-1), type, damage, 0f, 0);
					Timer = 0;
				}
				// Other code...
			}

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ModContent.ItemType<StarfleetScrap>(), 2 + Main.rand.Next(3));
		}
	}
}
