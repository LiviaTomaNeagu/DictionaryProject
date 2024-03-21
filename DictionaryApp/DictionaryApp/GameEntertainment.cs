using Dictionary;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp
{
    internal class GameEntertainment
    {
        public List<Tuple<Word, int>> GameWords = new List<Tuple<Word, int>>();
        public List<string> Guesses = new List<string> { "", "", "", "", "" };
        public int CurrentWord = 0;
        public int CorrectGuessCount = 0;

        internal DictionaryLogic dictionary;

        internal GameEntertainment(DictionaryLogic dictionaryL)
        {
            dictionary = dictionaryL;
        }


        public void ChooseRandomWords()
        {
            Random random = new Random();
            while (GameWords.Count < 5 && dictionary.getWordsList().Count > 0)
            {
                int randomIndex = random.Next(0, dictionary.getWordsList().Count - 1);
                int rand = random.Next(1, 13);
                bool exists = false;
                foreach (Tuple<Word, int> word in GameWords)
                {
                    if (dictionary.getWordsList()[randomIndex] == word.Item1)
                    {
                        exists = true;
                    }
                }
                if (exists == false)
                {
                    GameWords.Add(new Tuple<Word, int>(dictionary.getWordsList()[randomIndex], rand));
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
