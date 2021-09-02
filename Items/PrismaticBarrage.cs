using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SealTheHeavens.Projectiles;
using Microsoft.Xna.Framework;

namespace SealTheHeavens.Items
{
    public class PrismaticBarrage : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prismatic Blade Barrage");
            Tooltip.SetDefault("Shoots 2 black blades and 1 rainbow blade.");
        }
        public override void SetDefaults()
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
            item.shoot = ModContent.ProjectileType<BlackBlade>();
            item.shootSpeed = 7f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 speed = new Vector2(speedX, speedY);
            for (int i = -1; i <= 1; i++)
            {
                type = i % 2 == 0 ? ModContent.ProjectileType<BlackBlade>() : ModContent.ProjectileType<BlackBlade>();
                Projectile.NewProjectile(position, speed.RotatedBy(MathHelper.ToRadians(10) * i), type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
