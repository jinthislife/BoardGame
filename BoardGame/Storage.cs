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
        public Storage()
        {
        }

        public void save(List<Move> moves)
        {
            const string FILENAME = "gamestate.txt";
            
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            String serialized = JsonSerializer.Serialize<object>(moves, options); // Polymorphic serialization
            FileStream outFile = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);

            Console.WriteLine($"Serialized: {serialized}");

            writer.Write(serialized);

            writer.Close();
            outFile.Close();
        }

        public List<Move> load()
        {
            const string FILENAME = "gamestate.txt";
            FileStream inFile = new FileStream(FILENAME, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string json = reader.ReadToEnd();
            Console.WriteLine($"json {json}");

            var results = JsonSerializer.Deserialize<List<Move>>(json);
            Console.WriteLine($"results {results.Count}");
            return results;
        }
    }
}

