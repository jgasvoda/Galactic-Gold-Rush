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
        private List<Asteroid> ControlledAsteroids { get; set; }

        public Player(int team, string color)
        {
            Team = team;
            TeamColor = color;
            Pieces = new List<Piece>();
            ControlledAsteroids = new List<Asteroid>();
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

            ControlledAsteroids = ControlledAsteroids.Where(a => a.ControllingPlayer == Team).ToList();
            //Mine claimed asteroids to accumlate gold
            foreach (var asteroid in ControlledAsteroids)
            {
                asteroid.MineGold(1);
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

        public void ClaimAsteroid(Asteroid asteroid)
        {
            ControlledAsteroids.Add(asteroid);
        }

        public bool TurnIsOver()
        {
            return !Pieces.Any(p => p.WaitingForMove);
        }

    }
}
