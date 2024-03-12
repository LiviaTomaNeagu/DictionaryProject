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
            FilterWordsByCategory(SearchBar.Text, Categories.SelectedItem as string);
        }

        private void FilterWordsByCategory(string searchText, string selectedCategory)
        {
            searchText = searchText.ToLower();

            var filteredWords = WordsList
                .Where(word =>
                    ((selectedCategory == null) || word.Category == selectedCategory) &&
                    (string.IsNullOrWhiteSpace(searchText) || word.Syntax.ToLower().StartsWith(searchText)))
                .Select(word => word.Syntax)
                .ToList();

            if (filteredWords.Count == 0)
            {
                filteredWords.Add("No words found. :(");
            }

            Suggestions.ItemsSource = filteredWords;
            Suggestions.Visibility = !string.IsNullOrWhiteSpace(searchText)
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;
        }

        private void CategoriesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Categories.SelectedItem is string selectedCategory)
            {
                FilterWordsByCategory(SearchBar.Text, selectedCategory);
            }
        }

        private void SuggestionsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Suggestions.SelectedItem is string selectedWord)
            {
                string lowerSearchText = SearchBar.Text.ToLower();

                if (lowerSearchText != selectedWord.ToLower())
                {
                     SearchBar.Text = selectedWord;
                }

                Suggestions.Visibility = string.IsNullOrWhiteSpace(SearchBar.Text)
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;
            }

            Suggestions.SelectedItem = null;
        }
    }
}
