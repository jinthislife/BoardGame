using System;
//using System.Collections.Generic;
//using System.Linq;


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
        protected String color;

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
