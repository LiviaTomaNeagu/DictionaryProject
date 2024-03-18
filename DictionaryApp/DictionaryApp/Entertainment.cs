using DictionaryApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dictionary
{
    public partial class MainWindow
    {

        internal GameEntertainment game = new GameEntertainment(dictionary);


        private void EntertainmentButtonClick(object sender, RoutedEventArgs e)
        {
            game.ChooseRandomWords();
            EntertainmentButton.Visibility = Visibility.Collapsed;
            TypeWord.Visibility = Visibility.Visible;

            PreviousButton.Visibility = Visibility.Visible;
            PreviousButton.IsEnabled = false;

            NextButton.Visibility = Visibility.Visible;
            NextButton.IsEnabled = true;

            FinishButton.Visibility = Visibility.Visible;
            FinishButton.IsEnabled = false;

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

        private void PreviousButtonClick(object sender, RoutedEventArgs e)
        {
            game.Guesses[game.CurrentWord] = TypeWord.Text;
            game.CurrentWord--;
            DescriptionEntertainmentBox.Text = game.GameWords[game.CurrentWord].Description;
            TypeWord.Text = game.Guesses[game.CurrentWord];
            PreviousButton.IsEnabled = game.enablePreviousButton();
            NextButton.IsEnabled = game.enableNextButton();
            FinishButton.IsEnabled = game.enableFinishButton();
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            game.Guesses[game.CurrentWord] = TypeWord.Text;
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
            PreviousButton.IsEnabled = game.enablePreviousButton();
            NextButton.IsEnabled = game.enableNextButton();
            FinishButton.IsEnabled = game.enableFinishButton();
        }

        private void FinishButtonClick(object sender, RoutedEventArgs e)
        {
            game.Guesses[game.CurrentWord] = TypeWord.Text;
            for (int i = 0; i < game.Guesses.Count; i++)
            {
                if (game.GameWords[i].Syntax == game.Guesses[i])
                {
                    game.CorrectGuessCount++;
                }
            }

            TypeWord.Visibility = Visibility.Collapsed;
            PreviousButton.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Collapsed;
            FinishButton.Visibility = Visibility.Collapsed;
            DescriptionEntertainmentBox.Visibility = Visibility.Collapsed;

            AnswerBox.Visibility = Visibility.Visible;
            RestartButton.Visibility = Visibility.Visible;
            AnswerBox.Text = "You got " + game.CorrectGuessCount + " answers right out of 5";
        }
        private void RestartButtonClick(object sender, RoutedEventArgs e)
        {
            game.Restart();

            RestartButton.Visibility = Visibility.Collapsed;
            AnswerBox.Visibility = Visibility.Collapsed;

            EntertainmentButtonClick(sender, e);
        }
    }
}

