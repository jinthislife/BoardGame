using System;
namespace BoardGame
{
    public class AIPlayer : Player
    {
        public AIPlayer(Piece piece, string name = "AI") : base(name, piece)
        {
        }

        
        public override void makeMove(MoveTracker moveTracker)
        {
            Console.WriteLine($"{name} made a move");
            //return new Move();
        }
    }
}

