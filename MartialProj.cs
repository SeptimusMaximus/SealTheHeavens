using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SealTheHeavens
{
	public abstract class MartialProj : ModProjectile
    {
		public override void AI()
        {
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) //damage edit not needed, since it's added from the item
        {
			if (Main.player[projectile.owner].GetModPlayer<MartialPlayer>().martialCrit > 0)
            {
                if (Main.rand.Next(1, 101) < (Main.player[projectile.owner].GetModPlayer<MartialPlayer>().martialCrit))
                {
                    crit = true;
                }
			}
		}
		public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
			if (Main.player[projectile.owner].GetModPlayer<MartialPlayer>().martialCrit > 0)
            {
                if (Main.rand.Next(1, 101) < (Main.player[projectile.owner].GetModPlayer<MartialPlayer>().martialCrit))
                {
                    crit = true;
                }
			}
		}
	}
}
