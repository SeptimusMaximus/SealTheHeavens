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
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 22;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 400;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.ai[1] == 0)
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    Projectile.velocity = Projectile.DirectionTo(Main.MouseWorld).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-5, 6)));
                }
                Helper.Dusts.DustCircle2(Projectile.Center, DustID.Shadowflame, 16, Projectile.rotation, Color.Black);
                Projectile.ai[1] = 1;
            }
            Projectile.ai[0] += 1/60f;
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, 0, 0, 0, Color.Black);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 0.5f;
            Projectile.velocity *= 1.1f;
            if (Projectile.velocity.Length() > 32f)
            {
                Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 32;
            }
            NPC n = Helper.NPCs.FindNearestNPCDirect(Projectile.Center, 200f);
            if (n != null)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(n.Center) * 32, .07f);
            }
        }
        public static bool PreDraw(SpriteBatch spriteBatch, Color lightColor, Projectile projectile)
        {
            Texture2D texture = Main.ProjectileTexture[projectile.type]; // texture
            int num156 = Main.PojectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; // height of each frame
            int y = num156 * projectile.frame; // height * frame
            Rectangle rectangle = new(0, y, texture.Width, num156); // rectangle to see which parts get drawn format: (x pos, y pos, width, height)
            Vector2 origin = rectangle.Size() / 2f; // pretty sure its the center
            Color c1 = Color.Black;
            Color c2 = new Color(220, 255, 153);
            Color c3 = new Color(255, 200, 129);
            Color c4 = new Color(255, 102, 142);
            Color alpha2 = Helper.Colors.Lerp4(c1, c2, c3, c4, (float)Math.Sin(projectile.ai[0] % Math.PI));
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(rectangle), alpha2, projectile.rotation, origin, projectile.scale + (.1f * (float)Math.Sin(projectile.ai[0] % Math.PI)), SpriteEffects.None, 0f);
            return false;
        }
    }
}
