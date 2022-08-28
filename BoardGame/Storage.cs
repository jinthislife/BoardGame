using System;
using System.IO;
using System.Collections.Generic;

namespace BoardGame
{
    public abstract class Storage
    {

        public Storage()
        {
        }

        public abstract string FILENAME { get; }

        public abstract Move parseLine(String line);

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
    }
}

