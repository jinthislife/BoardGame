using System;
using System.Collections.Generic;

namespace BoardGame
{
    public abstract class Board
    {
        protected int width;
        protected int height;
        protected Move[,] moves;
        public int CountOccupied
        {
            get =>
                ((width * height) - GetAvaliableLocs().Count);
        }

        // let subclass implement checkWin
        // as each game has different rules to decide win
        public abstract bool CheckWin(Move latest);
        public abstract void Render();

        protected Board(int width, int height)
        {
            this.width = width;
            this.height = height;
            moves = new Move[width, height];
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

        public List<Move> GetStates()
        {
            List<Move> movelist = new List<Move>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (moves[x, y] != null)
                    {
                        Console.WriteLine($"Append to list {x} {y}");
                        movelist.Add(moves[x, y]);
                    }
                }
            }
            return movelist;
        }

        public void LoadStates(List<Move> movelist)
        {
            foreach (Move move in movelist)
            {
                moves[move.locX, move.locY] = move;
            }
            Console.WriteLine($"Total: {movelist.Count}");
            Render();
        }

        public List<(int, int)> GetAvaliableLocs()
        {
            List<(int, int)> locs = new List<(int, int)>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (moves[x, y] == null)
                    {
                        locs.Add((x, y));
                    }
                }
            }
            return locs;
        }

        public bool CheckAvailableLoc(int x, int y)
        {
            if (x > (width - 1) || y > (height - 1))
            {
                Console.WriteLine("Out of valid range.");
                return false;
            }

            if (moves[x, y] != null)
            {
                Console.WriteLine($"{x} {y} is already occupied.");
                return false;
            }
            return true;
        }

        public BoardGameState CheckState(Move latest)
        {
            BoardGameState state = BoardGameState.Playing;
            if (CheckWin(latest))
            {
                state = BoardGameState.Won;
            }
            else if (GetAvaliableLocs().Count == 0)
            {
                state = BoardGameState.Draw;
            }
            return state;
        }
    }
}