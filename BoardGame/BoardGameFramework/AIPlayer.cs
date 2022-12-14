using System;
using System.Threading;

namespace BoardGame
{
    public class AIPlayer : Player
    {
        private readonly MoveStrategy strategy;

        public AIPlayer(int Id, Piece piece, MoveStrategy strategy) : base(Id, piece)
        {
            this.strategy = strategy;
        }

        public override string Play(Board board)
        {
            Console.WriteLine($"\nIt's AI Player's turn now!");
            Thread.Sleep(1000);
            
            string cmdstr = "";

            try
            {   // using interface to MoveStrategy, AIPlayer can access to a concrete implementation
                (int, int) loc = strategy.SelectPosition();

                Console.Write($"AI placed {loc.Item1} {loc.Item2}");
                cmdstr = $"move {loc.Item1} {loc.Item2}";
            }
            catch
            {
                Console.WriteLine("AI failed to move");
            }

            return cmdstr;
        }

        public override string ToString()
        {
            return ($"AI Player");
        }
    }
}