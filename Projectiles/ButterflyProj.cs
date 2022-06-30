using System;
using Terraria.Graphics.Shaders;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using SealTheHeavens;
using Terraria.Audio;

namespace SealTheHeavens.Projectiles
{
    public class ButterflyProj : MartialProj //custom Projectile class
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Butterfly Impact"); //Genshin impact
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 90;
            Projectile.penetrate = 4;
			Projectile.ownerHitCheck = true;//if the Projectile should check if it's attacking through walls, if true, the Projectile won't damage through walls unless the foe goes through them
        }
		public override void Kill(int timeLeft)
        {
            if(timeLeft > 10) {
				SoundEngine.PlaySound(SoundID.Pixie/*, (int)Projectile.Center.X, (int)Projectile.Center.Y, 27, 0.75f, 0.15f*/);
				for(int i = 0; i < 5; i++) {
					int dust = Dust.NewDust(Projectile.Center-new Vector2(1,1), 2, 2, 227, 0f, 0f, 75, Color.Pink, 1.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.75f;
				}
			}
        }
		public override void AI()
        {
			Projectile.velocity *= 1.0011f; //added this cus funny haha xd acceleration
			
			//these make the proj rotate acording to their speed
			Projectile.spriteDirection = Projectile.direction;
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		}
		public override Color? GetAlpha(Color lightColor)
        {
            byte num4 = (byte)(Projectile.timeLeft * 2.833f); //complex equation never seen to human kind, 2.833f corresponds to 255(max color value) divided by the Projectile's timeLeft
            byte num5 = (byte)(100.0 * ((double)num4 / (double)byte.MaxValue));
            return new Color((int)num4, (int)num4, (int)num4, (int)num5);
        }
    }

}