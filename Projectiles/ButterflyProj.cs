using System;
using Terraria.Graphics.Shaders;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SealTheHeavens;

namespace SealTheHeavens.Projectiles
{
    public class ButterflyProj : MartialProj //custom projectile class
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Butterfly Impact"); //Genshin impact
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.timeLeft = 90;
            projectile.penetrate = 4;
			projectile.ownerHitCheck = true;//if the projectile should check if it's attacking through walls, if true, the projectile won't damage through walls unless the foe goes through them
        }
		public override void Kill(int timeLeft)
        {
            if(timeLeft > 10) {
				Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 27, 0.75f, 0.15f);
				for(int i = 0; i < 5; i++) {
					int dust = Dust.NewDust(projectile.Center-new Vector2(1,1), 2, 2, 227, 0f, 0f, 75, Color.Pink, 1.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.75f;
				}
			}
        }
		public override void AI()
        {
			projectile.velocity *= 1.0011f; //added this cus funny haha xd acceleration
			
			//these make the proj rotate acording to their speed
			projectile.spriteDirection = projectile.direction;
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
		}
		public override Color? GetAlpha(Color lightColor)
        {
            byte num4 = (byte)(projectile.timeLeft * 2.833f); //complex equation never seen to human kind, 2.833f corresponds to 255(max color value) divided by the projectile's timeLeft
            byte num5 = (byte)(100.0 * ((double)num4 / (double)byte.MaxValue));
            return new Color((int)num4, (int)num4, (int)num4, (int)num5);
        }
    }

}