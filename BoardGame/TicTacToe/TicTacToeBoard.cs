﻿using System;
namespace BoardGame
{
    public class TicTacToeBoard : Board
    {
        public TicTacToeBoard(int width, int height) : base(width, height)
        {
        }

        // TODO: wincheck except diag/antidiagonal
        public override bool CheckWin(Move latest)
        {
            // check col
            for (int col = 0; col < 3; col++)
            {
                if (grid[latest.row, col] == null || grid[latest.row, col].piece.ToString() != latest.piece.ToString())
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
                if (grid[row, latest.col] == null || grid[row, latest.col].piece.ToString() != latest.piece.ToString())
                {
                    break;
                }
                else if (row == 2)
                {
                    return true;
                }
            }

            // check diag
            if (latest.row == latest.col)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (grid[i, i] == null || grid[i, i].piece.ToString() != latest.piece.ToString())
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
            if (latest.row + latest.col == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (grid[i, 2 - i] == null || grid[i, 2 - i].piece.ToString() != latest.piece.ToString())
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