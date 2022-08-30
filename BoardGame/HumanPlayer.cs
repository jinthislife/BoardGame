using System;

namespace BoardGame
{
    public class HumanPlayer : Player
    {
        // name taking process, save and restore add complexity, cannot be restored in the middle of the game
        public HumanPlayer(int Id, Piece piece) : base(Id, piece)
        {
        }

        public override string Play(Board board)
        {
            Console.WriteLine($"\n{this.ToString()}, your turn!");
            Console.Write(">> ");
            string cmd = Console.ReadLine();

            return cmd;
        }

        public override string ToString()
        {
            return ($"Player-{Id}");
        }
    }
}
