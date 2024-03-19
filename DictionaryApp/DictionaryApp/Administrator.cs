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
using System.Windows.Media;

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

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                BitmapImage myImage = new BitmapImage(new Uri(filePath, UriKind.Absolute));
                ImageButton.Tag = myImage;
                ImageDisplay.Source = myImage;
            }

        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidateWord(SyntaxBox.Text, CategoryBox.Text, DescriptionBox.Text)) {
                MessageBox.Show("Incorrect data inserted", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var imageFromButton = ImageButton.Tag as ImageSource;
            String newCategory =  CategoryBox.Text;
            if (!dictionary.getCategoriesList().Any(c => c == newCategory))
            {
                dictionary.addCategory(newCategory);
            }
            ExistingCategories.ItemsSource = dictionary.getCategoriesList();
            ExistingCategoriesEdit.ItemsSource = dictionary.getCategoriesList();
            Categories.ItemsSource = dictionary.getCategoriesList();
            processData.WriteWord(SyntaxBox.Text, CategoryBox.Text, DescriptionBox.Text, imageFromButton);

            SyntaxBox.Text = null;
            CategoryBox.Text = null;   
            DescriptionBox.Text = null;
            ExistingCategories.SelectedItem = null;
            LoadImage("no_image");

        }

        private void SearchCategory(object sender, TextChangedEventArgs e)
        {
            string searchText = CategoryBox.Text.ToLower();

            var categories = dictionary.getCategoriesList()
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
            else
            {
                ExistingCategories.SelectedItem = null;
                CategoryBox.Text = "";
            }
        }

        private void CategorySuggestionSelected(object sender, SelectionChangedEventArgs e)
        {
            if (ListExistingCategories.SelectedItem is string selectedCategory)
            {
                if (selectedCategory != "No category found. :(")
                {
                    CategoryBox.Text = selectedCategory;

                    string selectedCategoryObject = dictionary.getCategoriesList().FirstOrDefault(cat => cat == selectedCategory);
                    if (selectedCategoryObject != null)
                    {
                        ExistingCategories.SelectedItem = selectedCategoryObject;
                        ListExistingCategories.SelectedItem = selectedCategoryObject;
                        ListExistingCategories.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    ListExistingCategories.SelectedItem = null;
                    ListExistingCategories.Visibility = Visibility.Visible;
                }
            }
        }

    }
}
