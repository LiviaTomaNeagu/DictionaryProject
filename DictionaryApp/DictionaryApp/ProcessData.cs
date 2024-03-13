using Dictionary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dictionary
{

    public partial class MainWindow
    {
        private static readonly string DataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "WordsData.json");

        public void ReadWords()
        {
            try
            {
                if (File.Exists(DataFilePath))
                {
                    string json = File.ReadAllText(DataFilePath);
                    WordsList = JsonConvert.DeserializeObject<List<Word>>(json) ?? new List<Word>();
                }
                else
                {
                    Console.WriteLine("The file does not exist. Creating a new list.");
                    WordsList = new List<Word>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
            }
        }

        public void WriteWord(Word word)
        {
            try
            {
                // Read existing words, add the new word, then write back to ensure we're always working with a list
                ReadWords(); // Load current words
                WordsList.Add(word); // Add the new word
                CategoriesList.Add(word.Category);

                string json = JsonConvert.SerializeObject(WordsList, Formatting.Indented);
                File.WriteAllText(DataFilePath, json);

                Console.WriteLine("Word has been added to the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to the file: {ex.Message}");
            }
        }
    }
}
