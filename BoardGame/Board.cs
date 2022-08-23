using System;
using System.Reflection;
using System.Linq;

namespace BoardGame
{
    public class Board
    {
        private int row;
        private int column;
        public Move[,] grid;
        public Board(int width, int height)
        {
            row = width;
            column = height;
            grid = new Move[width, height];
        }

        public void placeMove(Move move)
        {

            grid[move.row, move.col] = move;
            render(); // QQ Observer Pattern?
            // add move to MoveTracker
    
        }

        public void withdrawMove(Move move)
        {
            grid[move.row, move.col] = null;
            render();
        }

        //public bool placeMove(string command, Player p)
        //{

        //    String[] cmdSlices = command.Split(' ');
        //    if (cmdSlices.Length != 3)
        //    {
        //        return false;
        //    }
        //    int r, c;

        //    if (!int.TryParse(cmdSlices[1], out r) || !int.TryParse(cmdSlices[2], out c))
        //    {
        //        return false;
        //    }

        //    if (r > row || c > row) return false;

        //    grid[r, c] = new Move(r, c, p);
        //    render(); // QQ Observer Pattern?
        //    // add move to MoveTracker
        //    return true;
        //}

        //public void render(Move[,] grid)
        public void render()
        {
            const int moduleWidth = 6; //QQ: naming convention
            const int moduleHeight = 3;

            int maxX = row * moduleWidth - 1;
            int maxY = column * moduleHeight - 1;
            

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
                        //Console.WriteLine($"RENDER ROW: {gridRow} WID:{gridCol
                        if (grid[gridRow, gridCol] == null)
                        {
                            Console.Write("*");
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
    }
}

