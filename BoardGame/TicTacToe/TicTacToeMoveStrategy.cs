using System;
using System.Collections.Generic;

namespace BoardGame
{
    public class TicTacToeEasyMoveStrategy : MoveStrategy
    {
        public TicTacToeEasyMoveStrategy(Board board) : base(board)
        {
        }

        public override (int, int) SelectPosition()
        {
            List<(int, int)> locs = board.GetEmptyPositions();

            // Easy level simply returns the first available move
            if (locs.Count == 0) throw new IndexOutOfRangeException();

            return locs[0];
        }
    }

    public class TicTacToeNormalMoveStrategy : MoveStrategy
    {
        public TicTacToeNormalMoveStrategy(Board board) : base(board)
        {
        }

        public override (int, int) SelectPosition()
        {
            List<(int, int)> locs = board.GetEmptyPositions();

            if (locs.Count == 0) throw new IndexOutOfRangeException();

            // Normal level returns random selected move from the avaiables
            var random = new Random();
            int index = random.Next(locs.Count);

            return locs[index];
        }
    }
}

