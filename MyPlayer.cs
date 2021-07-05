using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;

namespace SealTheHeavens
{
    public class MyPlayer : ModPlayer
    {
        private const int saveVersion = 0;
        public bool SpicySpaceSnakeMinion;
		public bool minionName = false;
		public bool BirdBuff = false;

		public bool StarsrykesRespawnBonus;
		int StarsrykesRespawnBonusCount;
		int StarsrykesRespawnBonusTime;
		bool StarsrykesRespawnBonusCheck;
		int StarsrykesRespawnBonusHit;

        public override void ResetEffects()
        {
            SpicySpaceSnakeMinion = false;
			BirdBuff = false;
		}
	}
}
