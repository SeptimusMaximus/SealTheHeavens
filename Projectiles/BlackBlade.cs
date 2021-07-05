using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;

namespace SealTheHeavens.Projectiles
{
    public class BlackBlade: ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 22;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 400;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
            if (projectile.ai[1] == 0)
            {
                if (projectile.owner == Main.myPlayer)
                {
                    projectile.velocity = projectile.DirectionTo(Main.MouseWorld).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-5, 6)));
                }
                Helper.Dusts.DustCircle2(projectile.Center, DustID.Shadowflame, 16, projectile.rotation, Color.Black);
                projectile.ai[1] = 1;
            }
            projectile.ai[0] += 1/60f;
            int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Shadowflame, 0, 0, 0, Color.Black);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 0.5f;
            projectile.velocity *= 1.1f;
            if (projectile.velocity.Length() > 32f)
            {
                projectile.velocity = Vector2.Normalize(projectile.velocity) * 32;
            }
            NPC n = Helper.NPCs.FindNearestNPCDirect(projectile.Center, 200f);
            if (n != null)
            {
                projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.DirectionTo(n.Center) * 32, .07f);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type]; // texture
            int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; // height of each frame
            int y = num156 * projectile.frame; // height * frame
            Rectangle rectangle = new Rectangle(0, y, texture.Width, num156); // rectangle to see which parts get drawn format: (x pos, y pos, width, height)
            Vector2 origin = rectangle.Size() / 2f; // pretty sure its the center
            Color c1 = Color.Black;
            Color c2 = new Color(220, 255, 153);
            Color c3 = new Color(255, 200, 129);
            Color c4 = new Color(255, 102, 142);
            Color alpha2 = Helper.Colors.lerp4(c1, c2, c3, c4, (float)Math.Sin(projectile.ai[0] % Math.PI));
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(rectangle), alpha2, projectile.rotation, origin, projectile.scale + (.1f * (float)Math.Sin(projectile.ai[0] % Math.PI)), SpriteEffects.None, 0f);
            return false;
        }
    }
}
