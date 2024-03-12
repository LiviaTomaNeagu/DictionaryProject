using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dictionary
{
   public partial class MainWindow
    {
        private void SearchWordEdit(object sender, TextChangedEventArgs e)
        {
            string searchText = SyntaxBoxEdit.Text.ToLower();

            var words = WordsList
                .Where(words =>
                    string.IsNullOrWhiteSpace(searchText) || words.Syntax.ToLower().StartsWith(searchText))
                .Select(category => category.Syntax)
                .ToList();


            SearchEdit.ItemsSource = words;
            SearchEdit.Visibility = !string.IsNullOrWhiteSpace(searchText)
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;
        }

        private void SearchEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchEdit.SelectedItem is string selectedWord)
            {
                string lowerSearchText = SyntaxBoxEdit.Text.ToLower();

                if (lowerSearchText != selectedWord.ToLower())
                {
                   SyntaxBoxEdit.Text = selectedWord;
                }

                SearchEdit.Visibility = string.IsNullOrWhiteSpace(SyntaxBoxEdit.Text)
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;

                //ExistingCategoriesEdit.SelectedItem = selectedWord.Category;
                //CategoryBoxEdit.Text = selectedWord.Category;
            }

            
        }

    }
}
