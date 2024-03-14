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

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal List<string> CategoriesList = new List<string>();
        //{
        //    new Category("Choose category"),
        //    new Category("Categoria1"),
        //    new Category("Categoria2"),
        //};
        internal List<Word> WordsList = new List<Word>();
        //{
        //    new Word("apple", "Categoria1", "Description1"),
        //    new Word("banana", "Categoria2", "Description2"),
        //    new Word("orange", "Categoria1", "Description3"),
        //    new Word("avocado", "Categoria2", "Description4"),
        //    new Word("grape", "Categoria1", "Description5"),
        //};

        public MainWindow()
        {
           
            InitializeComponent();

            ReadWords();
            Categories.ItemsSource = CategoriesList;
            ExistingCategories.ItemsSource = CategoriesList;
            ExistingCategoriesEdit.ItemsSource = CategoriesList;
        }

        

        private void DeleteButtonEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        public void ModifyWordInFile(string wordToModify, string newCategory, string newDefinition)
        {
            // Read all lines from the file
            var lines = File.ReadAllLines("Data.txt");
            var updatedLines = new List<string>();

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    var word = parts[0].Trim();
                    var category = parts[1].Trim();
                    var definition = parts[2].Trim();

                    // Check if this is the word to modify
                    if (word.Equals(wordToModify, StringComparison.OrdinalIgnoreCase))
                    {
                        // Modify the word and/or definition
                        updatedLines.Add($"{wordToModify},{newCategory}, {newDefinition}");
                    }
                    else
                    {
                        // Keep the line as it is
                        updatedLines.Add(line);
                    }
                }
                else
                {
                    // Line format not as expected, keep it as it is
                    updatedLines.Add(line);
                }
            }

            // Write the updated lines back to the file
            File.WriteAllLines("Data.txt", updatedLines);
        }

        
    }
}
