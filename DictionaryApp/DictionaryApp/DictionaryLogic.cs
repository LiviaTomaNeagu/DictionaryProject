using Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DictionaryApp
{
    internal class DictionaryLogic
    {
        internal List<string> CategoriesList = new List<string>();
        internal List<Word> WordsList = new List<Word>();

        internal DictionaryLogic() { }

        public List<string> getCategoriesList() { return CategoriesList; }
        public List<Word> getWordsList() { return WordsList; }

        public void setCategoriesList(List<string> categoriesList) {  CategoriesList = categoriesList; }
        public void setWordsList(List<Word> wordsList) {  WordsList = wordsList; }

        public Word modifyWord(string syntax, string category, string description, ImageSource imageSource)
        {
            Word myWord = null;
            foreach (Word word in WordsList)
            {
                if (word.Syntax == syntax)
                    myWord = word;
            }

            if (myWord != null)
            {
                myWord.Description = description;
                if(!CategoriesList.Contains(category))
                {
                    CategoriesList.Add(category);
                    
                }
                myWord.Category = category;
                myWord.AddImage(imageSource);
            }
            return myWord;
            
        }

        public void addCategory(string category)
        {
            CategoriesList.Add(category);
        }

        public void addWord(Word word)
        {
            WordsList.Add(word);
        }

        public void clearCategory()
        {
            CategoriesList.Clear();
        }
    }
}
