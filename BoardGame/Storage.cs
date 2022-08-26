using System;
using System.IO;
using System.Collections.Generic;

namespace BoardGame
{
    public class Storage
    {
        private const string FILENAME = "gamestate.txt";

        public Storage()
        {
        }

        public void save(List<Move> moves)
        {
            FileStream outFile = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);

            foreach (Move move in moves)
            {
                writer.WriteLine(move.ToString());
            }

            writer.Close();
            outFile.Close();
        }

        public List<Move> load()
        {
            FileStream inFile = new FileStream(FILENAME, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string recordIn = reader.ReadLine();

            List<Move> moves = new List<Move>();

            while (recordIn != null)
            {
                try
                {
                    Console.WriteLine($"{recordIn}");

                    Move m = parseLine(recordIn);
                    moves.Add(m);
                    recordIn = reader.ReadLine();
                }
                catch
                {
                    Console.WriteLine("Failed to load history");
                    break;
                }
            }

            return moves;
        }

        private Move parseLine(String line)
        {
            int row, col, isHuman;
            String[] slices = line.Split(",");
           
            if (int.TryParse(slices[0], out row) &&
                int.TryParse(slices[1], out col) &&
                int.TryParse(slices[2], out isHuman))
            {
                Piece piece;
                Player player;

                if (slices[4] == "BoardGame.SymbolPiece")
                {
                    piece = new SymbolPiece(symbol: slices[5].ToCharArray()[0]);
                }
                else
                {
                    piece = new ColorPiece(color: slices[5]);
                }

                if (isHuman == 0)
                {
                    player = new AIPlayer(piece, slices[2]);
                }
                else
                {
                    player = new HumanPlayer(piece, slices[2]);
                }
                return new Move(row, col, player);
            }
            else
            {
                throw new InvalidDataException();
            }      
        }
    }
}

