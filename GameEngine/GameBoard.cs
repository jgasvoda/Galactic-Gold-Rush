using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGR_Game_Engine
{
    public class GameBoard
    {
        private int BoardSize { get; set; }
        public List<List<List<BoardSpace>>> Spaces { get; set; }

        public GameBoard(int boardSize)
        {
            if(boardSize < 2)
            {
                boardSize = 2;
            }
            this.BoardSize = boardSize;

            GenerateBoard();
        }

        private void GenerateBoard()
        {
            Spaces = new List<List<List<BoardSpace>>>();

            //Create BoardSpaces
            for(int i = 0; i < 6; i++)
            {
                Spaces.Add(new List<List<BoardSpace>>());

                for(int j = 0; j < BoardSize; j++)
                {
                    Spaces[i].Add(new List<BoardSpace>());

                    for(int k = 0; k <= (j * 2); k++)
                    {
                        Spaces[i][j].Add(new BoardSpace(i,j,k, BoardSize));
                    }
                }
            }

            //Fill spaces with 12 Asteroids
            Random rnd = new Random();
            for (int i = 0; i < (BoardSize - 1) * 4; i++)
            {
                bool spaceFound = false;
                while(!spaceFound)
                {
                    int section = rnd.Next(6);
                    int row = rnd.Next(1, BoardSize);
                    int position = rnd.Next((row * 2) + 1);

                    if(!Spaces[section][row][position].isFilled())
                    {
                        Spaces[section][row][position].asteroid = new Asteroid(10);
                        spaceFound = true;
                    }
                }
            }

        }

    }
}
