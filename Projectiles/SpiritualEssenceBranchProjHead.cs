using System;
using SealTheHeavens.Items.Martial;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SealTheHeavens.Projectiles
{
    public class SpiritualEssenceBranchProjHead : MartialProj
    {
        public override string Texture => "SealTheHeavens/Projectiles/SpiritualEssenceBranchProj";
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
            DisplayName.SetDefault("Celestial Eclipse Sun");
			//aiType = 495;
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
			Projectile.width = 30;
			Projectile.height = 31;
            Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = false;
			Projectile.penetrate = 999;
			Projectile.timeLeft = 175;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public static bool PreDraw(SpriteBatch spriteBatch, Color lightColor, Projectile projectile)
        {
            var myTexture = ModContent.Request<Texture2D>("Projectiles/SpiritualEssenceBranchProj");
            var rect = new Rectangle(0, 0, 30, 31);
            Main.spriteBatch.Draw((Texture2D)myTexture, projectile.Center - Main.screenPosition, rect, lightColor, projectile.rotation, rect.Size() / 2f, 1f, SpriteEffects.None, 0f);
            return false;
        }

        float groundCovered = 0f;
        int Max
        {
            get => (int)Projectile.ai[0] - 1;
        }
        int Trails
        {
            get => (int)Projectile.ai[1];
            set
            {
                Projectile.ai[1] = value;
            }
        }
        const float SPEED = 5f;
        public override void AI()
        {
            Projectile.velocity = Vector2.UnitX.RotatedBy(Projectile.rotation - MathHelper.PiOver2) * SPEED;
            groundCovered += Projectile.velocity.Length();
            if (groundCovered >= 25 && Trails < Max)
            {
                if (Trails + 1 < Max)
                {
                    var trail = Projectile.NewProjectileDirect(Projectile.Center + (Projectile.velocity.SafeNormalize(Vector2.UnitX) * 1.5f), Vector2.Zero, ModContent.ProjectileType<SpiritualEssenceBranchProjTrail>(), Projectile.damage, 0, Projectile.owner, Projectile.whoAmI);
                    trail.rotation = Projectile.rotation;
                }
                groundCovered = 0f;
                Trails++;
            }

            else if (Trails >= Max)
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.timeLeft = Projectile.timeLeft > 30 ? 30 : Projectile.timeLeft;
            }
        }
    }
}
