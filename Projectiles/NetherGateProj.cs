using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SealTheHeavens.Projectiles;
using Microsoft.Xna.Framework;

namespace SealTheHeavens.Projectiles
{
    public class NetherGateProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.damage = 20;
            projectile.aiStyle = -1;
            projectile.timeLeft = 30;
        }

        float threshold
        {
            get => projectile.ai[0] * 3;
            set => projectile.ai[0] = value;
        }

        float initialRotation
        {
            get => projectile.ai[1];
            set => projectile.ai[1] = value;
        }

        public override void AI()
        {
            if (initialRotation == -1)
                initialRotation = projectile.velocity.ToRotation();
            if (projectile.velocity.Length() > threshold)
            {
                projectile.velocity = -projectile.velocity;
                threshold = 9999;
            }

            else
            {
                projectile.velocity -= new Vector2(0, 1.5f).RotatedBy(initialRotation);
            }
        }
    }
}
