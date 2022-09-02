using System;
using System.Collections.Generic;

namespace BoardGame
{
    public abstract class Board
    {
        protected int width;
        protected int height;
        protected Move[,] moves;

        // Some games need diffrent rendering and placement of pieces
        // even though they use a board with same width and height.
        // For example, Go requires that pieces are placed
        // on the intersection of lines rather than inside sqaures.
        // Therefore abstract methods below need to be implemented by derived classes.
        public abstract void Render();
        public abstract bool CheckWin(Move latest);
        public abstract bool CheckIfEmpty(int x, int y);
        public abstract List<(int, int)> GetEmptyPositions();
        public abstract int getOccupiedCount();

        protected Board(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Place(Move move)
        {
            moves[move.locX, move.locY] = move;
            Render();
        }

        public void Withdraw(Move move)
        {
            moves[move.locX, move.locY] = null;
            Render();
        }

        public BoardGameState CheckState(Move latest)
        {
            BoardGameState state = BoardGameState.Playing;
            if (CheckWin(latest))
                state = BoardGameState.Won;
            else if (GetEmptyPositions().Count == 0)
                state = BoardGameState.Draw;

            return state;
        }

        public void LoadStates(List<Move> movelist)
        {
            try
            {
                foreach (Move move in movelist)
                {
                    moves[move.locX, move.locY] = move;
                }
                Console.WriteLine("\nSuccessfully loaded from the saved state.");
            }
            catch
            {
                Console.WriteLine("\nFailed to load.");
            }
            Render();
        }

        public List<Move> GetStates()
        {
            List<Move> movelist = new List<Move>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (moves[x, y] != null) movelist.Add(moves[x, y]);
                }
            }
            return movelist;
        }
    }
}