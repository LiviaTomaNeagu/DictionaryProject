using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace Dictionary
{
    public partial class MainWindow
    {
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

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            WordsList.Add(new Word(SyntaxBox.Text, CategoryBox.Text, DescriptionBox.Text));
            String newCategory =  CategoryBox.Text;
            if (!CategoriesList.Any(c => c == newCategory))
            {
                CategoriesList.Add(newCategory);
            }

            WriteWord(SyntaxBox.Text, CategoryBox.Text, DescriptionBox.Text);
        }

        private void SearchCategory(object sender, TextChangedEventArgs e)
        {
            string searchText = CategoryBox.Text.ToLower();

            var categories = CategoriesList
                .Where(category =>
                    string.IsNullOrWhiteSpace(searchText) || category.ToLower().StartsWith(searchText))
                .ToList();

            if (categories.Count == 0)
            {
                categories.Add("No category found. :(");
            }

            ListExistingCategories.ItemsSource = categories;
            ListExistingCategories.Visibility = !string.IsNullOrWhiteSpace(searchText)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }


        private void CategorySelected(object sender, SelectionChangedEventArgs e)
        {
            if (ExistingCategories.SelectedItem is string selectedCategory)
            {
                CategoryBox.Text = selectedCategory;
                ExistingCategories.SelectedItem = selectedCategory;
                ListExistingCategories.SelectedItem = selectedCategory;
                ListExistingCategories.Visibility = Visibility.Collapsed;
            }
        }

        private void CategorySuggestionSelected(object sender, SelectionChangedEventArgs e)
        {
            if (ListExistingCategories.SelectedItem is string selectedCategory)
            {
                CategoryBox.Text = selectedCategory;

                String selectedCategoryObject = CategoriesList.FirstOrDefault(cat => cat == selectedCategory);
                if (selectedCategoryObject != null) 
                {
                    ExistingCategories.SelectedItem = selectedCategoryObject;
                    ListExistingCategories.SelectedItem = selectedCategoryObject;
                }

                ListExistingCategories.Visibility = Visibility.Collapsed;
            }
        }

    }
}
