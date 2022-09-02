using System;
using System.Collections.Generic;

namespace BoardGame
{
    public abstract class MoveStrategy
    {
        protected Board board;
        protected MoveStrategy(Board board)
        {
            this.board = board;
        }

        public abstract (int, int) SelectPosition();
    }
}

