using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SealTheHeavens.Items
{
	public class StarfleetScrap : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starfleet Scrap");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 22;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(0, 0, 1, 0);
			item.maxStack = 999;
		}
	}
}
