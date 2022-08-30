﻿using System;
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

            if (int.TryParse(slices[0], out row) && int.TryParse(slices[1], out col))
            {
                Piece piece;

                if (slices[4] == "BoardGame.SymbolPiece")
                {
                    piece = new SymbolPiece(symbol: slices[5].ToCharArray()[0]);
                }
                else
                {
                    piece = new ColorPiece(color: slices[5]); //delete this
                }
 
                return new Move(row, col, piece);
            }
            else
            {
                throw new InvalidDataException();
            }
        }
    }
}

