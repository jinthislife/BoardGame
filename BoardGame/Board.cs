﻿using System;
using System.Reflection;
using System.Linq;

namespace BoardGame
{
    public class Board : IObserver<MoveRecord>
    {
        private int row;
        private int column;
        //public Move[,] grid;
        public Board(int width, int height)
        {
            row = width;
            column = height;
            //grid = new Move[width, height];
        }

        //public void placeMove(Move move)
        //{
        //    grid[move.row, move.col] = move;
            //render(); // QQ Observer Pattern?
        //}

        public void render(Move[,] grid)
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

        // IObserver Interfaces

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(MoveRecord record)
        {
            Console.WriteLine("Board Received Notification");
            render(record.moves);
        }
    }
}
