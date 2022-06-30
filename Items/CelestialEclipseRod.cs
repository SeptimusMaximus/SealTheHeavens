using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SealTheHeavens.Projectiles;

namespace SealTheHeavens.Items
{
	public class CelestialEclipseRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Eclipse Rod");
			Tooltip.SetDefault("Casts two Homing Orbs");
		}

		public override void SetDefaults()
		{
			Item.damage = 36;
			Item.crit = 11;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 12;
			Item.width = 74;
			Item.height = 74;
			Item.useTime = 22;
			Item.useAnimation = 31;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4.85f;
			Item.scale = 0.95f;
			Item.value = 989075;
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item43;
			Item.shoot = ModContent.ProjectileType<CelestialSun>();
			Item.shootSpeed = 8.5f;
			Item.autoReuse = true;
			Item.staff[Item.type] = true;
		}

		public override Vector2? HoldoutOffset() {
			return Vector2.Zero;
		}

		public static bool Shoot(Player player, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(position, velocity*0.6f, /*ModContent.ProjectileType<CelestialMoon>()*/ damage, knockback, player.whoAmI);

			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			return true;
		}
	}
}
