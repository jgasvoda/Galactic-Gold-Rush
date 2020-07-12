using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGR_Game_Engine
{
    public class Asteroid
    {
        private int MineralCount { get; set; }
        private int PlayerMine { get; set; }

        public Asteroid(int mineralCount)
        {
            this.MineralCount = mineralCount;
            PlayerMine = 0;
        }
    }
}
