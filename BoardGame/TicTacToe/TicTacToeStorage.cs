using System;
using System.IO;

namespace BoardGame
{
    public class TicTacToeStorage : Storage
    {
        protected override string FILENAME
        {
            get => "tictactoe.txt";
        }

        protected override Move ParseLine(String line)
        {
            int row, col;
            String[] slices = line.Split(",");

            if (slices.Length == 3 && int.TryParse(slices[0], out row) && int.TryParse(slices[1], out col))
            {
                Piece piece = new TicTacToePiece(symbol: slices[2].ToCharArray()[0]);
                return new Move(row, col, piece);
            }

            throw new InvalidDataException();    
        }
    }
}