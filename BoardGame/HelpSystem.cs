using System;

namespace BoardGame
{
    public abstract class HelpSystem
    {
        //abstract public void displayIntro();

        // template method
        public void DisplayManual()
        {
            Console.WriteLine("General commands manual");
        }

        public int SelectGameMode()
        {
            int mode;
            do
            {
                Console.WriteLine("Please select game mode");
                Console.WriteLine("1. Human vs Human");
                Console.WriteLine("2. Human vs AI");
                Console.Write(">> ");
                int.TryParse(Console.ReadLine(), out mode);
            } while (mode != 1 && mode != 2);

            Console.WriteLine($"Mode {mode} chosen.");
            return mode;
        }
    }
}

