using System;
using System.Threading;

namespace BoardGame
{
    class Game
    {
        private Board board;
        private Storage storage;
        private CommandTracker commandTracker;
        private HelpSystem helpSystem;
        private MoveStrategy strategy;
        private Player[] players;
        private Player currentPlayer;
        private int curPlayerID = 1;
        private bool gameFinished = false;

        public Game(GameFactory factory)
        {
            board = factory.CreateBoard();
            helpSystem = factory.CreateHelpSystem();

            int gameMode = helpSystem.SelectGameMode();
            storage = factory.CreateStorage();

            if (gameMode == 2)
                strategy = factory.CreateEasyMoveStrategy(board);
            else
                strategy = factory.CreateNormalMoveStrategy(board);

            players = factory.CreatePlayers(gameMode, strategy);

            commandTracker = new CommandTracker();
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
                        commandTracker.Undo();
                        break;
                    case "redo":
                        commandTracker.Redo();
                        break;
                    case "save":
                        storage.Save(board.GetStates());
                        break;
                    case "load":
                        if (storage.ExistsPreviousState())
                        {
                            board.LoadStates(storage.Load());
                            commandTracker.Clear();
                            break;
                        }
                        Console.WriteLine("No saved state.");
                        break;
                    case "help":
                        helpSystem.DisplayManual();
                        break;
                    case string movestr when movestr.StartsWith("move"):
                        String[] cmdSlices = movestr.Split(' ');
                        int r, c;
                        if (cmdSlices.Length == 3 &&
                            int.TryParse(cmdSlices[1], out r) &&
                            int.TryParse(cmdSlices[2], out c) &&
                            board.CheckIfEmpty(x: r, y: c)
                         )
                        {
                            Move move = new Move(r, c, currentPlayer.Piece);
                            MoveCommand cmd = new MoveCommand(move, board);
                            cmd.Execute();
                            commandTracker.Insert(cmd);
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