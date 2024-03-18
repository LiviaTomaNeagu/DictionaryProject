using DictionaryApp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dictionary
{
    public partial class MainWindow
    {
        SearchWords searching = new SearchWords(dictionary);
        private void SearchWord(object sender, TextChangedEventArgs e)
        {
            FilterWordsByCategory(SearchBar.Text, Categories.SelectedItem as string);
        }

        private void FilterWordsByCategory(string searchText, string selectedCategory)
        {
            Suggestions.ItemsSource = searching.getFilteredWords(searchText, selectedCategory);
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
            if (Suggestions.SelectedItem is string selectedWord && selectedWord != "No words found. :(")
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

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            Syntax.Visibility = Visibility.Visible;
            SyntaxText.Visibility = Visibility.Visible;

            Description.Visibility = Visibility.Visible;
            DescriptionText.Visibility = Visibility.Visible;

            ImageDisplaySearch.Visibility = Visibility.Visible;

            Syntax.Text = SearchBar.Text;
            SearchBar.Text = "";
            Categories.SelectedItem = null;

            foreach (Word word in dictionary.getWordsList())
            {
                if (word.Syntax == Syntax.Text)
                {
                    Description.Text = word.Description;
                    ImageDisplaySearch.Source = word.DisplayImage();
                }    
            }
        }
    }
}
