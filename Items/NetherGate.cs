using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SealTheHeavens.Projectiles;
using Microsoft.Xna.Framework;

namespace SealTheHeavens.Items
{
    public class NetherGate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prismatic Blade Barrage");
            Tooltip.SetDefault("Shoots 2 black blades and 1 rainbow blade.");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.damage = 30;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 12, 50);
            Item.UseSound = SoundID.Item84;
            Item.rare = 12;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<NetherGateProj>();
            Item.shootSpeed = 7f;
        }
        public static bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, 30, knockBack, player.whoAmI, shootSpeed, -1);
            return false;
        }
    }
}
