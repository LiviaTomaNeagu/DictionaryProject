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
        internal List<Word> GameWords = new List<Word>();
        internal List<String> Guesses = new List<string> { "", "", "", "", "" };
        private int CurrentWord = 0;  // Track the current position in GameWords
        private int CorrectGuessCount = 0;  // Track the number of correct guesses

        private void ChooseRandomWords()
        {
            Random random = new Random();
            // GameWords = WordsList.OrderBy(word => random.Next()).Take(5).ToList();
            while (GameWords.Count < 5)
            {
                int randomIndex = random.Next(0, WordsList.Count - 1);
                if (!GameWords.Contains(WordsList[randomIndex]))
                {
                    GameWords.Add(WordsList[randomIndex]);
                }
            }
        }

        private void EntertainmentButtonClick(object sender, RoutedEventArgs e)
        {
            ChooseRandomWords();
            EntertainmentButton.Visibility = Visibility.Collapsed;
            TypeWord.Visibility = Visibility.Visible;
            PreviousButton.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Visible;
            FinishButton.Visibility = Visibility.Visible;
            DescriptionEntertainmentBox.Visibility = Visibility.Visible;
            DescriptionEntertainmentBox.Text = GameWords[CurrentWord].Description;
            TypeWord.Text = "";
        }

        private void PreviousButtonClick(object sender, RoutedEventArgs e)
        {
            Guesses[CurrentWord] = TypeWord.Text;
            CurrentWord--;
            DescriptionEntertainmentBox.Text = GameWords[CurrentWord].Description;
            TypeWord.Text = Guesses[CurrentWord];
            if (CurrentWord != 0)
            {
                PreviousButton.IsEnabled = true;
            }
            if (CurrentWord != 4)
            {
                NextButton.IsEnabled = true;
            }
            if (CurrentWord == 4)
            {
                NextButton.IsEnabled = false;
                FinishButton.IsEnabled = true;
            }
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            Guesses[CurrentWord] = TypeWord.Text;
            CurrentWord++;
            DescriptionEntertainmentBox.Text = GameWords[CurrentWord].Description;
            TypeWord.Text = Guesses[CurrentWord];
            if (CurrentWord != 0)
            {
                PreviousButton.IsEnabled = true;
            }
            if (CurrentWord != 4)
            {
                NextButton.IsEnabled = true;
            }
            if (CurrentWord == 4)
            {
                NextButton.IsEnabled = false;
                FinishButton.IsEnabled = true;
            }
        }

        private void FinishButtonClick(object sender, RoutedEventArgs e)
        {
            Guesses[CurrentWord] = TypeWord.Text;
            for (int i = 0; i < Guesses.Count; i++)
            {
                if (GameWords[i].Syntax == Guesses[i])
                {
                    CorrectGuessCount++;
                }
            }
        }
    }
}
