using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

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
			item.damage = 36;
			item.crit = 11;
			item.magic = true;
			item.mana = 12;
			item.width = 74;
			item.height = 74;
			item.useTime = 22;
			item.useAnimation = 31;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 4.85f;
			item.scale = 0.95f;
			item.value = 989075;
			item.rare = 11;
			item.UseSound = SoundID.Item43;
			item.shoot = mod.ProjectileType("CelestialSun");
			item.shootSpeed = 8.5f;
			item.autoReuse = true;
			Item.staff[item.type] = true;
		}

		public override Vector2? HoldoutOffset() {
			return Vector2.Zero;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX*0.6f, speedY*0.6f, mod.ProjectileType("CelestialMoon") , damage, knockBack, player.whoAmI);

			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			return true;
		}

	}
}
