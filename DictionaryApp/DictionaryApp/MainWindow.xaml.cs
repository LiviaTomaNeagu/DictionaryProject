using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;
using DictionaryApp;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal List<string> CategoriesList = new List<string>();
        internal List<Word> WordsList = new List<Word>();

        public MainWindow()
        {
           
            InitializeComponent();

            ReadWords();
            Categories.ItemsSource = CategoriesList;
            ExistingCategories.ItemsSource = CategoriesList;
            ExistingCategoriesEdit.ItemsSource = CategoriesList;
            LoadImage("no_image");

        }

        public void LoadImage(string imageName)
        {
            string imagePath = DataPathHelper.GetDataFilePath($"{imageName}.jpg"); // Make sure to include the correct file extension

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(imagePath, UriKind.Absolute);
            image.CacheOption = BitmapCacheOption.OnLoad; // To ensure the image is loaded immediately and stored in memory
            image.EndInit();

            ImageDisplay.Source = image;
            ImageDisplayEdit.Source = image;
        }

        private void DeleteButtonEdit_Click(object sender, RoutedEventArgs e)
        {

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

            // Write the updated JSON data back to the file
            File.WriteAllText(filePath, jsonData);
        }


    }
}
