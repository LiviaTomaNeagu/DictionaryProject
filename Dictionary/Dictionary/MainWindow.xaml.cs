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

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal List<Category> CategoriesList = new List<Category>
        {
            new Category("Choose category"),
            new Category("Categoria1"),
            new Category("Categoria2"),
        };
        private List<Word> WordsList = new List<Word>
        {
            new Word("apple", "Categoria1", "Description1"),
            new Word("banana", "Categoria2", "Description2"),
            new Word("orange", "Categoria1", "Description3"),
            new Word("avocado", "Categoria2", "Description4"),
            new Word("grape", "Categoria1", "Description5"),
        };

        public MainWindow()
        {
            InitializeComponent();

            // Setează sursa pentru ComboBox
            Categories.ItemsSource = CategoriesList;
        }

        private void SearchWord(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBar.Text.ToLower();

            var suggestions = WordsList
                .Where(word => string.IsNullOrWhiteSpace(searchText) || word.Syntax.ToLower().StartsWith(searchText));

            if (Categories.SelectedItem is Category selectedCategory && selectedCategory.Name != "Choose category")
            {
                suggestions = suggestions.Where(word => word.Category == selectedCategory.Name);
            }

            var suggestionList = suggestions.Select(word => word.Syntax).ToList();

            if (suggestionList.Count == 0)
            {
                suggestionList.Add("No words found. :(");
            }

            Suggestions.ItemsSource = suggestionList;
            Suggestions.Visibility = !string.IsNullOrWhiteSpace(searchText)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }



        private void FilterWordsByCategory(string selectedCategory)
        {
            string searchText = SearchBar.Text.ToLower();

            var filteredWords = WordsList
                .Where(word => (selectedCategory == "Choose category" || word.Category == selectedCategory) &&
                               (string.IsNullOrWhiteSpace(searchText) || word.Syntax.ToLower().StartsWith(searchText)))
                .Select(word => word.Syntax);

            Suggestions.ItemsSource = filteredWords;
            Suggestions.Visibility = !string.IsNullOrWhiteSpace(searchText) && filteredWords.Any()
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void CategoriesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Categories.SelectedItem is Category selectedCategory)
            {
                Debug.WriteLine($"Selected Category: {selectedCategory.Name}");
                FilterWordsByCategory(selectedCategory.Name);
            }
        }

        private void SuggestionsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Suggestions.SelectedItem is string selectedWord)
            {
                //Word selectedWordObject = WordsList.FirstOrDefault(word => word.Syntax == selectedWord);

                //if (selectedWordObject != null)
                //{
                //    SearchBar.Text = selectedWordObject.Syntax;
                //    Suggestions.Visibility = Visibility.Collapsed;
                //}
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Preia informațiile din TextBox-uri
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (username == "1" && password == "2")
            {
                txtResult.Text = "Login succes!";
                txtResult.Visibility = Visibility.Visible;
                Login.Visibility = Visibility.Hidden;
                AdminActions.Visibility = Visibility.Visible;
            }
            else
            {
                txtResult.Text = "Login failed! Try again.";
                txtResult.Visibility = Visibility.Visible;
            }
        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                var image = new Image();
                image.Source = new BitmapImage(new Uri(filePath, UriKind.Absolute));
                ImageButton.Content = image;
                //de adaugat butonul in stanga si de modificat imaginea
            }

        }
    }
}
