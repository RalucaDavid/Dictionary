using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Dictionary
{
    public class WordsManager
    {
        private List<WordDefinition> wordsList;
        HashSet<string> categories = new HashSet<string>();
        public List<WordDefinition> WordsList
        {
            get { return wordsList; }
            set { wordsList = value; }
        }
        public HashSet<string> Categories
        {  
           get { return categories; } 
           set {  categories = value; }
        }
        public WordsManager()
        {
            wordsList = new List<WordDefinition>();
        }
        public void LoadWords(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            string word=string.Empty, category=string.Empty, description = string.Empty;
            int numberLine = 1;
            foreach (string line in lines)
            {
                if (numberLine % 2 == 1)
                {
                    string[] parts = line.Split(' ');
                    if (parts.Length == 2)
                    {
                        word = parts[0];
                        category = parts[1];
                    }
                }
                else
                {
                    description = line;
                    WordDefinition newWord = new WordDefinition(word, category, description);
                    wordsList.Add(newWord);
                    categories.Add(category);
                }
                numberLine++;
            }
        }
        public void SaveWords(string filePath)
        {
            List<string> lines = new List<string>();
            foreach (WordDefinition word in wordsList)
            {
                lines.Add($"{word.Name} {word.Category}");
                lines.Add(word.Description);
            }
            File.WriteAllLines(filePath, lines);
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
        public List<WordDefinition> SearchWordByCategory(string category)
        {
            return wordsList.Where(word => word.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
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
