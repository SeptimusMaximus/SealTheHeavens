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
            Main.projFrames[projectile.type] = 2;
            DisplayName.SetDefault("Celestial Eclipse Sun");
			//aiType = 495;
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
			projectile.width = 30;
			projectile.height = 31;
            projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.penetrate = 999;
			projectile.timeLeft = 175;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var myTexture = mod.GetTexture("Projectiles/SpiritualEssenceBranchProj");
            var rect = new Rectangle(0, 0, 30, 31);
            Main.spriteBatch.Draw(myTexture, projectile.Center - Main.screenPosition, rect, lightColor, projectile.rotation, rect.Size() / 2f, 1f, SpriteEffects.None, 0f);
            return false;
        }

        float groundCovered = 0f;
        int max
        {
            get => (int)projectile.ai[0] - 1;
        }
        int trails
        {
            get => (int)projectile.ai[1];
            set
            {
                projectile.ai[1] = value;
            }
        }
        const float SPEED = 5f;
        public override void AI()
        {
            projectile.velocity = Vector2.UnitX.RotatedBy(projectile.rotation - MathHelper.PiOver2) * SPEED;
            groundCovered += projectile.velocity.Length();
            if (groundCovered >= 25 && trails < max)
            {
                if (trails + 1 < max)
                {
                    var trail = Projectile.NewProjectileDirect(projectile.Center + projectile.velocity.SafeNormalize(Vector2.UnitX) * 1.5f, Vector2.Zero, ModContent.ProjectileType<SpiritualEssenceBranchProjTrail>(), projectile.damage, 0, projectile.owner, projectile.whoAmI);
                    trail.rotation = projectile.rotation;
                }
                groundCovered = 0f;
                trails++;
            }

            else if (trails >= max)
            {
                projectile.velocity = Vector2.Zero;
                projectile.timeLeft = projectile.timeLeft > 30 ? 30 : projectile.timeLeft;
            }
        }
    }
}
