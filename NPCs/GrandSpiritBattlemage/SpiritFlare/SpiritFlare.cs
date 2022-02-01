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
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.height = 11;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.damage = 24;
            projectile.timeLeft = 300;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.friendly = false;
            projectile.hostile = true;
        }

        int Target
        {
            get => (int)projectile.ai[0];
            set => projectile.ai[0] = value;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.timeLeft = 0;
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
                    float distance = Main.player[i].DistanceSQ(projectile.Center);
                    if (distance <= Math.Pow(DETECTION_THRESHOLD, 2) && (distance < lastDistance || lastDistance == 0))
                    {
                        lastDistance = distance;
                        closest = Main.player[i].whoAmI;
                    }
                }
            }
            Target = closest;
        }
        void SetVelocity() => projectile.velocity = projectile.velocity.SafeNormalize(Vector2.UnitX) * 5f;
        public void TryApproachTarget()
        {
            if (Target != -1 && projectile.frameCounter < 150)
            {
                var player = Main.player[Target];
                finalVelocity = -(projectile.Center - player.Center).SafeNormalize(Vector2.UnitX) * projectile.velocity.Length() * 1.04f;
            }
            projectile.velocity = Vector2.Lerp(projectile.velocity, finalVelocity, 0.25f);
            if (State1)
            {
                SetVelocity();
            }
        }

        void IncrementFrameCounter() => projectile.frameCounter++;
        void ChangeState()
        {
            if (projectile.frameCounter > 145)
            {
                State = 1;
            }
        }
        void IfStartedSetFinalVelocity()
        {
            if (projectile.frameCounter == 0) 
            {
                finalVelocity = projectile.velocity;
            }
        }

        void FixRotation() => projectile.rotation = projectile.velocity.ToRotation();

        Vector2 finalVelocity;
        int State
        {
            get => (int)projectile.ai[0];
            set => projectile.ai[0] = value;
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