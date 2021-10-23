using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SealTheHeavens.Projectiles;
using Microsoft.Xna.Framework;

namespace SealTheHeavens.Items.Martial
{
    public class ShiningStar : MartialItem
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
            item.useTime = 4;
            item.useAnimation = 18;
            item.reuseDelay = 42;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.value = Item.sellPrice(0, 12, 50);
            item.UseSound = SoundID.Item84;
            item.rare = 12;
            item.noMelee = true;
            item.shoot = ModContent.ProjectileType<Projectiles.ShiningStar>();
            item.shootSpeed = 9f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 shootPosition = player.MountedCenter + Main.rand.NextVector2Circular(84, 72);
            Vector2 velocity = (Main.screenPosition + Main.MouseScreen - shootPosition).SafeNormalize(Vector2.UnitX) * new Vector2(speedX, speedY).Length();
            // Projectile.NewProjectile(shootPosition, velocity, type, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(shootPosition, velocity, type, damage, knockBack, player.whoAmI, 0f, item.shootSpeed);
            return false;
        }
    }
}
