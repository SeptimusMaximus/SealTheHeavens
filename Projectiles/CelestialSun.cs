using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SealTheHeavens.Projectiles
{
    public class CelestialSun : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Eclipse Sun");
			//aiType = 495;
        }

        public override void SetDefaults()
        {
			Projectile.aiStyle = -1;
			Projectile.width = 30;
			Projectile.height = 30;
            Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = false;
			Projectile.penetrate = 5;
			Projectile.extraUpdates = 1;
			Projectile.timeLeft = 175;
            Projectile.DamageType = DamageClass.Magic;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
        }

		private const float maxTicks = 60f;
        private const int alphaReducation = 25;

		public override void AI()
        {
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.7f, 0.5f, 0.2f);

			int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 64, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 100, default(Color), 0.25f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].scale = 1f;

			Projectile.rotation += 0.2f;
			float maxdist = 600;
			for(int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (Projectile.Distance(n.Center) < maxdist && n.active && !n.friendly)
                {
                    Projectile.ai[0] = n.whoAmI;
                    maxdist = Projectile.Distance(n.Center);
                }
            }
            if (Projectile.ai[0] > 0 && Projectile.ai[0] < 200)
            {
                Vector2 vel = Projectile.DirectionTo(Main.npc[(int)Projectile.ai[0]].Center) * 8;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, vel, .1f);
            }
        }
    }
}
