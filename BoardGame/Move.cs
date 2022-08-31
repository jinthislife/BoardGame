using System;

namespace BoardGame
{
    public class Move
    {
        public readonly int locX;
        public readonly int locY;
        public readonly Piece piece;

        public Move(int x, int y, Piece piece)
        {
            this.locX = x;
            this.locY = y;
            this.piece = piece;
        }

        public override string ToString()
        {
            return $"{locX},{locY},{piece.ToString()}";
        }
    }
}