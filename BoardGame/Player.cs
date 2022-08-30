using System;

namespace BoardGame
{
    public abstract class Player
    {
        public String name { get; set; } // QQ need to be properties?
        public Piece piece { get; set; }

        public Player(Piece piece, String name)
        {
            this.name = name;
            this.piece = piece;
        }

        public abstract string Play(Board board);

        public override string ToString()
        {
            return ($"{name}");
        }
    }
}

