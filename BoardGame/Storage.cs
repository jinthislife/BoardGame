using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

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

            List<Move> moves = new List<Move>();
            int row, col, isHuman;
            string recordIn = reader.ReadLine();
            while (recordIn != null)
            {
                String[] slices = recordIn.Split(",");
                Console.WriteLine($"{recordIn}");
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

                    moves.Add(new Move(row, col, player));
                    recordIn = reader.ReadLine();
                }
                else
                {
                    throw new InvalidDataException();
                }
            }
            return moves;
        }

    }
}

