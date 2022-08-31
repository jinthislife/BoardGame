using System;

namespace BoardGame
{
    public abstract class Piece
    {
    }

    public class ColorPiece : Piece
    {
        private String color;

        public ColorPiece(String color)
        {
            this.color = color;
        }

        public override string ToString()
        {
            return color.ToString();
        }
    }
}
