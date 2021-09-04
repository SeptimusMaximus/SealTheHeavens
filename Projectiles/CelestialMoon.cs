using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SealTheHeavens.Projectiles
{
    public class CelestialMoon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Eclipse Moon");
        }

        public override void SetDefaults()
        {
			projectile.aiStyle = 0;
			projectile.width = 30;
			projectile.height = 30;
            projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.penetrate = 5;
			projectile.extraUpdates = 1;
			projectile.timeLeft = 180;
			projectile.magic = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
        }

		public override void AI()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.5f, 0.2f, 0.7f);

			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 65, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 100, default(Color), 0.25f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].scale = 1f;

			projectile.rotation -= 0.2f;
			float maxdist = 600;
			for(int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (projectile.Distance(n.Center) < maxdist && n.active && !n.friendly)
                {
                    projectile.ai[0] = n.whoAmI;
                    maxdist = projectile.Distance(n.Center);
                }
            }
            if (projectile.ai[0] > 0 && projectile.ai[0] < 200)
            {
                Vector2 vel = projectile.DirectionTo(Main.npc[(int)projectile.ai[0]].Center) * 8;
                projectile.velocity = Vector2.Lerp(projectile.velocity, vel, .1f);
            }
        }
    }
}
