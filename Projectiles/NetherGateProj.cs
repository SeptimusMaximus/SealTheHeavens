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
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.damage = 20;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 30;
        }

        float threshold
        {
            get => Projectile.ai[0] * 3;
            set => Projectile.ai[0] = value;
        }

        float initialRotation
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public override void AI()
        {
            if (initialRotation == -1)
                initialRotation = Projectile.velocity.ToRotation();
            if (Projectile.velocity.Length() > threshold)
            {
                Projectile.velocity = -Projectile.velocity;
                threshold = 9999;
            }

            else
            {
                Projectile.velocity -= new Vector2(0, 1.5f).RotatedBy(initialRotation);
            }
        }
    }
}
