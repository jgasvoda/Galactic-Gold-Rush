using GameEngine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGR_Game_Engine
{
    public sealed class GameManager : MonoBehaviour
    {
        private GameBoard Board { get; set; }
        private int CurrentPlayer { get; set; }

        private List<Piece> Player1Pieces { get; set; }
        private int Player1Score { get; set; }

        private List<Piece> Player2Pieces { get; set; }
        private int Player2Score { get; set; }

        private static readonly Lazy<GameManager>
        lazy =
        new Lazy<GameManager>
            (() => new GameManager());

        public static GameManager Instance { get { return lazy.Value; } }

        private GameManager()
        {
            CreateGame();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        
        private void CreateGame()
        {
            //Make a board with 4 rings
            Board = new GameBoard(4);


            //Add Pieces in starting locations
            Player1Pieces = new List<Piece>();
            Player2Pieces = new List<Piece>();

            for (int i = 0; i < 6; i++)
            {
                if (i < 3)
                {
                    Player1Pieces.Add(new Piece(1, new Coordinates(i, 0, 0)));
                }
                else
                {
                    Player2Pieces.Add(new Piece(2, new Coordinates(i, 0, 0)));
                }
            }

            CurrentPlayer = 1;

            //PlayGame();
        }

        public int PlayGame()
        {
            while(GetVictor() == 0)
            {
                //Console.WriteLine("Player " + CurrentPlayer + "'s Turn");
                if (CurrentPlayer == 1)
                {
                    PlayerTurn(Player1Pieces);
                }
                else
                {
                    PlayerTurn(Player2Pieces);
                }

                CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
            }
            return GetVictor();
        }

        public int GetVictor()
        {
            if (Player1Score >= 15)
            {
                return 1;
            }
            if (Player2Score >= 15)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public void PlayerTurn(List<Piece> playerPieces)
        {
            
            foreach(var piece in playerPieces)
            {
                piece.WaitingForMove = true;
                //Coordinates location = piece.CurrentLocation;
                //BoardSpace currentSpace = Board.Spaces[location.Section][location.Row][location.Position];

                //foreach(var adjacentSpace in currentSpace.AdjacentSpaces)
                //{
                //    var target = GameObject.Find("Space." + adjacentSpace.Section + "." + adjacentSpace.Row + "." + adjacentSpace.Position);
                //    target.GetComponent<MeshCollider>().enabled = true;
                //}


                //GetPlayerMove(); 

                //PausedForUser = true;
                //while (PausedForUser) { }

            }
            while (playerPieces.Any(p => p.WaitingForMove)) { };

        }

        public void PieceSelect(GameObject seletedSpace)
        {
            var pieceNumber = (int)Char.GetNumericValue(seletedSpace.name[seletedSpace.name.Length - 1]);
            var piece = CurrentPlayer == 1 ? Player1Pieces[pieceNumber] : Player2Pieces[pieceNumber];

            Coordinates location = piece.CurrentLocation;
            BoardSpace currentSpace = Board.Spaces[location.Section][location.Row][location.Position];

            foreach (var adjacentSpace in currentSpace.AdjacentSpaces)
            {
                var target = GameObject.Find("Space." + adjacentSpace.Section + "." + adjacentSpace.Row + "." + adjacentSpace.Position);
                target.GetComponent<MeshCollider>().enabled = true;
            }
        }

    }
}
