using System;
using SealTheHeavens.Items.Martial;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SealTheHeavens.Projectiles
{
    public class SpiritualEssenceBranchProjTrail : MartialProj
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
			projectile.penetrate = 3;
			projectile.timeLeft = 175;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }

        public float groundCovered;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var myTexture = mod.GetTexture("Projectiles/SpiritualEssenceBranchProj");
            var rect = new Rectangle(0, 31, 30, 31);
            Main.spriteBatch.Draw(myTexture, projectile.Center - Main.screenPosition, rect, lightColor, projectile.rotation, rect.Size() / 2f, 1f, SpriteEffects.None, 0f);
            return false;
        }

        Projectile parent
        {
            get => Main.projectile[(int)projectile.ai[0]];
        }

        public override void AI()
        {
            if (!parent.active)
            {
                projectile.timeLeft = 0;
            }

            else if (parent.timeLeft == 30)
            {
                projectile.timeLeft = 30;
            }
        }
    }
}
