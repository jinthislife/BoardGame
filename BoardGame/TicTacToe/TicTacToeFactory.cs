using System;
using System.Linq;

namespace BoardGame
{
    public class TicTacToeFactory : GameFactory
    {
        public override Board CreateBoard()
        {
            return new TicTacToeBoard(width: 3, height: 3);
        }

        public override Storage CreateStorage()
        {
            return new TicTacToeStorage();
        }

        public override HelpSystem CreateHelpSystem()
        {
            return new TicTacToeHelpSystem();
        }

        public override Piece CreatePiece()
        {
            Char[] availableSymbols = SymbolPiece.GetAvailable();

            if (availableSymbols.Length == 0) {
                throw new ArgumentOutOfRangeException();
            }

            return new SymbolPiece(symbol: availableSymbols[0]);
        }
    }
}