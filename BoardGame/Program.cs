using System;
using System.ComponentModel;

namespace BoardGame
{
    enum Games
    {
        TicTacToe = 1,
        Othello
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***********************************************");
            Console.WriteLine("*            Welcome to BoardGame             *");
            Console.WriteLine("***********************************************");
            GameFactory factory = selectGame();
            Game game = new Game(factory);
            game.run();

        }

        static GameFactory selectGame()
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
                int.TryParse(Console.ReadLine(), out selectedGame);

            } while (selectedGame > Enum.GetValues(typeof(Games)).Length);

            switch ((Games)selectedGame)
            {
                case Games.TicTacToe:
                    return new TicTacToeFactory();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}

