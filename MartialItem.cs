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
	public abstract class MartialItem : ModItem
	{
		public override bool CloneNewInstances => true;
		public int qi = 0;

		public virtual void SafeSetDefaults() {
		}

		public sealed override void SetDefaults() {
			SafeSetDefaults();
			item.melee = false;
			item.ranged = false;
			item.magic = false;
			item.thrown = false;
			item.summon = false;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat) {
			mult *= MartialPlayer.ModPlayer(player).martialDamage;
		}

		public override void GetWeaponKnockback(Player player, ref float knockback) {
			knockback += MartialPlayer.ModPlayer(player).martialKB;
		}

		public override void GetWeaponCrit(Player player, ref int crit) {
			crit += MartialPlayer.ModPlayer(player).martialCrit;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
			if (tt != null) {
				string[] splitText = tt.text.Split(' ');
				string damageValue = splitText.First();
				string damageWord = splitText.Last();
				tt.text = damageValue + " martial " + damageWord;
			}

			//change the color code here to set the color, look up for color code picker and replace the code below. I'm using this for my Artist and Programmer classes
			tooltips.Add(new TooltipLine(mod, "SealTheHeavens Martial Item", $"[c/faa7ad:< Martial >]"));
		}
		public override void HoldItem(Player player)
        {
			if(qi > 0)player.GetModPlayer<MartialPlayer>().martialItem = true;
		}
		public override bool CanUseItem(Player player) {
			if(player.GetModPlayer<MartialPlayer>().statQi >= (int)((float)qi * player.GetModPlayer<MartialPlayer>().qiCost)) {
				player.GetModPlayer<MartialPlayer>().statQi -= (int)((float)qi * player.GetModPlayer<MartialPlayer>().qiCost);
				player.GetModPlayer<MartialPlayer>().qiRegenDelay = (int)player.GetModPlayer<MartialPlayer>().maxQiRegenDelay;
				return true;
			}
			return false;
		}
	}
}
