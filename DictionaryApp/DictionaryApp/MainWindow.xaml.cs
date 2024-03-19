using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.IO;
using Newtonsoft.Json;
using DictionaryApp;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static DictionaryLogic dictionary = new DictionaryLogic();
        static ProcessData processData = new ProcessData(dictionary);
        //internal List<string> CategoriesList = new List<string>();
        //internal List<Word> WordsList = new List<Word>();

        public MainWindow()
        {
           
            InitializeComponent();

            processData.ReadWords();
            Categories.ItemsSource = dictionary.getCategoriesList();
            ExistingCategories.ItemsSource = dictionary.getCategoriesList();
            ExistingCategoriesEdit.ItemsSource = dictionary.getCategoriesList();
            LoadImage("no_image");
            Categories.SelectedItem = null;
            ExistingCategories.SelectedItem = null;
            ExistingCategoriesEdit.SelectedItem = null;
            CategoryBoxEdit.Text = string.Empty;
        }

        public ImageSource LoadImage(string imageName)
        {
            string imagePath = DataPathHelper.GetDataFilePath($"{imageName}.jpg"); // Make sure to include the correct file extension

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(imagePath, UriKind.Absolute);
            image.CacheOption = BitmapCacheOption.OnLoad; // To ensure the image is loaded immediately and stored in memory
            image.EndInit();

            ImageDisplay.Source = image;
            ImageDisplayEdit.Source = image;

            return image;
        }

        private bool ValidateWord(string syntax, string category, string description)
        {
            if(syntax == "" || category == "" || description == "" ) {  return false; }
            return true;
        }
    }
}
