using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace BoardGame
{
    public abstract class Piece
    {
        public Piece()
        {

        }
    }

    public class ColorPiece : Piece
    {
        protected Color color;

    }

    public class SymbolPiece : Piece
    {
        static protected Dictionary<Char, bool>  symbols = new Dictionary<Char, bool>{ ['O'] = false, ['X'] = false };

        public Char symbol;//QQ"

        static public Char[] getAvailableSymbols()
        {
            return symbols.Where(s => s.Value == false).ToDictionary(s => s.Key, s => s.Value).Keys.ToArray();
        }

        public SymbolPiece(Char symbol)
        {
            this.symbol = symbol;
            symbols[symbol] = true;
        }

    }
}

