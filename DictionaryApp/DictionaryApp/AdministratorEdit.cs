using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Dictionary
{
   public partial class MainWindow
    {
        private void SearchWordEdit(object sender, TextChangedEventArgs e)
        {
            string searchText = SyntaxBoxEdit.Text.ToLower();
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
                DescriptionBoxEdit.Text = myWord.Description;
                ImageDisplayEdit.Source = myWord.DisplayImage();

            }

            
        }

        private void DoneButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Word myWord = null;
            foreach (Word word in WordsList)
            {
                if (word.Syntax == SyntaxBoxEdit.Text)
                    myWord = word;
            }

            myWord.Description = DescriptionBoxEdit.Text;
            ExistingCategoriesEdit.SelectedItem = CategoryBoxEdit.Text;
            myWord.Category = CategoryBoxEdit.Text;
            myWord.AddImage(ImageButtonEdit.Tag);
            ModifyWordInFile(myWord);


        }

        private void ExistingCategoriesEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoryBoxEdit.Text = ExistingCategoriesEdit.SelectedItem.ToString();
        }

        private void ImageButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                BitmapImage myImage = new BitmapImage(new Uri(filePath, UriKind.Absolute));
                ImageButtonEdit.Tag = myImage;
                ImageDisplayEdit.Source = myImage;
            }
        }
    }
}
