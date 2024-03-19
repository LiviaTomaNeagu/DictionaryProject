using DictionaryApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Dictionary
{
    public partial class MainWindow
    {

        internal GameEntertainment game = new GameEntertainment(dictionary);


        private void EntertainmentButtonClick(object sender, RoutedEventArgs e)
        {
            TableEntertainment.Visibility = Visibility.Visible;
            game.addRect(new List<Rectangle>{ Rect1, Rect2, Rect3, Rect4, Rect5});
            game.ChooseRandomWords();
            EntertainmentButton.Visibility = Visibility.Collapsed;
            TypeWord.Visibility = Visibility.Visible;

            NextButton.Visibility = Visibility.Visible;
            NextButton.IsEnabled = true;

            FinishButton.Visibility = Visibility.Visible;
            FinishButton.IsEnabled = false;

            Game.Visibility = Visibility.Visible;

            Random rnd = new Random();
            int rand = rnd.Next(1, 13);

            if (game.GameWords[game.CurrentWord].ImageBase64 == null)
            {
                DescriptionEntertainmentBox.Visibility = Visibility.Visible;
                DescriptionEntertainmentBox.Text = game.GameWords[game.CurrentWord].Description;
                TypeWord.Text = "";
            }
            else
            {
                if (rand % 2 == 0)
                {
                    ImageEntertainmentBox.Source = game.GameWords[game.CurrentWord].DisplayImage();
                    ImageEntertainmentBox.Visibility = Visibility.Visible;
                    TypeWord.Text = "";
                }
                else
                {
                    DescriptionEntertainmentBox.Visibility = Visibility.Visible;
                    DescriptionEntertainmentBox.Text = game.GameWords[game.CurrentWord].Description;
                    TypeWord.Text = "";
                }
            }
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            DescriptionEntertainmentBox.Visibility = Visibility.Hidden;
            ImageEntertainmentBox.Visibility = Visibility.Hidden;
            Color red = Color.FromArgb(255, 122, 62, 62);
            Color green = Color.FromArgb(255, 62, 122, 62); 


            game.Guesses[game.CurrentWord] = TypeWord.Text;
            if (TypeWord.Text == game.GameWords[game.CurrentWord].Syntax)
            {
                game.Rectangles[game.CurrentWord].Visibility = Visibility.Visible;
                game.Rectangles[game.CurrentWord].Fill = new SolidColorBrush(green);
                CorrectAnswer.Visibility = Visibility.Hidden;
            }
            else
            {
                game.Rectangles[game.CurrentWord].Visibility = Visibility.Visible;
                game.Rectangles[game.CurrentWord].Fill = new SolidColorBrush(red);
                CorrectAnswer.Visibility = Visibility.Visible;
                CorrectAnswer.Text = "The correct answer was " + game.GameWords[game.CurrentWord].Syntax;
            }
            game.CurrentWord++;
            Random rnd = new Random();
            int rand = rnd.Next(1, 13);

            if (game.GameWords[game.CurrentWord].ImageBase64 == null)
            {
                DescriptionEntertainmentBox.Visibility = Visibility.Visible;
                DescriptionEntertainmentBox.Text = game.GameWords[game.CurrentWord].Description;
                TypeWord.Text = "";
            }
            else
            {
                if (rand % 2 == 0)
                {
                    ImageEntertainmentBox.Source = game.GameWords[game.CurrentWord].DisplayImage();
                    ImageEntertainmentBox.Visibility = Visibility.Visible;
                    TypeWord.Text = "";
                }
                else
                {
                    DescriptionEntertainmentBox.Visibility = Visibility.Visible;
                    DescriptionEntertainmentBox.Text = game.GameWords[game.CurrentWord].Description;
                    TypeWord.Text = "";
                }
            }
            TypeWord.Text = game.Guesses[game.CurrentWord];
            NextButton.IsEnabled = game.enableNextButton();
            FinishButton.IsEnabled = game.enableFinishButton();
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            Color red = Color.FromArgb(255, 122, 62, 62);
            Color green = Color.FromArgb(255, 62, 122, 62);

            if (TypeWord.Text == game.GameWords[game.CurrentWord].Syntax)
            {
                game.Rectangles[game.CurrentWord].Visibility = Visibility.Visible;
                game.Rectangles[game.CurrentWord].Fill = new SolidColorBrush(green);
                CorrectAnswer.Visibility = Visibility.Hidden;
            }
            else
            {
                game.Rectangles[game.CurrentWord].Visibility = Visibility.Visible;
                game.Rectangles[game.CurrentWord].Fill = new SolidColorBrush(red);
                CorrectAnswer.Visibility = Visibility.Visible;
                CorrectAnswer.Text = "The correct answer was " + game.GameWords[game.CurrentWord].Syntax;
            }
            FinishText.Visibility = Visibility.Visible;
            Game.Visibility = Visibility.Hidden;

            TypeWord.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Collapsed;
            FinishButton.Visibility = Visibility.Collapsed;
            DescriptionEntertainmentBox.Visibility = Visibility.Collapsed;
            ImageEntertainmentBox.Visibility = Visibility.Collapsed;

            RestartButton.Visibility = Visibility.Visible;
            
        }
        private void RestartButtonClick(object sender, RoutedEventArgs e)
        {
            Game.Visibility = Visibility.Visible;
            game.Restart();

            RestartButton.Visibility = Visibility.Collapsed;
            AnswerBox.Visibility = Visibility.Collapsed;
            FinishText.Visibility = Visibility.Hidden;
            CorrectAnswer.Visibility = Visibility.Hidden;
            foreach(var rect in game.Rectangles)
            {
               rect.Visibility = Visibility.Hidden;
            }

            EntertainmentButtonClick(sender, e);
        }

        private void DescriptionEntertainmentBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.UpdateLayout();
            ResizeTextBoxToFitText(textBox);

        }

        private void DescriptionEntertainmentBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null) return;

            textBox.UpdateLayout();
            // Perform the text measurement and resizing here, as in the previous example
            ResizeTextBoxToFitText(textBox);
        }
    }
}

