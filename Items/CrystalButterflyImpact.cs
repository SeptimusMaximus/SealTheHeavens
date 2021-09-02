using System;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SealTheHeavens;

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
			item.damage = 40;
			qi = 50; //uses 50 qi per use, variable in MartialItem
			item.crit = 2;
            item.width = 38;
            item.height = 38;
            item.useTime = 20;
            item.useAnimation = 20;
						item.noUseGraphic = true;
						item.noMelee = true;
            item.knockBack = 3.5f;
            item.value = 6900; //nice
            item.rare = 5;
            //item.UseSound = SoundID.Item19; //couldn't decide which sound to use so I left none
            item.shoot = mod.ProjectileType("ButterflyProj");
            item.shootSpeed = 9f;
            item.autoReuse = true;
			item.useStyle = 4;
		}

		private float dustRot = 0f; //epic variable used for epic dust effect, which I use for summoner sub-class evocator weapons
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			position.Y -= 20;
            if(player.direction == 1)position.X += 42;
			else position.X -= 42;

			for(int i = 0; i < 7; i++) {
			int dust = Dust.NewDust(position-new Vector2(1,1), 2, 2, 66, (float)((Math.Cos(dustRot) * 10f) * -1), (float)((Math.Sin(dustRot) * 10f) * -1), 50, new Color(125,25,75,200), 1.75f);
            Main.dust[dust].noGravity = true;
			dustRot += 0.9f;
			}
			dustRot = Main.rand.NextFloat(0f,0.9f);
            return true;
        }
	}
}
