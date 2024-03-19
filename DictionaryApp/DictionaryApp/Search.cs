using DictionaryApp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

            CategorySearch.Visibility = Visibility.Visible;

            ImageDisplaySearch.Visibility = Visibility.Visible;

            Syntax.Text = SearchBar.Text;
            SearchBar.Text = "";
            Categories.SelectedItem = null;

            foreach (Word word in dictionary.getWordsList())
            {
                if (word.Syntax == Syntax.Text)
                {
                    Description.Text = word.Description;
                    CategorySearch.Text = word.Category.ToUpper();
                    if(word.DisplayImage() != null)
                        ImageDisplaySearch.Source = word.DisplayImage();
                    else
                    {
                        ImageDisplaySearch.Source = LoadImage("no_image");
                    }
                }    
            }
        }

        private void AutoSizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.UpdateLayout();
            ResizeTextBoxToFitText(textBox);
        }

        private void AutoSizeTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null) return;

            textBox.UpdateLayout();
            // Perform the text measurement and resizing here, as in the previous example
            ResizeTextBoxToFitText(textBox);
        }

        private void ResizeTextBoxToFitText(TextBox textBox)
        {
            FormattedText formattedText = new FormattedText(
                textBox.Text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(textBox.FontFamily, textBox.FontStyle, textBox.FontWeight, textBox.FontStretch),
                textBox.FontSize,
                Brushes.Black,
                new NumberSubstitution(),
                VisualTreeHelper.GetDpi(textBox).PixelsPerDip);

            double minHeight = formattedText.Height;
            if (textBox.TextWrapping != TextWrapping.NoWrap && textBox.LineCount > 1)
            {
                minHeight *= textBox.LineCount;
            }

            double finalHeight = Math.Max(minHeight, 20); // Use a sensible minimum height
            textBox.Height = finalHeight;
        }

    }
}
