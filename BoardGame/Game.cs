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
        private AIMoveStrategy strategy;
        private Player[] players;
        private Player currentPlayer;
        private int curPlayerID = 1;
        private bool gameFinished = false;

        public Game(GameFactory factory)
        {
            board = factory.CreateBoard();
            helpSystem = factory.CreateHelpSystem();
            int gameMode = helpSystem.SelectGameMode();
            if (gameMode == 2)
            {
                strategy = factory.CreateEasyMoveStrategy(board);
            }
            else
            {
                strategy = factory.CreateNormalMoveStrategy(board);
            }
            storage = factory.CreateStorage();
            players = factory.CreatePlayers(gameMode, strategy);

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
                        storage.Save(board.GetStates());
                        break;
                    case "load":
                        if (storage.ExistsPreviousState())
                        {
                            board.LoadStates(storage.Load());
                            moveTracker.Clear();
                            break;
                        }
                        Console.WriteLine("No saved state.");
                        break;
                    case "help":
                        helpSystem.DisplayManual();
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
                            PlaceCommand placeCmd = new PlaceCommand(move, board);
                            placeCmd.Execute();
                            moveTracker.Insert(placeCmd);
                            CheckGameState(move);
                            break;
                        }
                        Console.WriteLine("Please try again!");
                        break;
                    default:
                        Console.WriteLine("Invalid Command");
                        break;
                }
            }
        }

        private Player ChangeTurns()
        {
            curPlayerID = (curPlayerID + 1) % 2;
            return players[curPlayerID];
        }

        private bool LoadFromSavedState()
        {
            Console.WriteLine("\nPreviously saved state found. If you want to start from there, please type 'y'. If you want a fresh start, plase type any key.");
            Console.Write(">> ");
            ConsoleKey input = Console.ReadKey().Key;
            if (input == ConsoleKey.Y)
            {
                board.LoadStates(storage.Load());

                if ((board.getOccupiedCount() % 2) == 1) currentPlayer = ChangeTurns();
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