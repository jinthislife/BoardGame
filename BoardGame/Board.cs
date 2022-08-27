using System;
using System.Collections.Generic;

namespace BoardGame
{
    public abstract class Board
    {
        private int row;
        private int column;
        public Move[,] grid;

        // let subclass implement checkWin
        // as each game has different rules to decide win
        public abstract bool checkWin(Move latest);

        public Board(int width, int height)
        {
            row = width;
            column = height;
            grid = new Move[width, height];
        }

        public void place(Move move)
        {
            grid[move.row, move.col] = move;
            render();
        }

        public void withdraw(Move move)
        {
            grid[move.row, move.col] = null;
            render();
        }

        public void render()
        {
            const int moduleWidth = 6; //QQ: naming convention
            const int moduleHeight = 3;

            int maxX = row * moduleWidth - 1;
            int maxY = column * moduleHeight - 1;

            Console.WriteLine("\n");
            for (var y=0; y <= maxY; y++)
            {
                for (var x=0; x <= maxX; x++)
                {
                    if ( (x + 1) % moduleWidth == 0 && x != maxX)
                    {
                        Console.Write("|");
                    }
                    else if ((y+1) % moduleHeight == 0 && y != maxY)
                    {
                        Console.Write("_");
                    }
                    else if ( x % moduleWidth == 2 && y % moduleHeight == 1)
                    {
                        int gridRow = x / moduleWidth;
                        int gridCol = y / moduleHeight;
                        
                        if (grid[gridRow, gridCol] == null)
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            SymbolPiece piece = grid[gridRow, gridCol].player.piece as SymbolPiece;
                            Console.Write(piece.symbol);
                        }
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("\n");
            }

         }

        public List<Move> getStates()
        {
            List<Move> movelist = new List<Move>();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (grid[i, j] != null)
                    {
                        Console.WriteLine($"Append to list {i} {j}");
                        movelist.Add(grid[i, j]);
                    }
                }
            }

            return movelist;
        }

        public void loadStates(List<Move> moves)
        {
            foreach (Move move in moves)
            {
                grid[move.row, move.col] = move;
            }
            Console.WriteLine($"Total: {moves.Count}");
            render();
        }

        public int[] getEmptyModule()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (grid[i, j] == null)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return null;
        }

        public bool checkValidMove(int x, int y)
        {
            if (x > (row - 1) || y > (column - 1))
            {
                Console.WriteLine("The move is out of valid range.");
                return false;
            }

            if (grid[x, y] != null)
            {
                Console.WriteLine($"{x} {y} is already occupied.");
                return false;
            }

            return true;
        }
    }
}