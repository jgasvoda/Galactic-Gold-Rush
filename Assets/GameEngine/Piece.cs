using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameEngine
{
    public class Piece
    {
        private int Team { get; set; }
        private int Gold { get; set; }
        public Coordinates CurrentLocation { get; set; }
        public bool WaitingForMove { get; set; }

        public Piece(int team, Coordinates startingLocation)
        {
            this.Team = team;
            Gold = 0;
            CurrentLocation = startingLocation;
        }
        
        public void Move(Coordinates newLocation)
        {
            CurrentLocation = newLocation;
        }
        public void CollectGold(int amount)
        {
            Gold += amount;
        }
        public int DepositGold()
        {
            int amount = Gold;
            Gold = 0;
            return amount;
        }
    }
}
