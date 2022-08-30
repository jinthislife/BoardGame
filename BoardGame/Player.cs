using System;
using System.Xml.Linq;

namespace BoardGame
{
    public abstract class Player
    {
        public int Id { get; }
        public Piece Piece { get; }

        protected Player(int Id, Piece piece)
        {
            this.Id = Id;
            this.Piece = piece;
        }

        public abstract string Play(Board board);
    }
}

