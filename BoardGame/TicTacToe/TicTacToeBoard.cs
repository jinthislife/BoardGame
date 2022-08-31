using System;
using System.Data.Common;

namespace BoardGame
{
    public class TicTacToeBoard : Board
    {
        public TicTacToeBoard(int width, int height) : base(width, height)
        {
        }

        public override void Render()
        {
            const int moduleWidth = 6; //QQ: naming convention
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
                            SymbolPiece piece = moves[gridRow, gridCol].piece as SymbolPiece;

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

        // TODO: wincheck except diag/antidiagonal
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
    }
}