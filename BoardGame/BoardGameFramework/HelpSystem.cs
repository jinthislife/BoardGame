using System;

namespace BoardGame
{
    public class HelpSystem
    {
        public void DisplayManual()
        {
            Console.WriteLine("\n                 General Commands Manual\n");
            Console.WriteLine("\nThe following commands are available:\n");
            Console.WriteLine("move: used to place a move specifying row and column number");
            Console.WriteLine("      For example, to place a piece on the top left corner is.");
            Console.WriteLine("      >> move 0 0 \n");
            Console.WriteLine("save: used to save game state to file system\n");
            Console.WriteLine("load: used to restore previously saved state to play from the restore state\n");
            Console.WriteLine("undo: used to go to the previous state\n");
            Console.WriteLine("redo: used to go to the next state from present state\n");

            

            Console.WriteLine("help: used for open General Commands Manual\n");

        }

        public int SelectGameMode()
        {
            int mode;
            string modeStr;
            do
            {
                Console.WriteLine("Please select a game mode");
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