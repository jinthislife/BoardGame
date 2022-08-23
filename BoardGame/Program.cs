using System;
using System.ComponentModel;

namespace BoardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the BoardGame");

            Game game = Game.createGame();
            game.initialize();
            game.run();
        }
    }
}

