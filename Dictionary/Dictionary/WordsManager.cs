using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    class WordsManager
    {
        private List<WordDefinition> wordsList;
        public WordsManager()
        {
            wordsList = new List<WordDefinition>();
        }
        public void AddWord(string name, string category, string description)
        {
            WordDefinition newWord = new WordDefinition(name, category, description);
            wordsList.Add(newWord);
        }
        public WordDefinition SearchWordByName(string name)
        {
            return wordsList.Find(word => word.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public WordDefinition SearchWordByCategory(string category)
        {
            return wordsList.Find(word => word.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }
        public void RemoveWord(string name)
        {
            WordDefinition wordToRemove = SearchWordByName(name);
            if (wordToRemove != null)
            {
                wordsList.Remove(wordToRemove);
            }
        }
    }
}
