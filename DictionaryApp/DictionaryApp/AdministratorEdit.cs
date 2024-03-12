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

            //var words = WordsList
            //    .Where(word =>
            //        string.IsNullOrWhiteSpace(searchText) || word.Syntax.ToLower().StartsWith(searchText))
            //    .Select(word => word.Syntax)
            //    .ToList();
            var words = WordsList
                .Where(word => string.IsNullOrWhiteSpace(searchText) || word.Syntax.ToLower().StartsWith(searchText))
                .ToList();


            List<String> wordsList = new List<String>();
            foreach (var wordtext in words) { wordsList.Add(wordtext.Syntax.ToLower()); }
            SearchEdit.ItemsSource = wordsList;
            SearchEdit.Visibility = !string.IsNullOrWhiteSpace(searchText)
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;
        }

        private void SearchEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchEdit.SelectedItem is string selectedWord)
            {
                Word myWord = null;
                foreach (Word word in WordsList)
                {
                    if (word.Syntax == selectedWord)
                        myWord = word;
                }
                string lowerSearchText = SyntaxBoxEdit.Text.ToLower();

                if (lowerSearchText != selectedWord.ToLower())
                {
                   SyntaxBoxEdit.Text = selectedWord;
                }

                SearchEdit.Visibility = string.IsNullOrWhiteSpace(SyntaxBoxEdit.Text)
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;

                ExistingCategoriesEdit.SelectedItem = myWord.Category;
                CategoryBoxEdit.Text = myWord.Category;
            }

            
        }

    }
}
