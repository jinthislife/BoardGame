using System;
using BoardGame;

namespace BoardGame
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, Piece piece) : base(name, piece)
        {
        }

        public override string play()
        {
            Console.Write(">> ");
            string cmd = Console.ReadLine();

            return cmd;
        }
    }
}
