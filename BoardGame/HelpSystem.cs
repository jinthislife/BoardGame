using System;
using System.Security.Cryptography;

namespace BoardGame
{
    public abstract class HelpSystem
    {
        //abstract public void displayIntro();

        public void DisplayManual()
        {
            Console.WriteLine("General commands manual");
        }

        public int SelectGameMode()
        {
            int mode;
            string modeStr;
            do
            {
                Console.WriteLine("Please select game mode");
                Console.WriteLine("1. Human vs Human");
                Console.WriteLine("2. Human vs AI - Easy");
                Console.WriteLine("3. Human vs AI - Normal");

                Console.Write(">> ");
                int.TryParse(Console.ReadLine(), out mode);
                modeStr = mode == 1 ? "Human vs Human" : mode == 2 ? "Human vs AI - Easy" : "Human vs AI - Normal";
            } while (mode != 1 && mode != 2 && mode != 3);

            Console.WriteLine($"{modeStr} mode chosen.");
            return mode;
        }
    }
}