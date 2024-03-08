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

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        internal List<Category> CategoriesList = new List<Category>
        {
            new Category("Choose category"),
            new Category("Categoria1"),
            new Category("Categoria2"),
        };
        private List<Word> WordsList = new List<Word>
        {
            new Word("apple", "Categoria1", "Description1"),
            new Word("banana", "Categoria2", "Description2"),
            new Word("orange", "Categoria1", "Description3"),
            new Word("avocado", "Categoria2", "Description4"),
            new Word("grape", "Categoria1", "Description5"),
        };

        public MainWindow()
        {
            InitializeComponent();

            // Setează sursa pentru ComboBox
            Categories.ItemsSource = CategoriesList;
        }
    }


}
