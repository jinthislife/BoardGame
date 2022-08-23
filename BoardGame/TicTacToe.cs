using System;
using System.Threading;
using System.Xml.Linq;

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

            moveTracker = new MoveTracker();
        }

        public override void run()
        {
            board.render();

            while (!gameFinished)
            {
                currentPlayer = changeTurns();
                //currentPlayer.makeMove(board);
                string input = currentPlayer.generateCommand();

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
                        break;
                    case string command when command.StartsWith("place"):
                        int r, c;
                        String[] cmdSlices = input.Split(' ');

                        if (int.TryParse(cmdSlices[1], out r) && int.TryParse(cmdSlices[2], out c))
                        {
                            Console.WriteLine($"r: {r}, c: {c}");
                            //Move move = new Move(r, c, currentPlayer, moveTracker, board);
                            Move move = new Move(r, c, currentPlayer);
                            cmd = new PlaceCommand(move, moveTracker, board);
                            cmd.Execute();
                            //move.Execute();
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid Command");
                        break;
                }

                Thread.Sleep(500);
            }

            Console.WriteLine("Game Finished");
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
            curPlayerID = (curPlayerID + 1) % 2;
            return players[curPlayerID];
        }

        protected override bool checkWin(Move[,] moves, Move latest)
        {
            // check col
            for (int col=0; col<3; col++)
            {
                Console.WriteLine($"col: {col}, latest.row {latest.row}");
                //Console.WriteLine($"player: {moves[latest.row, col].player}, latestPlayer {latest.player}");

                if (moves[latest.row, col] == null)
                {
                    return false;
                }
                else if (moves[latest.row, col].player != latest.player)
                {
                    return false;
                }
                else if (col == 2)
                {
                    return true;
                }
            }

            // check row
            for (int row = 0; row < 3; row++)
            {
                if (moves[row, latest.col] != null)
                {
                    return false;
                }
                else if (moves[row, latest.col].player != latest.player)
                {
                    return false;
                }
                else if ( row == 2)
                {
                    return true;
                }
            }

            // check diag
            if (latest.row == latest.col)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (moves[i, i] != null)
                    {
                        return false;
                    }
                    else if (moves[i, i].player != latest.player)
                    {
                        return false;
                    } else if (i == 2)
                    {
                        return true;
                    }

                }
            }

            // check anti-diagonal
            if (latest.row + latest.col == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (moves[i, 2 - i] != null)
                    {
                        return false;
                    } else if (moves[i, 2 - i].player != latest.player)
                    {
                        return false;
                    }
                    else if ( i == 2)
                    {
                        return true;
                    }
                }
            }

            //check draw??

            return false;
        }
    }
}

