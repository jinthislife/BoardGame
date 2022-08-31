using System;
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
            if (storage.ExistsPreviousState() == false || LoadFromSavedState() == false)
            {
                board.Render();
            }
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
                        String[] cmdSlices = movestr.Split(' ');
                        int r, c;
                        if (cmdSlices.Length == 3 &&
                            int.TryParse(cmdSlices[1], out r) &&
                            int.TryParse(cmdSlices[2], out c) &&
                            board.CheckAvailableLoc(x: r, y: c)
                         )
                        {
                            Move move = new Move(r, c, currentPlayer.Piece);
                            PlaceCommand place = new PlaceCommand(move, board);
                            place.Execute();
                            moveTracker.Insert(place);
                            CheckGameState(move);
                            break;
                        }
                        Console.WriteLine("Failed to place your move. Please try again!");
                        break;
                    default:
                        Console.WriteLine("Invalid Command");
                        break;
                }
            }
        }

        private Player[] CreatePlayers(int mode, Func<Piece> CreatePiece)
        {
            Player[] players = new Player[2];
            players[0] = new HumanPlayer(Id: 1, piece: CreatePiece());
            //players[1] = (mode == 1) ? new HumanPlayer(Id: 2, piece: CreatePiece()): new AIPlayer(Id: 2, piece: CreatePiece());

            if (mode == 1)
            {
                players[1] = new HumanPlayer(Id: 2, piece: CreatePiece());
            }
            else
            {
                players[1] = new AIPlayer(Id: 2, piece: CreatePiece());
            }

            Console.Write($"\n{players[0].ToString()} got '{players[0].Piece.ToString()}'. ");
            Console.WriteLine($"{players[1].ToString()} got '{players[1].Piece.ToString()}'.");
            return players;
        }

        private Player ChangeTurns()
        {
            Thread.Sleep(2000);

            curPlayerID = (curPlayerID + 1) % 2;
            return players[curPlayerID];
        }

        private bool LoadFromSavedState()
        {
            Console.WriteLine("\nIf you want to start from the previously saved state, please type 'y'. If you want a fresh start , plase type any key.");
            Console.Write(">> ");
            ConsoleKey input = Console.ReadKey().Key;
            if (input == ConsoleKey.Y)
            {
                LoadCommand load = new LoadCommand(storage, board);
                load.Execute();
                Thread.Sleep(1500);

                if ((board.CountOccupied % 2) == 1) currentPlayer = ChangeTurns();
                return true;
            }
            return false;
        }

        private void CheckGameState(Move latest)
        {
            switch (board.CheckState(latest))
            {
                case BoardGameState.Playing:
                    currentPlayer = ChangeTurns();
                    break;
                case BoardGameState.Won:
                    gameFinished = true;
                    Console.WriteLine($"\n{currentPlayer.ToString()} won the game!");
                    break;
                case BoardGameState.Draw:
                    gameFinished = true;
                    Console.WriteLine($"\nDraw, There is no winner!");
                    break;
            }
        }
    }
}