using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SealTheHeavens.Items.Martial
{
	public class MartialPlayer : ModPlayer
	{
		public static MartialPlayer ModPlayer(Player player)
		{
			return player.GetModPlayer<MartialPlayer>();
		}

		public float martialDamage = 1f;
		public float martialKB;
		public int martialCrit = 4;

		private int DefaultQiMax = 1000;//change default max qi
		public int statQiMax = 1000;
		public int statQi = 0;

		private int DefaultQiRegen = 1;//change default qi regen rate
		public int qiRegen = 1;
		public int qiRegenCount = 0;
		public int qiRegenBonus = 0;//increase this value for extra regen rate accessory or armor
		private int maxQiRegenDelay = 0;
		public int qiRegenDelay = 0;
		public int qiRegenDelayBonus = 0;

		public bool martialItem = false;

		public override void Initialize()
		{

		}

		public override void ResetEffects()
		{
			ResetVariables();
		}
		public override void UpdateDead()
		{
			ResetVariables();
		}

		private void ResetVariables()
		{
			martialDamage = 1f;
			martialKB = 0f;
			martialCrit = 4;
			statQiMax = DefaultQiMax;
			qiRegenBonus = 0;
			qiRegenDelayBonus = 0;
			martialItem = false;
		}

		public override void PostUpdateMiscEffects()
		{
			UpdateResource();
		}

		private void UpdateResource()
		{
			if (qiRegenDelay > 0)
			{
				qiRegenDelay--;
				qiRegenDelay -= qiRegenDelayBonus;
				if ((Player.velocity.X == 0f && Player.velocity.Y == 0f) || Player.grappling[0] >= 0 /*|| manaRegenBuff*/)
				{
					qiRegenDelay--;
				}
			}
			/*if (manaRegenBuff && qiRegenDelay > 20)
			{
				qiRegenDelay = 20;
			}*/
			if (qiRegenDelay <= 0)
			{
				qiRegenDelay = 0;
				qiRegen = statQiMax / 7 + 1 + qiRegenBonus;
				if ((Player.velocity.X == 0f && Player.velocity.Y == 0f) || Player.grappling[0] >= 0 /*|| manaRegenBuff*/)
				{
					qiRegen += statQiMax / 2;
				}
				float num2 = statQi / statQiMax * 0.8f + 0.2f;
				/*if (manaRegenBuff)
				{
					num2 = 1f;
				}*/
				qiRegen = (int)((double)(qiRegen * num2) * 1.15);
			}
			else
			{
				qiRegen = 0;
			}
			qiRegenCount += qiRegen;
			while (qiRegenCount >= 120)
			{
				bool flag = false;
				qiRegenCount -= 120;
				if (statQi < statQiMax)
				{
					statQi++;
					flag = true;
				}
				if (statQi < statQiMax)
				{
					continue;
				}
				if (Player.whoAmI == Main.myPlayer && flag)
				{
					SoundEngine.PlaySound(SoundID.MaxMana);
					for (int i = 0; i < 5; i++)
					{
						int num3 = Dust.NewDust(Player.position, Player.width, Player.height, DustID.ManaRegeneration, 0f, 0f, 255, Color.Orange, (float)Main.rand.Next(20, 26) * 0.1f);
						Main.dust[num3].noLight = true;
						Main.dust[num3].noGravity = true;
						Main.dust[num3].velocity *= 0.5f;
					}
				}
				statQi = statQiMax;
			}
			if (martialItem /*&& (item.type != 127 || !spaceGun)*/) //bools for the space gun and meteorite set bonus, left them here just in case
			{
				qiRegenDelay = maxQiRegenDelay;
			}
		}

		public override void ProcessTriggers(TriggersSet triggersSet)
		{

		}

		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write((byte)Player.whoAmI);
			packet.Write(statQi);
			packet.Send(toWho, fromWho);
		}
		public override void SaveData(TagCompound tag)
		{
			new TagCompound()
			{
				{"statQi", statQi},
			};
		}
		public override void LoadData(TagCompound tag)
		{
			statQi = tag.GetInt("statQi");
		}
		public override void SendClientChanges(ModPlayer clientPlayer)
		{
			MartialPlayer clone = clientPlayer as MartialPlayer;
			if (clone.statQi != statQiMax)
			{
				var packet = Mod.GetPacket();
				packet.Write((byte)Player.whoAmI);
				packet.Write(statQi);
				packet.Send();
			}
		}
	}
}
