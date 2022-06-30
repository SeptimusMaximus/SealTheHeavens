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
			Projectile.penetrate = 3;
			Projectile.timeLeft = 175;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public float groundCovered;
        public static bool PreDraw(SpriteBatch spriteBatch, Color lightColor, Projectile projectile)
        {
            var myTexture = ModContent.Request<Texture2D>("Projectiles/SpiritualEssenceBranchProj");
            var rect = new Rectangle(0, 31, 30, 31);
            Main.spriteBatch.Draw((Texture2D)myTexture, projectile.Center - Main.screenPosition, rect, lightColor, projectile.rotation, rect.Size() / 2f, 1f, SpriteEffects.None, 0f);
            return false;
        }

        Projectile parent
        {
            get => Main.projectile[(int)Projectile.ai[0]];
        }

        public override void AI()
        {
            if (!parent.active)
            {
                Projectile.timeLeft = 0;
            }

            else if (parent.timeLeft == 30)
            {
                Projectile.timeLeft = 30;
            }
        }
    }
}
