using Dictionary;
using DictionaryApp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace Dictionary
{

    public partial class ProcessData
    {
        DictionaryLogic dictionaryIO;

        internal ProcessData(DictionaryLogic dictionary)
        {
            dictionaryIO = dictionary;
        }

        private static readonly string DataFilePath = DataPathHelper.GetDataFilePath("WordsData.json");



        public void ReadWords()
        {   
            try
            {
                if (File.Exists(DataFilePath))
                {
                    string json = File.ReadAllText(DataFilePath);
                    dictionaryIO.setWordsList(JsonConvert.DeserializeObject<List<Word>>(json) ?? new List<Word>());
                    // Clear the CategoryList to avoid duplicating categories if this method is called multiple times
                    dictionaryIO.clearCategory();

                    // Add categories to CategoryList
                    foreach (var word in dictionaryIO.getWordsList())
                    {
                        if (!dictionaryIO.getCategoriesList().Contains(word.Category))
                        {
                            dictionaryIO.addCategory(word.Category);
                        }
                    }   
                }
                else
                {
                    Console.WriteLine("The file does not exist. Creating a new list.");
                    dictionaryIO.setWordsList(new List<Word>());
                    dictionaryIO.clearCategory() ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
            }
        }

        public void WriteWord(string syntax, string category, string description, ImageSource imageSource)
        {
            try
            {
                Word word = new Word(syntax, category, description, imageSource);
                // Read existing words, add the new word, then write back to ensure we're always working with a list
                ReadWords(); // Load current words
                dictionaryIO.addWord(word); // Add the new word
                if (!dictionaryIO.CategoriesList.Contains(word.Category))
                {
                    dictionaryIO.addCategory(word.Category); // Add the new category
                }



                string json = JsonConvert.SerializeObject(dictionaryIO.getWordsList(), Formatting.Indented);
                File.WriteAllText(DataFilePath, json);

                Console.WriteLine("Word has been added to the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to the file: {ex.Message}");
            }
        }
        public void ModifyWordInFile(Word updatedWord)
        {
            //Path to the JSON file
            string filePath = DataPathHelper.GetDataFilePath("WordsData.json");

            // Ensure the file exists
            if (!File.Exists(filePath))
            {
                // Handle the case where the file does not exist, if necessary
                return;
            }

            // Read the existing JSON data
            var jsonData = File.ReadAllText(filePath);
            var wordsList = JsonConvert.DeserializeObject<List<Word>>(jsonData) ?? new List<Word>();

            // Find the index of the word to modify
            int index = wordsList.FindIndex(w => w.Syntax.Equals(updatedWord.Syntax, System.StringComparison.OrdinalIgnoreCase));
            if (index != -1)
            {
                // Replace the old word with the updated word in the list
                wordsList[index] = updatedWord;
            }
            else
            {
                // If the word does not exist in the list, add it (optional)
                wordsList.Add(updatedWord);
            }

            // Serialize the updated list back to JSON
            jsonData = JsonConvert.SerializeObject(wordsList, Formatting.Indented);

            dictionaryIO.clearCategory();

            // Add categories to CategoryList
            foreach (var word in wordsList)
            {
                if (!dictionaryIO.getCategoriesList().Contains(word.Category))
                {
                    dictionaryIO.addCategory(word.Category);
                }
            }


            // Write the updated JSON data back to the file
            File.WriteAllText(filePath, jsonData);
        }

        // Method to delete a word from the file based on its syntax
        public List<Word> DeleteWord(string syntax)
        {
            // Path to the JSON file
            string filePath = DataPathHelper.GetDataFilePath("WordsData.json");

            // Ensure the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return null;
            }

            // Read the existing JSON data
            var jsonData = File.ReadAllText(filePath);
            var wordsList = JsonConvert.DeserializeObject<List<Word>>(jsonData) ?? new List<Word>();

            // Find the index of the word to delete
            int index = wordsList.FindIndex(w => w.Syntax.Equals(syntax, StringComparison.OrdinalIgnoreCase));

            // Check if the word was found
            if (index != -1)
            {
                // Remove the word from the list
                wordsList.RemoveAt(index);

                // Serialize the updated list back to JSON
                jsonData = JsonConvert.SerializeObject(wordsList, Formatting.Indented);

                // Write the updated JSON data back to the file
                File.WriteAllText(filePath, jsonData);

                Console.WriteLine("Word has been deleted from the file.");
            }
            else
            {
                Console.WriteLine("Word not found in the file.");
            }

            dictionaryIO.clearCategory();

            // Add categories to CategoryList
            foreach (var word in wordsList)
            {
                if (!dictionaryIO.getCategoriesList().Contains(word.Category))
                {
                    dictionaryIO.addCategory(word.Category);
                }
            }

            return wordsList;
        }
    }
}
