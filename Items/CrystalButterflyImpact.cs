using System;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using SealTheHeavens;
using SealTheHeavens.Projectiles;

namespace SealTheHeavens.Items
{
	public class CrystalButterflyImpact : MartialItem //custom item class
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Butterfly Impact");
            Tooltip.SetDefault("Shoots a crystal shard that fades out");
        }

		public override void SafeSetDefaults() {
			Item.damage = 40;
			qi = 50; //uses 50 qi per use, variable in MartialItem
			Item.crit = 2;
            Item.width = 46;
            Item.height = 36;
            Item.useTime = 20;
            Item.useAnimation = 20;
			Item.noUseGraphic = true;
			Item.noMelee = true;
            Item.knockBack = 3.5f;
            Item.value = 6900; //nice
            Item.rare = 5;
            Item.UseSound = SoundID.NPCDeath7;
            Item.shoot = ModContent.ProjectileType<ButterflyProj>();
            Item.shootSpeed = 9f;
            Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.HoldUp;
		}

		public static float dustRot = 0f; //epic variable used for epic dust effect, which I use for summoner sub-class evocator weapons
		public static bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			position.Y -= 20;
            if(player.direction == 1)position.X += 42;
			else position.X -= 42;

			for(int i = 0; i < 7; i++)
			{
			int dust = Dust.NewDust(position-new Vector2(1, 1), 2, 2, DustID.RainbowTorch, (float)((Math.Cos(dustRot) * 10f) * -1), (float)((Math.Sin(dustRot) * 10f) * -1), 50, new Color(125,25,75,200), 1.75f);
            Main.dust[dust].noGravity = true;
			dustRot += 0.9f;
			}
			dustRot = Main.rand.NextFloat(0f, 0.9f);
            return true;
        }
	}
}
