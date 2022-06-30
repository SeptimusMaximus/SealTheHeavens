using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SealTheHeavens.NPCs.GrandSpiritBattlemage.SpiritFlare
{
    public class SpiritFlare : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Eclipse Moon");
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.height = 11;
            Projectile.width = 32;
            Projectile.aiStyle = -1;
            Projectile.damage = 24;
            Projectile.timeLeft = 300;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.friendly = false;
            Projectile.hostile = true;
        }

        int Target
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.timeLeft = 0;
        }

        const int DETECTION_THRESHOLD = 480;
        public void DetectPlayer()
        {
            int closest = -1;
            if (State2)
            {
                float lastDistance = 0;
                for (int i = 0; i < Main.player.Length; i++)
                {
                    float distance = Main.player[i].DistanceSQ(Projectile.Center);
                    if (distance <= Math.Pow(DETECTION_THRESHOLD, 2) && (distance < lastDistance || lastDistance == 0))
                    {
                        lastDistance = distance;
                        closest = Main.player[i].whoAmI;
                    }
                }
            }
            Target = closest;
        }
        void SetVelocity() => Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.UnitX) * 5f;
        public void TryApproachTarget()
        {
            if (Target != -1 && Projectile.frameCounter < 150)
            {
                var player = Main.player[Target];
                finalVelocity = -(Projectile.Center - player.Center).SafeNormalize(Vector2.UnitX) * Projectile.velocity.Length() * 1.04f;
            }
            Projectile.velocity = Vector2.Lerp(Projectile.velocity, finalVelocity, 0.25f);
            if (State1)
            {
                SetVelocity();
            }
        }

        void IncrementFrameCounter() => Projectile.frameCounter++;
        void ChangeState()
        {
            if (Projectile.frameCounter > 145)
            {
                State = 1;
            }
        }
        void IfStartedSetFinalVelocity()
        {
            if (Projectile.frameCounter == 0) 
            {
                finalVelocity = Projectile.velocity;
            }
        }

        void FixRotation() => Projectile.rotation = Projectile.velocity.ToRotation();

        Vector2 finalVelocity;
        int State
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        bool State1 => State == 0;
        bool State2 => State == 1;
        public override void AI()
        {
            FixRotation();
            IfStartedSetFinalVelocity();
            IncrementFrameCounter();
            ChangeState();
            DetectPlayer();
            TryApproachTarget();
        }
    }
}