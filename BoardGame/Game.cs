using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace BoardGame
{
    class Game
    {
        private Board board;
        private Storage storage;
        private MoveTracker moveTracker;
        private HelpSystem helpSystem;
        private Player[] players;
        private Player currentPlayer;
        private int curPlayerID = 1;
        private bool gameFinished = false;

        public Game(GameFactory factory)
        {
            helpSystem = factory.CreateHelpSystem();
            board = factory.CreateBoard();
            storage = factory.CreateStorage();

            players = CreatePlayers(helpSystem.SelectGameMode(), factory.CreatePiece);
            moveTracker = new MoveTracker();
        }

        public void Run()
        {
            //helpSystem.displayIntro();
            board.Render();
            currentPlayer = ChangeTurns();

            while (!gameFinished)
            {
                string input = currentPlayer.Play(board);

                switch (input)
                {
                    case "undo":
                        moveTracker.Undo();
                        break;
                    case "redo":
                        moveTracker.Redo();
                        break;
                    case "save":
                        SaveCommand save = new SaveCommand(storage, board);
                        save.Execute();
                        break;
                    case "load":
                        LoadCommand load = new LoadCommand(storage, board);
                        load.Execute();
                        break;
                    case "help":
                        HelpCommand help = new HelpCommand(helpSystem);
                        help.Execute();
                        break;
                    case string movestr when movestr.StartsWith("place"):
                        Move move = MoveFrom(movestr);
                        if (move != null)
                        {
                            PlaceCommand place = new PlaceCommand(move, moveTracker, board);
                            place.Execute();

                            if (board.CheckWin(move))
                            {
                                gameFinished = true;
                            }
                            else
                            {
                                currentPlayer = ChangeTurns();
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

        private Player[] CreatePlayers(int mode, Func<string, Piece> CreatePiece)
        { 
            Player CreateHumanPlayer(int playerNumber)
            {         
                String name = "";
                while (name.Length <= 0)
                {
                    Console.Write($"Enter name for Player {playerNumber}: ");
                    name = Console.ReadLine();
                }

                return new HumanPlayer(name: name, piece: CreatePiece(name));
            }

            Player[] players = new Player[2];
            players[0] = CreateHumanPlayer(1);
            players[1] = (mode == 1) ? CreateHumanPlayer(2) : new AIPlayer(piece: CreatePiece("AI"));
            Console.Write($"\n{players[0].Name} got '{players[0].Piece.ToString()}'. {players[1].Name} got '{players[1].Piece.ToString()}'");

            return players;
        }

        private Player ChangeTurns()
        {
            Thread.Sleep(2000);

            curPlayerID = (curPlayerID + 1) % 2;
            //Console.WriteLine($"\n{players[curPlayerID].ToString()}, your turn!");
            return players[curPlayerID];
        }

        private Move MoveFrom(String moveStr)
        {
            int r, c;
            String[] cmdSlices = moveStr.Split(' ');

            if (cmdSlices.Length == 3 &&
                int.TryParse(cmdSlices[1], out r) &&
                int.TryParse(cmdSlices[2], out c) &&
                board.CheckValidMove(x: r, y: c)
             )
            {
                return new Move(r, c, currentPlayer);
            }

            Console.WriteLine("Failed to place your move. Please try again!");
            return null; // QQ ok?
        }
    }
}