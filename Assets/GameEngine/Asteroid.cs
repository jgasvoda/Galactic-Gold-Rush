using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameEngine
{
    public class Asteroid
    {
        public int Number { get; set; }
        public int ControllingPlayer { get; set; }


        private int RemainingGold { get; set; }
        private int GoldPile { get; set; }

        public Asteroid(int number, int goldCount)
        {
            Number = number;
            RemainingGold = goldCount;
            ControllingPlayer = 0;
        }

        public void MineGold(int amount)
        {
            GoldPile += amount;
            RemainingGold -= amount;
        }

        public int TakeGold()
        {
            int temp = GoldPile;
            GoldPile = 0;
            return temp;
        }

        public int GetRemainingGold()
        {
            return RemainingGold;
        }
    }
}
