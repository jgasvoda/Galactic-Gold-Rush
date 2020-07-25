using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameEngine
{
    public class BoardSpace
    {
        private Coordinates BoardLocation { get; set; }
        private bool PiecePresent { get; set; }
        public Asteroid asteroid { get; set; }
        //public bool WormholePresent { get; set; }
        //public bool PowerupPresent { get; set; }
        private Coordinates Wormhole { get; set; }
        public List<Coordinates> AdjacentSpaces { get; set; }

        public BoardSpace(int section, int row, int position, int boardSize)
        {
            BoardLocation = new Coordinates(section, row, position);

            AdjacentSpaces = new List<Coordinates>();

            #region Assign Adjacent Spaces
            //Adjacent space is away from center
            if (position % 2 == 0)
            {
                if(row + 1 < boardSize)
                {
                    AdjacentSpaces.Add(new Coordinates(section, row + 1, position + 1));
                }
            }
            //Else Adjacent space is towards center
            else
            {
                AdjacentSpaces.Add(new Coordinates(section, row - 1, position - 1));
            }
            
            //CounterClockwise
            //If position is 0, adjacent space is in neighboring section
            if(position == 0)
            {
                if(section == 0)
                {
                    AdjacentSpaces.Add(new Coordinates(5, row, row * 2));
                }
                else
                {
                    AdjacentSpaces.Add(new Coordinates(section - 1, row, row * 2));
                }
            }
            else
            {
                AdjacentSpaces.Add(new Coordinates(section, row, position - 1));
            }

            //Clockwise
            //If position is at the end of the row, adjacent space is in neighboring section
            if (position == row * 2)
            {
                if(section == 5)
                {
                    AdjacentSpaces.Add(new Coordinates(0, row, 0));
                }
                else
                {
                    AdjacentSpaces.Add(new Coordinates(section + 1, row, 0));
                }
            }
            else
            {
                AdjacentSpaces.Add(new Coordinates(section, row, position + 1));
            }
            #endregion
        }

        public bool isFilled()
        {
            return asteroid != null;
        }
    }
}
