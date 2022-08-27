using System;
using System.Threading;

namespace BoardGame
{
    public class AIPlayer : Player
    {
        public AIPlayer(Piece piece, string name = "AI") : base(piece, name)
        {
        }

        public override string play(Board board)
        {
            Thread.Sleep(3000);
            int[] loc = board.getEmptyLoc();
            Console.Write($"AI placed {loc[0]} {loc[1]}");
            return loc == null ? "" : $"place {loc[0]} {loc[1]}";
        }

    }
}

