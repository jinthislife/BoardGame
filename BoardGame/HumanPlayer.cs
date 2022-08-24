using System;
namespace BoardGame
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, Piece piece) : base(name, piece)
        {
        }

        public override string generateCommand()
        {
            Console.WriteLine($"{name}, your turn!");
            Console.Write(">> ");
            string cmd = Console.ReadLine();

            return cmd;
        }
    }
}

