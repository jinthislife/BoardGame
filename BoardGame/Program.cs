using System;
using System.ComponentModel;

namespace BoardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***********************************************");
            Console.WriteLine("*            Welcome to BoardGame             *");
            Console.WriteLine("***********************************************");

            GameFactory factory = GameFactory.getUserSelection();
            Game game = new Game(factory);
            game.run();
        }
    }
}
