using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace BoardGame
{
    class Game // QQ: abstract vs normal class
    {
        public Board board;
        public Storage storage;
        public MoveTracker moveTracker;
        public HelpSystem helpSystem;
        public Player[] players;
        protected Player currentPlayer;
        public int curPlayerID;
        protected bool gameFinished = false;

        public Game(GameFactory factory)
        {
            helpSystem = factory.CreateHelpSystem();
            board = factory.CreateBoard();
            storage = factory.CreateStorage();

            players = CreatePlayers(helpSystem.SelectGameMode(), factory.CreatePiece);
            moveTracker = new MoveTracker();
        }

        public void run()
        {
            //helpSystem.displayIntro();
            board.render();
            currentPlayer = changeTurns();

            while (!gameFinished)
            {
                string input = currentPlayer.play(board);

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
                    case "quit":
                        break;
                    case string movestr when movestr.StartsWith("place"):
                        Move move = moveFrom(movestr);
                        if (move != null)
                        {
                            PlaceCommand place = new PlaceCommand(move, moveTracker, board);
                            place.Execute();

                            if (board.checkWin(move))
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

        public Player[] CreatePlayers(int mode, Func<string, Piece> CreatePiece)
        { 
            Player CreateHumanPlayer(int playerNumber)
            {
                Console.Write($"Enter name for Player {playerNumber}: ");
                String name = Console.ReadLine();
                return new HumanPlayer(name: name, piece: CreatePiece(name));
            }

            Player[] players = new Player[2];
            players[0] = CreateHumanPlayer(1);
            players[1] = (mode == 1) ? CreateHumanPlayer(2) : new AIPlayer(piece: CreatePiece("AI"));
            Console.Write($"{players[0].name} got '{players[0].piece.ToString()}'. {players[1].name} got '{players[1].piece.ToString()}'");

            return players;
        }


        private Move moveFrom(String moveStr)
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

        protected Player changeTurns()
        {
            Thread.Sleep(2000);

            curPlayerID = (curPlayerID + 1) % 2;
            Console.WriteLine($"\n{players[curPlayerID].ToString()}, your turn!");
            return players[curPlayerID];
        }
    }
}