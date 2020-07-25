using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameEngine
{
    public class Coordinates
    {
        public int Section { get; set; }
        public int Row { get; set; }
        public int Position { get; set; }

        public Coordinates(int section, int row, int position)
        {
            this.Section = section;
            this.Row = row;
            this.Position = position;
        }

        public override string ToString()
        {
            return this.Section + "," + this.Row + "," + this.Position;
        }
    }
}
