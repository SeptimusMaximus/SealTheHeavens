using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SealTheHeavens.Items
{
    public class SoulSacrifice : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Sacrifice");
            Tooltip.SetDefault("Use to deal one of two effects to the player:\n" +
                "70% chance to heal the player 100 life\n" +
                "30% to deal 50 damage to the player");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 36;
            item.rare = ItemRarityID.Orange;
            item.useAnimation = 120;
            item.useTime = 120;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.UseSound = SoundID.Item103;
            item.consumable = false;
            item.autoReuse = false;
            item.damage = 50;
        }

        public override bool UseItem(Player player)
        {
            int randChance = Main.rand.Next(10);

            if (randChance > 3)
            {
                player.statLife += 100;
                player.HealEffect(100);
            }
            else
            {
                player.Hurt(PlayerDeathReason.ByCustomReason("'s soul was sacrificed"), 50, 1);
            }

            return base.UseItem(player);
        }
    }
}