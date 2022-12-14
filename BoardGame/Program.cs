using System;

namespace BoardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***********************************************");
            Console.WriteLine("*            Welcome to BoardGame             *");
            Console.WriteLine("***********************************************");

            GameFactory factory = GetUserSelection();
            Game game = new Game(factory);
            game.Run();
        }

        // Returns a concrete factory of game a user selected
        // Currently, only TicTacToe is available
        static GameFactory GetUserSelection()
        {
            do
            {
                Console.WriteLine("\nSelect a game to play");

                foreach (BoardGames game in Enum.GetValues(typeof(BoardGames)))
                {
                    Console.WriteLine($"{(uint)game}. {Enum.GetName(typeof(BoardGames), game)}");
                }
                Console.Write(">> ");

                int selectedGame;
                int.TryParse(Console.ReadLine(), out selectedGame);

                switch ((BoardGames)selectedGame)
                {
                    case BoardGames.TicTacToe:
                        Console.WriteLine("\nLet's paly TicTacToe!\n");
                        return new TicTacToeFactory();
                    case BoardGames.Othello:
                    default:
                        Console.WriteLine("Currently only TicTacToe is available.");
                        break;
                }
            } while (true);
        }
    }
}