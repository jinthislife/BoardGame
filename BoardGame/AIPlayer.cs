using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections;

namespace BoardGame
{
    public class AIPlayer : Player
    {
        public AIPlayer(int Id, Piece piece) : base(Id, piece)
        {
        }

        public override string Play(Board board)
        {
            Console.WriteLine($"\nIt's AI Player's turn now!");
            Thread.Sleep(1000);
            
            string cliInput = "";
            List<(int, int)> locs = board.GetAvaliableLocs();

            if (locs.Count > 0)
            {
                var random = new Random();
                int index = random.Next(locs.Count);

                (int, int) loc = locs[index];

                Console.Write($"AI placed {loc.Item1} {loc.Item2}");
                cliInput = $"place {loc.Item1} {loc.Item2}";
            }

            return cliInput;
        }

        public override string ToString()
        {
            return ($"AI Player");
        }
    }
}