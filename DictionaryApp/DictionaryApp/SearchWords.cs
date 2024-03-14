using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp
{
    internal class SearchWords
    {
        public DictionaryLogic dictionary;

        internal SearchWords(DictionaryLogic dictionary)
        {
            this.dictionary = dictionary;
        }

        public List<string> getFilteredWords(string searchText, string selectedCategory)
        {
            searchText = searchText.ToLower();

            var filteredWords = dictionary.getWordsList()
                .Where(word =>
                    ((selectedCategory == null) || word.Category == selectedCategory) &&
                    (string.IsNullOrWhiteSpace(searchText) || word.Syntax.ToLower().StartsWith(searchText)))
                .Select(word => word.Syntax)
                .ToList();

            if (filteredWords.Count == 0)
            {
                filteredWords.Add("No words found. :(");
            }
            return filteredWords;
        }
    }
}
