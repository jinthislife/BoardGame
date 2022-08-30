using System;
using System.IO;

namespace BoardGame
{
    public class TicTacToeStorage : Storage
    {
        public TicTacToeStorage()
        {
        }

        public override string FILENAME
        {
            get => "tictactoe.txt";
        }

        public override Move ParseLine(String line)
        {
            int row, col, isHuman;
            String[] slices = line.Split(",");

            if (int.TryParse(slices[0], out row) &&
                int.TryParse(slices[1], out col) &&
                int.TryParse(slices[2], out isHuman))
            {
                Piece piece;
                Player player;

                if (slices[4] == "BoardGame.SymbolPiece")
                {
                    piece = new SymbolPiece(symbol: slices[5].ToCharArray()[0]);
                }
                else
                {
                    piece = new ColorPiece(color: slices[5]); //delete this
                }

                if (isHuman == 0)
                {
                    player = new AIPlayer(piece, slices[2]);
                }
                else
                {
                    player = new HumanPlayer(piece, slices[2]);
                }
                return new Move(row, col, player);
            }
            else
            {
                throw new InvalidDataException();
            }
        }
    }
}

