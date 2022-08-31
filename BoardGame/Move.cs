using System;

namespace BoardGame
{
    public class Move
    {
        public readonly int row;
        public readonly int col;
        public readonly Piece piece;

        public Move(int row, int col, Piece piece)
        {
            this.row = row;
            this.col = col;
            this.piece = piece;
        }

        public override string ToString()
        {
            return $"{row},{col},{piece.ToString()}";
        }
    }
}