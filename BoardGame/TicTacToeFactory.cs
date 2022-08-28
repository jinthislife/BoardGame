using System;
using System.Linq;

namespace BoardGame
{
    class TicTacToeFactory : GameFactory //public?
    {
        public override Board CreateBoard()
        {
            return new TicTacToeBoard(width: 3, height: 3);
        }

        public override Piece CreatePiece(string playername)
        {
            Char[] availableSymbols = SymbolPiece.getAvailable();
            char symbol = availableSymbols[0];

            if (availableSymbols.Length > 1)
            {
                do
                {
                    Console.Write($"{playername}, select your piece either 'O' or 'X' : ");
                    symbol = Console.ReadKey().KeyChar;
                    Console.Write("\n");
                } while (!availableSymbols.Contains(symbol));
            }
      
            return new SymbolPiece(symbol: symbol);
        }

        public override Storage CreateStorage()
        {
            return new TicTacToeStorage();
        }

        public override HelpSystem CreateHelpSystem()
        {
            return new TicTacToeHelpSystem();
        }
    }
}