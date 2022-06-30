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
            Item.width = 30;
            Item.height = 30;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.damage = 30;
            Item.useTime = 4;
            Item.useAnimation = 18;
            Item.reuseDelay = 42;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 12, 50);
            Item.UseSound = SoundID.Item84;
            Item.rare = 12;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.ShiningStar>();
            Item.shootSpeed = 9f;
        }
        public static bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 shootPosition = player.MountedCenter + Main.rand.NextVector2Circular(84, 72);
            Vector2 velocity = (Main.screenPosition + Main.MouseScreen - shootPosition).SafeNormalize(Vector2.UnitX) * new Vector2(speedX, speedY).Length();
            // Projectile.NewProjectile(shootPosition, velocity, type, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(shootPosition, velocity, type, damage, knockBack, player.whoAmI, 0f, shootSpeed);
            return false;
        }
    }
}
