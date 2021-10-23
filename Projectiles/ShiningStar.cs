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
			projectile.aiStyle = -1;
			projectile.width = 34;
			projectile.height = 26;
            projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 175;
        }

        float rotationInitial
        {
            get => projectile.ai[0];
            set => projectile.ai[0] = value;
        }
        float speed
        {
            get => projectile.ai[1];
        }
        public override void AI()
        {
            projectile.frameCounter++;
            projectile.rotation = projectile.velocity.ToRotation();
            if (projectile.frameCounter == 1)
                rotationInitial = projectile.rotation;
            projectile.velocity = new Vector2(speed, 0).RotatedBy(rotationInitial) + new Vector2(0, (float)Math.Sin(projectile.frameCounter/6f) * 2f).RotatedBy(rotationInitial);
        }
    }
}
