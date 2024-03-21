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

            TitleGame.Text = "Guess the word!";

            if (game.GameWords[game.CurrentWord].Item1.ImageBase64 == null)
            {
                DescriptionEntertainmentBox.Visibility = Visibility.Visible;
                DescriptionEntertainmentBox.Text = game.GameWords[game.CurrentWord].Item1.Description;
                TypeWord.Text = "";
            }
            else
            {
                if (game.GameWords[game.CurrentWord].Item2 % 2 == 0)
                {
                    ImageEntertainmentBox.Source = game.GameWords[game.CurrentWord].Item1.DisplayImage();
                    ImageEntertainmentBox.Visibility = Visibility.Visible;
                    TypeWord.Text = "";
                }
                else
                {
                    DescriptionEntertainmentBox.Visibility = Visibility.Visible;
                    DescriptionEntertainmentBox.Text = game.GameWords[game.CurrentWord].Item1.Description;
                    TypeWord.Text = "";
                }
            }
        }

        private void PreviousButtonClick(object sender, RoutedEventArgs e)
        {
            DescriptionEntertainmentBox.Visibility = Visibility.Hidden;
            ImageEntertainmentBox.Visibility = Visibility.Hidden;
            game.Guesses[game.CurrentWord] = TypeWord.Text;
            game.CurrentWord--;
            if (game.GameWords[game.CurrentWord].Item1.ImageBase64 == null)
            {
                DescriptionEntertainmentBox.Visibility = Visibility.Visible;
                DescriptionEntertainmentBox.Text = game.GameWords[game.CurrentWord].Item1.Description;
                TypeWord.Text = "";
            }
            else
            {
                if (game.GameWords[game.CurrentWord].Item2 % 2 == 0)
                {
                    ImageEntertainmentBox.Source = game.GameWords[game.CurrentWord].Item1.DisplayImage();
                    ImageEntertainmentBox.Visibility = Visibility.Visible;
                    TypeWord.Text = "";
                }
                else
                {
                    DescriptionEntertainmentBox.Visibility = Visibility.Visible;
                    DescriptionEntertainmentBox.Text = game.GameWords[game.CurrentWord].Item1.Description;
                    TypeWord.Text = "";
                }
            }
            TypeWord.Text = game.Guesses[game.CurrentWord];
            PreviousButton.IsEnabled = game.enablePreviousButton();
            NextButton.IsEnabled = game.enableNextButton();
            FinishButton.IsEnabled = game.enableFinishButton();
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            DescriptionEntertainmentBox.Visibility = Visibility.Hidden;
            ImageEntertainmentBox.Visibility = Visibility.Hidden;
            game.Guesses[game.CurrentWord] = TypeWord.Text;
            game.CurrentWord++;

            if (game.GameWords[game.CurrentWord].Item1.ImageBase64 == null)
            {
                DescriptionEntertainmentBox.Visibility = Visibility.Visible;
                DescriptionEntertainmentBox.Text = game.GameWords[game.CurrentWord].Item1.Description;
                TypeWord.Text = "";
            }
            else
            {
                if (game.GameWords[game.CurrentWord].Item2 % 2 == 0)
                {
                    ImageEntertainmentBox.Source = game.GameWords[game.CurrentWord].Item1.DisplayImage();
                    ImageEntertainmentBox.Visibility = Visibility.Visible;
                    TypeWord.Text = "";
                }
                else
                {
                    DescriptionEntertainmentBox.Visibility = Visibility.Visible;
                    DescriptionEntertainmentBox.Text = game.GameWords[game.CurrentWord].Item1.Description;
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
                if (game.GameWords[i].Item1.Syntax == game.Guesses[i])
                {
                    game.CorrectGuessCount++;
                }
            }

            TypeWord.Visibility = Visibility.Collapsed;
            PreviousButton.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Collapsed;
            FinishButton.Visibility = Visibility.Collapsed;
            DescriptionEntertainmentBox.Visibility = Visibility.Collapsed;
            ImageEntertainmentBox.Visibility = Visibility.Collapsed;

            AnswerBox.Visibility = Visibility.Visible;
            RestartButton.Visibility = Visibility.Visible;
            if (game.CorrectGuessCount > 1 || game.CorrectGuessCount == 0)
            {
                AnswerBox.Text = "You got " + game.CorrectGuessCount + " answers right out of 5\n";
            }
            else
            {
                AnswerBox.Text = "You got " + game.CorrectGuessCount + " answers right out of 5\n";
            }
            if(game.CorrectGuessCount == 3)
            {
                AnswerBox.Text += "Well done!";
            }
            else if (game.CorrectGuessCount <= 2)
            {
                AnswerBox.Text += "What a bummer..";
            }
            else
            {
                AnswerBox.Text += "Congratulations!";
            }
            TitleGame.Text = "Wanna play again?";
        }
        private void RestartButtonClick(object sender, RoutedEventArgs e)
        {
            game.Restart();

            RestartButton.Visibility = Visibility.Collapsed;
            AnswerBox.Visibility = Visibility.Collapsed;

            EntertainmentButtonClick(sender, e);
            TitleGame.Visibility = Visibility.Visible;
        }
    }
}

