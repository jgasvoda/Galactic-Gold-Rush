using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.GameEngine
{
    public sealed class GameManager
    {
        private GameBoard Board;
        private Player CurrentPlayer;

        private Player Player1;
        private Player Player2;

        private static readonly Lazy<GameManager>
        lazy =
        new Lazy<GameManager>
            (() => new GameManager());

        public static GameManager Instance { get { return lazy.Value; } }

        private GameManager()
        {
            CreateGame();
        }

        
        private void CreateGame()
        {
            //Make a board with 4 rings
            Board = new GameBoard(4);

            //Add Pieces in starting locations
            Player1 = new Player(1, "Blue");
            Player2 = new Player(2, "Red");
            for (int i = 0; i < 6; i++)
            {
                if (i < 3)
                {
                    Player1.AddPiece(new Piece(1, new Coordinates(i, 0, 0)));
                }
                else
                {
                    Player2.AddPiece(new Piece(2, new Coordinates(i, 0, 0)));
                }
            }

            CurrentPlayer = Player1;
            StartPlayerTurn();
        }

        public int GetVictor()
        {
            if (Player1.Score >= 15)
            {
                return 1;
            }
            if (Player2.Score >= 15)
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
            CurrentPlayer.StartTurn();
            for (int i = 0; i < 3; i++)
            {
                var target = GameObject.Find(CurrentPlayer.TeamColor + i);
                target.GetComponent<CircleCollider2D>().enabled = true;
            }
        }
        
        private void UpdatePlayerTurn()
        {
            CurrentPlayer.SelectedPiece.WaitingForMove = false;
            CurrentPlayer.DeselectPiece();

            if (CurrentPlayer.TurnOver())
            {
                if(CurrentPlayer.Team == 1)
                {
                    CurrentPlayer = Player2;
                }
                else
                {
                    CurrentPlayer = Player1;
                }

                StartPlayerTurn();
            }
        }

        public void PieceSelect(GameObject seletedObject)
        {
            //Validate piece can be clicked
            if (CurrentPlayer.SelectedPiece == null && seletedObject.name.Contains(CurrentPlayer.TeamColor))
            {
                int selection = (int)char.GetNumericValue(seletedObject.name[seletedObject.name.Length - 1]);
                CurrentPlayer.SelectPiece(selection);

                Coordinates location = CurrentPlayer.SelectedPiece.CurrentLocation;
                BoardSpace currentSpace = Board.Spaces[location.Section][location.Row][location.Position];

                //Enable neighboring spaces for selection
                foreach (var adjacentSpace in currentSpace.AdjacentSpaces)
                {
                    GameObject target = GameObject.Find("Space." + adjacentSpace.Section + "." + adjacentSpace.Row + "." + adjacentSpace.Position);
                    target.GetComponent<PolygonCollider2D>().enabled = true;
                }
            }
        }

        public void SpaceSelect(GameObject selectedObject)
        {
            //Disable board spaces
            Coordinates currentLocation = CurrentPlayer.SelectedPiece.CurrentLocation;
            BoardSpace currentBoardSpace = Board.Spaces[currentLocation.Section][currentLocation.Row][currentLocation.Position];
            foreach (var adjacentSpace in currentBoardSpace.AdjacentSpaces)
            {
                GameObject target = GameObject.Find("Space." + adjacentSpace.Section + "." + adjacentSpace.Row + "." + adjacentSpace.Position);
                target.GetComponent<PolygonCollider2D>().enabled = false;
            }

            //Move selectedPiece to new location
            GameObject currentPiece = GameObject.Find(CurrentPlayer.TeamColor + CurrentPlayer.GetPieceNumber());
            currentPiece.gameObject.transform.localPosition = selectedObject.transform.localPosition;

            //Disable selectedPiece for remainder of turn
            currentPiece.GetComponent<CircleCollider2D>().enabled = false;

            //Update GameBoard
            Coordinates newLocation = new Coordinates(
                (int)char.GetNumericValue(selectedObject.name[6]),
                (int)char.GetNumericValue(selectedObject.name[8]),
                (int)char.GetNumericValue(selectedObject.name[10])
            );
            CurrentPlayer.SelectedPiece.Move(newLocation);

            UpdatePlayerTurn();
        }
    }
}
