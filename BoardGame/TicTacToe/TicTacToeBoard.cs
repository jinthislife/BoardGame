using System;
using System.Collections.Generic;

namespace BoardGame
{
    public class TicTacToeBoard : Board
    {
        public TicTacToeBoard(int width, int height) : base(width, height)
        {
            moves = new Move[width, height];
        }

        public override void Render()
        {
            const int moduleWidth = 6;
            const int moduleHeight = 3;

            int maxX = width * moduleWidth - 1;
            int maxY = height * moduleHeight - 1;

            Console.WriteLine("\n");
            for (var y = 0; y <= maxY; y++)
            {
                for (var x = 0; x <= maxX; x++)
                {
                    if ((x + 1) % moduleWidth == 0 && x != maxX)
                    {
                        Console.Write("|");
                    }
                    else if ((y + 1) % moduleHeight == 0 && y != maxY)
                    {
                        Console.Write("_");
                    }
                    else if (x % moduleWidth == 2 && y % moduleHeight == 1)
                    {
                        int gridRow = x / moduleWidth;
                        int gridCol = y / moduleHeight;

                        if (moves[gridRow, gridCol] == null)
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            TicTacToePiece piece = moves[gridRow, gridCol].piece as TicTacToePiece;
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

        public override bool CheckWin(Move latest)
        {
            // check col
            for (int col = 0; col < 3; col++)
            {
                if (moves[latest.locX, col] == null || moves[latest.locX, col].piece.ToString() != latest.piece.ToString())
                {
                    break;
                }
                else if (col == 2)
                {
                    return true;
                }
            }

            // check row
            for (int row = 0; row < 3; row++)
            {
                if (moves[row, latest.locY] == null || moves[row, latest.locY].piece.ToString() != latest.piece.ToString())
                {
                    break;
                }
                else if (row == 2)
                {
                    return true;
                }
            }

            // check diag
            if (latest.locX == latest.locY)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (moves[i, i] == null || moves[i, i].piece.ToString() != latest.piece.ToString())
                    {
                        break;
                    }
                    else if (i == 2)
                    {
                        return true;
                    }
                }
            }

            // check anti-diagonal
            if (latest.locX + latest.locY == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (moves[i, 2 - i] == null || moves[i, 2 - i].piece.ToString() != latest.piece.ToString())
                    {
                        break;
                    }
                    else if (i == 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool CheckIfEmpty(int x, int y)
        {
            if (x > (width - 1) || y > (height - 1))
            {
                Console.Write("Out of valid range. ");
                return false;
            }

            if (moves[x, y] != null)
            {
                Console.Write($"{x} {y} is already occupied. ");
                return false;
            }
            return true;
        }

        public override List<(int, int)> GetEmptyPositions()
        {
            List<(int, int)> locs = new List<(int, int)>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (moves[x, y] == null) locs.Add((x, y));
                }
            }
            return locs;
        }

        public override int getOccupiedCount()
        {
            return ((width * height) - GetEmptyPositions().Count);
        }
    }
}