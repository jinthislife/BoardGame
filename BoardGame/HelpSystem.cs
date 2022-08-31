using System;

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
                Console.WriteLine("2. Human vs AI");
                Console.Write(">> ");
                int.TryParse(Console.ReadLine(), out mode);
                modeStr = mode == 1 ? "Human vs Human" : "Human vs AI";
            } while (mode != 1 && mode != 2);

            Console.WriteLine($"{modeStr} mode chosen.");
            return mode;
        }
    }
}