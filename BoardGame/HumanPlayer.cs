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

        //public override void makeMove(MoveTracker moveTracker)
        public override void makeMove(Board board)
        {
            //Console.WriteLine($"{name}, your turn!");
            //bool isValidMove = false;
            //do
            //{
            //    Console.Write(">> ");
            //    string cmd = Console.ReadLine();
            //    string[] cmdSlices = cmd.Split(' '); //QQ name
            //    if (cmdSlices.Length != 3)
            //    {
            //        throw new InvalidProgramException(); //QQ right exception?
            //    }
            //    int row = int.Parse(cmdSlices[1]);
            //    int col = int.Parse(cmdSlices[2]);
            //    Console.WriteLine($"make move {row} {col}");
            //    // QQ HOW to check validity of move
            //    // QQ retain cycle?
            //    Move move = new Move(row: row, col: col, player: this);
            //    board.placeMove(move);
            //    //isValidMove = moveTracker.addMove(move);
            //} while (!isValidMove);
        }
    }
}

