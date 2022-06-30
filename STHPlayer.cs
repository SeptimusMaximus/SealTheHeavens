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
    public class STHPlayer : ModPlayer
    {
        //private const int saveVersion = 0;
        public bool SpicySpaceSnakeMinion;
		public bool minionName = false;
		public bool BirdBuff = false;

		public bool StarsrykesRespawnBonus;
        //readonly int StarsrykesRespawnBonusCount;
		//readonly int StarsrykesRespawnBonusTime;
		//readonly bool StarsrykesRespawnBonusCheck;
		//readonly int StarsrykesRespawnBonusHit;

        public override void ResetEffects()
        {
            SpicySpaceSnakeMinion = false;
			BirdBuff = false;
		}
	}
}
