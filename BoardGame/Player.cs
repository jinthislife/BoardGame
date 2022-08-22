using System;
namespace BoardGame
{
    public abstract class Player
    {
        public Piece piece;
        protected String name;

        public Player(String name, Piece piece)
        {
            this.name = name;
            this.piece = piece;
        }

        public abstract void makeMove(MoveTracker moveTracker);

    }
}

