using System;
using System.IO;
using System.Collections.Generic;

namespace BoardGame
{
    public abstract class Storage
    {
        protected abstract string FILENAME { get; }

        protected abstract Move ParseLine(String line);

        public bool ExistsPreviousState() => File.Exists(FILENAME) ? true : false;
 
        public void Save(List<Move> moves)
        {
            FileStream outFile = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);

            try
            {
                foreach (Move move in moves)
                {
                    writer.WriteLine(move.ToString());
                }
                Console.WriteLine("\nSuccessfully saved.\n");
            }
            catch
            {
                Console.WriteLine("\nFailed to save.\n");
            }

            writer.Close();
            outFile.Close();
        }

        public List<Move> Load()
        {
            
            FileStream inFile = new FileStream(FILENAME, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string recordIn = reader.ReadLine();

            List<Move> moves = new List<Move>();

            while (recordIn != null)
            {
                try
                {
                    Move m = ParseLine(recordIn);
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