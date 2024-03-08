using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dictionary
{
    public partial class MainWindow
    {
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
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;
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
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;
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
    }
}
