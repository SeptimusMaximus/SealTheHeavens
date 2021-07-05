using SealTheHeavens.Projectiles;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace SealTheHeavens.Items
{
	public class FirstPrism : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Astral Refractor");
			Tooltip.SetDefault(@"The start of all existing prisms
Shoots a entity deleting beam of light.");
		}

		public override void SetDefaults()
		{
			item.useStyle = 5;
			item.useAnimation = 10;
			item.useTime = 10;
			item.reuseDelay = 5;
			item.shootSpeed = 30f;
			item.knockBack = 0f;
			item.width = 16;
			item.height = 16;
			item.UseSound = SoundID.Item13;
			item.shoot = 633;
			item.mana = 12;
			item.rare = 12;
			item.value = Item.sellPrice(0, 25);
			item.noMelee = true;
			item.noUseGraphic = true;
			item.magic = true;
			item.channel = true;
			item.mana = 20;
			item.damage = 175;
			item.shoot = ModContent.ProjectileType<FirstPrismHoldout>();
		}

		public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<FirstPrismHoldout>()] <= 0;
	}
}
