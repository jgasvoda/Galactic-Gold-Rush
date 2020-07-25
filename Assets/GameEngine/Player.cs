using System.Collections.Generic;
using System.Linq;

namespace Assets.GameEngine
{
    public class Player
    {
        public int Team { get; set; }
        public string TeamColor { get; set; }
        public int Score { get; set; }
        public Piece SelectedPiece;

        private List<Piece> Pieces { get; set; }

        public Player(int team, string color)
        {
            Team = team;
            TeamColor = color;
            Pieces = new List<Piece>();
        }

        public void AddPiece(Piece newPiece)
        {
            Pieces.Add(newPiece);
        }

        public void StartTurn()
        {
            foreach(var piece in Pieces)
            {
                piece.WaitingForMove = true;
            }
        }

        public void SelectPiece(int number)
        {
            SelectedPiece = Pieces[number];
        }

        public void DeselectPiece()
        {
            SelectedPiece = null;
        }

        public int GetPieceNumber()
        {
            return Pieces.IndexOf(SelectedPiece);
        }

        public bool TurnOver()
        {
            return !Pieces.Any(p => p.WaitingForMove);
        }

    }
}
