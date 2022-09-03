using System;
using System.Linq;
using System.Collections.Generic;

namespace BoardGame
{
    public class TicTacToePiece : Piece
    {
        static protected Dictionary<Char, bool> symbols = new Dictionary<Char, bool> { ['O'] = false, ['X'] = false };

        public readonly Char symbol;

        static public Char[] GetAvailable()
        {
            return symbols.Where(s => s.Value == false).ToDictionary(s => s.Key, s => s.Value).Keys.ToArray();
        }

        public TicTacToePiece(Char symbol)
        {
            this.symbol = symbol;
            symbols[symbol] = true;
        }

        public override string ToString()
        {
            return symbol.ToString();
        }
    }
}