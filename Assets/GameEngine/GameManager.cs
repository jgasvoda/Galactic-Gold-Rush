using GameEngine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGR_Game_Engine
{
    public sealed class GameManager
    {
        private GameBoard Board;
        private int CurrentPlayer;
        private int SelectedPiece;

        private List<Piece> Player1Pieces;
        private int Player1Score;

        private List<Piece> Player2Pieces;
        private int Player2Score;

        private static readonly Lazy<GameManager>
        lazy =
        new Lazy<GameManager>
            (() => new GameManager());

        public static GameManager Instance { get { return lazy.Value; } }

        private GameManager()
        {
            CreateGame();
        }

        //void Start()
        //{

        //}

        //// Update is called once per frame
        //void Update()
        //{

        //}
        
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
            StartPlayerTurn();
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

        private void StartPlayerTurn()
        {
            List<Piece> playerPieces = CurrentPlayer == 1 ? Player1Pieces : Player2Pieces;
            string teamColor = CurrentPlayer == 1 ? "Blue" : "Red";

            for (int i = 0; i < 3; i++)
            {
                playerPieces[i].WaitingForMove = true;

                var target = GameObject.Find(teamColor + i);
                target.GetComponent<CircleCollider2D>().enabled = true;
            }

            SelectedPiece = -1;
        }
        
        private void UpdatePlayerTurn(List<Piece> playerPieces)
        {
            playerPieces[SelectedPiece].WaitingForMove = false;

            if(!playerPieces.Any(p => p.WaitingForMove))
            {
                CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
                StartPlayerTurn();
            }
            else
            {
                SelectedPiece = -1;
            }
        }

        public void PieceSelect(GameObject seletedObject)
        {
            string teamColor = CurrentPlayer == 1 ? "Blue" : "Red";
            if (SelectedPiece == -1 && seletedObject.name.Contains(teamColor))
            {
                SelectedPiece = (int)char.GetNumericValue(seletedObject.name[seletedObject.name.Length - 1]);
                Piece gamePiece = CurrentPlayer == 1 ? Player1Pieces[SelectedPiece] : Player2Pieces[SelectedPiece];

                Coordinates location = gamePiece.CurrentLocation;
                BoardSpace currentSpace = Board.Spaces[location.Section][location.Row][location.Position];

                foreach (var adjacentSpace in currentSpace.AdjacentSpaces)
                {
                    var target = GameObject.Find("Space." + adjacentSpace.Section + "." + adjacentSpace.Row + "." + adjacentSpace.Position);
                    target.GetComponent<PolygonCollider2D>().enabled = true;
                }
            }
        }

        public void SpaceSelect(GameObject selectedObject)
        {
            //Disable board spaces
            List<Piece> playerPieces = CurrentPlayer == 1 ? Player1Pieces : Player2Pieces;
            Coordinates currentLocation = playerPieces[SelectedPiece].CurrentLocation;
            BoardSpace currentBoardSpace = Board.Spaces[currentLocation.Section][currentLocation.Row][currentLocation.Position];
            foreach (var adjacentSpace in currentBoardSpace.AdjacentSpaces)
            {
                GameObject target = GameObject.Find("Space." + adjacentSpace.Section + "." + adjacentSpace.Row + "." + adjacentSpace.Position);
                target.GetComponent<PolygonCollider2D>().enabled = false;
            }

            //Move selectedPiece to new location
            string teamColor = CurrentPlayer == 1 ? "Blue" : "Red";
            GameObject currentPiece = GameObject.Find(teamColor + SelectedPiece);
            currentPiece.gameObject.transform.localPosition = selectedObject.transform.localPosition;

            //Disable selectedPiece
            currentPiece.GetComponent<CircleCollider2D>().enabled = false;

            //Update GameBoard
            var newLocation = new Coordinates(
                (int)char.GetNumericValue(selectedObject.name[6]),
                (int)char.GetNumericValue(selectedObject.name[8]),
                (int)char.GetNumericValue(selectedObject.name[10])
            );
            playerPieces[SelectedPiece].Move(newLocation);

            UpdatePlayerTurn(playerPieces);
        }
    }
}
