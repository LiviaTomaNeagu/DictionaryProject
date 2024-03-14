using Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp
{
    internal class GameEntertainment
    {
        public List<Word> GameWords = new List<Word>();
        public List<String> Guesses = new List<string> { "", "", "", "", "" };
        public int CurrentWord = 0;  // Track the current position in GameWords
        public int CorrectGuessCount = 0;  // Track the number of correct guesses

        internal DictionaryLogic dictionary;

        internal GameEntertainment(DictionaryLogic dictionaryL)
        {
            dictionary = dictionaryL;
        }


        public void ChooseRandomWords()
        {
            Random random = new Random();
            // GameWords = WordsList.OrderBy(word => random.Next()).Take(5).ToList();
            while (GameWords.Count < 5 && dictionary.getWordsList().Count > 0)
            {
                int randomIndex = random.Next(0, dictionary.getWordsList().Count - 1);
                if (!GameWords.Contains(dictionary.getWordsList()[randomIndex]))
                {
                    GameWords.Add(dictionary.getWordsList()[randomIndex]);
                }
            }
        }

        public bool enablePreviousButton()
        {
            if (CurrentWord != 0)
            {
               return true;
            }
            else
            {
                return false;
            }

        }

        public bool enableNextButton()
        {
            if (CurrentWord != 4)
            {
                return true;
            }
            return false;
        }

        public bool enableFinishButton()
        {
            if (CurrentWord != 4)
            {
                return false;
            }
            return true;
        }

        public void Restart()
        {
            GameWords.Clear();
            Guesses = new List<string> { "", "", "", "", "" };
            CurrentWord = 0;
            CorrectGuessCount = 0;
        }
    }
}
