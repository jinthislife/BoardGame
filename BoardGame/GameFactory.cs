using System;
using System.ComponentModel;

namespace BoardGame
{
    enum Games
    {
        TicTacToe = 1,
        Othello
    }

    abstract class GameFactory
    {
        public abstract Board CreateBoard();
        public abstract Piece CreatePiece(string playername);
        public abstract Storage CreateStorage();
        public abstract HelpSystem CreateHelpSystem();

        public static GameFactory getUserSelection()
        {
            do
            {
                Console.WriteLine("\nSelect a game to play");

                foreach (Games game in Enum.GetValues(typeof(Games)))
                {
                    Console.WriteLine($"{(uint)game}. {Enum.GetName(typeof(Games), game)}");
                }
                Console.Write(">> ");

                int selectedGame;
                int.TryParse(Console.ReadLine(), out selectedGame);

                switch ((Games)selectedGame)
                {
                    case Games.TicTacToe:
                        return new TicTacToeFactory();
                    case Games.Othello:
                    default:
                        Console.WriteLine("Currently only TicTacToe is available.");
                        break;
                }
            } while (true);
        }
    }

}