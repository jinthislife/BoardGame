using System;
using System.Threading;

namespace BoardGame
{
    public class TicTacToe : Game
    {
        public TicTacToe()
        {
        }

        public override void initialize()
        {
            displayIntro();
            int mode = SelectGameMode();
            createPlayers(mode);
            board = createBoard(width: 3, height: 3);
            storage = new Storage();
            moveTracker = new MoveTracker();
        }

        public override void run()
        {
            board.render();
            currentPlayer = changeTurns();

            while (!gameFinished)
            {
                string input = currentPlayer.play(board);

                ICommand cmd;
              
                switch (input)
                {
                    case "undo":
                        moveTracker.Undo();
                        break;
                    case "redo":
                        moveTracker.Redo();
                        break;
                    case "save":
                        cmd = new SaveCommand(storage, board);
                        cmd.Execute();
                        break;
                    case "load":
                        cmd = new LoadCommand(storage, board);
                        cmd.Execute();
                        break;
                    case "help":
                        cmd = new HelpCommand();
                        cmd.Execute();
                        break;
                    case "quit":
                        break;
                    case string movestr when movestr.StartsWith("place"):
                        Move move = generateMoveFrom(movestr);
                        if (move != null) { 
                            cmd = new PlaceCommand(move, moveTracker, board);
                            cmd.Execute();

                            if (board.winningLineExists(move))
                            {
                                gameFinished = true;
                            }
                            else
                            {
                                currentPlayer = changeTurns();
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid Command");
                        break;
                }
            }

            Console.WriteLine($"\n{currentPlayer.ToString()} won the game!");
        }

        private Move generateMoveFrom(String moveStr)
        {
            int r, c;
            String[] cmdSlices = moveStr.Split(' ');

            if (cmdSlices.Length == 3 &&
                int.TryParse(cmdSlices[1], out r) &&
                int.TryParse(cmdSlices[2], out c) &&
                board.checkValidMove(x: r, y: c)
             )
            {
                return new Move(r, c, currentPlayer);
            }

            Console.WriteLine("Failed to place your move. Please try again!");
            return null; // QQ ok?
        }

        protected override void displayIntro()
        {
            Console.WriteLine("Welcome to TicTacToe");
        }

        protected override Piece createPieceForPlayer(string name)
        {
            SymbolPiece piece;

            Char[] availableSymbols = SymbolPiece.getAvailableSymbols();
            if (availableSymbols.Length > 1 && name != "AI")
            {
                Console.Write($"Enter symbol for Player {name} : ");
                char symbol = Console.ReadKey().KeyChar;
                piece = new SymbolPiece(symbol: symbol);
            }
            else
            {
                piece = new SymbolPiece(symbol: availableSymbols[0]);
            }
            
            Console.WriteLine($"\n{name}'s piece: {piece.symbol}"); //QQ accessing piece symbol?
            return piece;
        }

        protected override Player createHumanPlayer(int playerNumbers) // QQ: factory method vs template
        {      
            Console.Write($"Enter name for Player {playerNumbers + 1}: ");

            String name = Console.ReadLine();
            Piece piece = createPieceForPlayer(name);

            return new HumanPlayer(name: name, piece: piece);
        }

        protected override Player changeTurns()
        {
            Thread.Sleep(2000);

            curPlayerID = (curPlayerID + 1) % 2;
            Console.WriteLine($"\n{players[curPlayerID].ToString()}, your turn!");
            return players[curPlayerID];
        }

        protected override bool checkWin()
        {
            throw new NotImplementedException();
        }
    }
}

