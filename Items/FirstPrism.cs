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
			Tooltip.SetDefault("The start of all existing prisms.\nShoots a entity deleting beam of light.");
		}
		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.reuseDelay = 5;
			Item.shootSpeed = 30f;
			Item.knockBack = 0f;
			Item.width = 16;
			Item.height = 16;
			Item.UseSound = SoundID.Item13;
			Item.shoot = ProjectileID.LastPrism;
			Item.mana = 12;
			Item.rare = 12;
			Item.value = Item.sellPrice(0, 25);
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.mana = 20;
			Item.damage = 175;
			Item.shoot = ModContent.ProjectileType<FirstPrismHoldout>();
		}
		public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<FirstPrismHoldout>()] <= 0;
	}
}
