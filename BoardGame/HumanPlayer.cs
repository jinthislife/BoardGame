using System;

namespace BoardGame
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(Piece piece, string name) : base(piece, name)
        {
        }

        public override string play(Board board)
        {
            Console.Write(">> ");
            string cmd = Console.ReadLine();

            return cmd;
        }
    }
}
