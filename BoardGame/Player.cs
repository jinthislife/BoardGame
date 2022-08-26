using System;
using System.Text.Json;

namespace BoardGame
{
    public abstract class Player
    //public class Player
    {
        public String name { get; set; }
        public object piece { get; set; }

        public Player(Piece piece, String name)
        {
            this.name = name;
            this.piece = piece;
        }

        //public abstract void makeMove(MoveTracker moveTracker);
        public abstract string play(Board board);


        public override string ToString()
        {
            return ($"{name}");
        }
    }
}

