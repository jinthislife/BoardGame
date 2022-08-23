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
     
            do
            {
                Console.Write(">> ");
                string cmd = Console.ReadLine();
                string[] cmdSlices = cmd.Split(' ');

                switch (cmdSlices[0])
                {
                    case "undo":
                        
                    case "redo":
                        
                    case "place":
                        return cmd;
                    default:
                        break;
                }
            } while (true);
        }
    }
}

