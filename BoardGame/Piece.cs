using System;
//using System.Collections.Generic;
//using System.Linq;


namespace BoardGame
{
    public abstract class Piece
    {
        //protected Piece()
        //{

        //}
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
