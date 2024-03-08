using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

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
    }
}
