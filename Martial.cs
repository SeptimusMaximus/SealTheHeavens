using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SealTheHeavens
{
	public class MartialPlayer : ModPlayer
	{
		public static MartialPlayer ModPlayer(Player player) {
			return player.GetModPlayer<MartialPlayer>();
		}

		public float martialDamage = 1f;
		public float martialKB;
		public int martialCrit = 4;

		private static int DefaultQiMax = 1000;//change default max qi
		public int statQiMax = 1000;
		public int statQi = 0;

		public float qiCost = 1f;//decrease this value multiplier for less qi cost

		private static int DefaultQiRegen = 1;//change default qi regen rate
		public int qiRegen = 1;
		public int qiRegenCount = 0;
		public int qiRegenBonus = 0;//increase this value for extra regen rate accessory or armor

		public float maxQiRegenDelay = 0f;//
		public int qiRegenDelay = 0;
		public int qiRegenDelayBonus = 0;//increasing this makes the delay decrease faster

		public bool martialItem = false;

		public override void Initialize() {

		}

		public override void ResetEffects() {
			ResetVariables();
		}
		public override void UpdateDead() {
			ResetVariables();
		}

		private void ResetVariables() {
			martialDamage = 1f;
			martialKB = 0f;
			martialCrit = 4;
			statQiMax = DefaultQiMax;
			qiRegenBonus = 0;
			qiRegenDelayBonus = 0;
			qiCost = 1f;
			martialItem = false;
		}

		public override void PostUpdateMiscEffects() {
			UpdateResource();
		}

		private void UpdateResource() {
			maxQiRegenDelay = (1f - (float)statQi / (float)statQiMax) * 60f * 4f + 45f;
			maxQiRegenDelay *= 0.7f;
			if (qiRegenDelay > 0)
			{
				qiRegenDelay--;
				qiRegenDelay -= qiRegenDelayBonus;
				if ((player.velocity.X == 0f && player.velocity.Y == 0f) || player.grappling[0] >= 0 /*|| manaRegenBuff*/)
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
				if ((player.velocity.X == 0f && player.velocity.Y == 0f) || player.grappling[0] >= 0 /*|| manaRegenBuff*/)
				{
					qiRegen += statQiMax / 2;
				}
				float num2 = (float)statQi / (float)statQiMax * 0.8f + 0.2f;
				/*if (manaRegenBuff)
				{
					num2 = 1f;
				}*/
				qiRegen = (int)((double)((float)qiRegen * num2) * 1.15);
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
				if (player.whoAmI == Main.myPlayer && flag)
				{
					Main.PlaySound(25);//sound of full mana
					for (int i = 0; i < 5; i++)
					{
						//this is the dust that appears when having full mana as well, I tried making it orange but it didn't work, so change it if ya want
						int num3 = Dust.NewDust(player.position, player.width, player.height, 45, 0f, 0f, 255, Color.Orange, (float)Main.rand.Next(20, 26) * 0.1f);
						Main.dust[num3].noLight = true;
						Main.dust[num3].noGravity = true;
						Main.dust[num3].velocity *= 0.5f;
					}
				}
				statQi = statQiMax;
			}
			if (martialItem /*&& (item.type != 127 || !spaceGun)*/) //bools for the space gun and meteorite set bonus, left them here just in case
			{
				qiRegenDelay = (int)maxQiRegenDelay;
			}
		}

		public override void ProcessTriggers(TriggersSet triggersSet) {

		}

		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) {
			ModPacket packet = mod.GetPacket();
			packet.Write((byte)player.whoAmI);
			packet.Write(statQi);
			packet.Send(toWho, fromWho);
		}
		public override TagCompound Save() {
			return new TagCompound {
				{"statQi", statQi},
			};
		}
		public override void Load(TagCompound tag) {
			statQi = tag.GetInt("statQi");
		}
		public override void SendClientChanges(ModPlayer clientPlayer) {
			MartialPlayer clone = clientPlayer as MartialPlayer;
			if (clone.statQi != statQiMax) {
				var packet = mod.GetPacket();
				packet.Write((byte)player.whoAmI);
				packet.Write(statQi);
				packet.Send();
			}
		}
	}
}
