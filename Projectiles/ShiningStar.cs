using System;
using SealTheHeavens.Items.Martial;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SealTheHeavens.Projectiles
{
    public class ShiningStar : MartialProj
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Eclipse Sun");
			//aiType = 495;
        }

        public override void SetDefaults()
        {
			Projectile.aiStyle = -1;
			Projectile.width = 34;
			Projectile.height = 26;
            Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 175;
        }

        float rotationInitial
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        float speed
        {
            get => Projectile.ai[1];
        }
        public override void AI()
        {
            Projectile.frameCounter++;
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.frameCounter == 1)
                rotationInitial = Projectile.rotation;
            Projectile.velocity = new Vector2(speed, 0).RotatedBy(rotationInitial) + new Vector2(0, (float)Math.Sin(Projectile.frameCounter/6f) * 2f).RotatedBy(rotationInitial);
        }
    }
}
