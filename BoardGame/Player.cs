using System;
using System.Xml.Linq;

namespace BoardGame
{
    public abstract class Player
    {
        public Piece Piece { get; }
        public String Name { get; }

        protected Player(Piece piece, String name)
        {
            this.Piece = piece;
            this.Name = name;
        }

        public abstract string Play(Board board);

        public override string ToString()
        {
            return ($"{Name}");
        }
    }
}

