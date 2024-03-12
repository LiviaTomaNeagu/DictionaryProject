using Dictionary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    public partial class MainWindow
    {
        private const string DataFilePath = "E:\\facultate\\anul2\\semestrul2\\MVP\\DictionaryProject\\DictionaryApp\\DictionaryApp\\Data.txt";

        private void ReadWords()
        {
            try
            {
                if (File.Exists(DataFilePath))
                {
                    using (var reader = new StreamReader(DataFilePath))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();

                            // Skip empty or whitespace lines
                            if (string.IsNullOrWhiteSpace(line))
                                continue;

                            string[] wordDetails = line.Split(',');

                            if (wordDetails.Length == 3)
                            {
                                string syntax = wordDetails[0].Trim();
                                string category = wordDetails[1].Trim();
                                string description = wordDetails[2].Trim();

                                WordsList.Add(new Word(syntax, category, description));

                               
                                if (!CategoriesList.Any(c => c == category))
                                {
                                    CategoriesList.Add(category);
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Invalid line format: {line}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The file does not exist.");
                }
            }
            catch (IOException ex)
            {
                // Handle file reading error
                Console.WriteLine($"Error reading the file: {ex.Message}");
            }
        }

        private void WriteWord(string syntax, string category, string description)
        {
            try
            {
                using (var writer = new StreamWriter(DataFilePath, true))
                {
                    // Format the input parameters into a string with a newline character
                    string line = $"{syntax},{category},{description}";
                    writer.WriteLine('\n' + line);
                }

                Console.WriteLine("Data has been written to the file.");
            }
            catch (IOException ex)
            {
                // Handle file writing error
                Console.WriteLine($"Error writing to the file: {ex.Message}");
            }
        }
    }
}
