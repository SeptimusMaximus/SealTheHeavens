using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SealTheHeavens.Projectiles;
using Microsoft.Xna.Framework;

namespace SealTheHeavens.Items
{
    public class SpiritualEssenceBranch : MartialItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prismatic Blade Barrage");
            Tooltip.SetDefault("Shoots 2 black blades and 1 rainbow blade.");
        }
        public override void SafeSetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.magic = true;
            item.mana = 12;
            item.damage = 30;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.value = Item.sellPrice(0, 12, 50);
            item.UseSound = SoundID.Item84;
            item.rare = 12;
            item.noMelee = true;
            item.shoot = ModContent.ProjectileType<SpiritualEssenceBranchProjHead>();
            item.shootSpeed = 5f; // when plugged into a vector, returns a magnitude of 6 (sqrt of 18)
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var projectile = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 9f, 0f);
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            return false;
        }
    }
}
