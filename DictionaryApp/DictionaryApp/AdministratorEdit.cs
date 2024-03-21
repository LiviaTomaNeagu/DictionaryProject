using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dictionary
{
   public partial class MainWindow
    {
        private void SearchWordEdit(object sender, TextChangedEventArgs e)
        {
            string searchText = SyntaxBoxEdit.Text.ToLower();
            var words = dictionary.getWordsList()
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
                foreach (Word word in dictionary.getWordsList())
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
                if(myWord.DisplayImage() != null)
                    ImageDisplayEdit.Source = myWord.DisplayImage();
                else
                {
                    ImageDisplayEdit.Source = LoadImage("no_image");
                }
                ImageButtonEdit.Tag = myWord.DisplayImage();

            }

            
        }

        private void DoneButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateWord(SyntaxBoxEdit.Text, CategoryBoxEdit.Text, DescriptionBoxEdit.Text))
            {
                MessageBox.Show("Incorrect data inserted", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ImageButtonEdit.Tag is ImageSource imageSource)
            {
                processData.ModifyWordInFile(dictionary.modifyWord(SyntaxBoxEdit.Text, CategoryBoxEdit.Text, DescriptionBoxEdit.Text, imageSource));
            }
            else
            {
                processData.ModifyWordInFile(dictionary.modifyWord(SyntaxBoxEdit.Text, CategoryBoxEdit.Text, DescriptionBoxEdit.Text, null));
            }

            SyntaxBoxEdit.Text = null;
            CategoryBoxEdit.Text = null;
            ExistingCategoriesEdit.SelectedItem = null;
            DescriptionBoxEdit.Text = null;
            ListExistingCategoriesEdit.SelectedItem = null;
            SearchEdit.SelectedItem = null;
            LoadImage("no_image");


        }

        private void ExistingCategoriesEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ExistingCategoriesEdit.SelectedItem != null) {
                CategoryBoxEdit.Text = ExistingCategoriesEdit.SelectedItem.ToString();
            }
            
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


        private void DeleteButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            dictionary.setWordsList( processData.DeleteWord(SyntaxBoxEdit.Text));
            SyntaxBoxEdit.Text = null;
            CategoryBoxEdit.Text = null;
            ExistingCategoriesEdit.SelectedItem = null;
            DescriptionBoxEdit.Text = null;
            LoadImage("no_image");
        }
    }
}
