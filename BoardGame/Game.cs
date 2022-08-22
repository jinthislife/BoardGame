using System;
//using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace BoardGame
{
    enum Games
    {
        TicTacToe = 1,
        Othello
    }

    public abstract class Game : IObserver<MoveRecord>  // QQ: abstract vs normal class
    {
        //public Game()
        //{

        //}
        

        //protected abstract void initializeGame();
        public abstract void initializeGame();
        public abstract void run();
        protected abstract Player changeTurns();
        protected abstract void displayIntro();
        protected abstract bool checkWin(Move[,] moves, Move latest = null); // optional parm

        protected abstract Player createHumanPlayer(int playerNumbers);
        protected abstract Piece createPieceForPlayer(string name);
        public Board board;
        public MoveTracker moveTracker;
        protected bool gameFinished = false;
        //public virtual Player createHumanPlayer();
        protected abstract int boardWidth { get; }
        protected abstract int boardHeight { get; }

       
        protected Player currentPlayer;
        public int curPlayerID;
        //public List<Player> Players = new List<Player>();
        public Player[] players = { };

        public virtual Board createBoard(int width, int height)
        {
            return new Board(width: width, height: height);
        }



        // Let user select a game
        public static Game createGame() // QQ: Right place to declare func?
        {
            int selectedGame; //QQ : test case 0
            do
            {
                Console.WriteLine("Select a game to play");

                foreach (Games game in Enum.GetValues(typeof(Games)))
                {
                    Console.WriteLine($"{(uint)game}. {Enum.GetName(typeof(Games), game)}");
                }
                Console.Write(">> ");
                selectedGame = int.Parse(Console.ReadLine());

            } while (selectedGame > Enum.GetValues(typeof(Games)).Length);

            switch ((Games)selectedGame)
            {
                case Games.TicTacToe:
                    return new TicTacToe();
                default:
                    throw new InvalidEnumArgumentException();
            }

        }

        //protected abstract void createPlayers();
        public void createPlayers(int mode)
        {
            players = new Player[2];
            int playerNumbers = 0;
            Console.WriteLine($"Play Mode: {mode} Count: {playerNumbers + 1}");
            if (mode == 1)
            {
                // Create 2 Human Players
                while (playerNumbers < 2)
                {
                    Player p = createHumanPlayer(playerNumbers);
                    players[playerNumbers] = p;
                    playerNumbers++; //QQ
                }
            }
            else if (mode == 2)
            {
                // Create AI and Human players
                Player p = createHumanPlayer(playerNumbers);
                players[playerNumbers] = p;
                //Players.Add(p);

                Piece piece = createPieceForPlayer("AI");
                Player ai = new AIPlayer(piece: piece);
                players[playerNumbers++] = ai;
                //Players.Add(ai);
            }

        }
        

        // A template Method
        public int SelectGameMode()
        {
            int mode;
            String response;

            do
            {
                Console.WriteLine("Please select game mode");
                Console.WriteLine("1. Human vs Human");
                Console.WriteLine("2. Human vs AI");
                Console.Write(">> ");
                response = Console.ReadLine();

            } while (!int.TryParse(response, out mode));

            Console.WriteLine($"Mode {response} chosen.");
            return mode;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(MoveRecord record)
        {
            Console.WriteLine("Game Received Notification");
            gameFinished = checkWin(moves: record.moves, latest: record.latest);
        }
    }
}

