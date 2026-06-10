// Scripture.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    public class Scripture
    {
        private Reference reference;
        private List<Word> words;
        
        public Scripture(string reference, string text)
        {
            this.reference = new Reference(reference);
            InitializeWords(text);
        }
        
        public Scripture(Reference reference, string text)
        {
            this.reference = reference;
            InitializeWords(text);
        }
        
        public Scripture(string reference, string text, bool[] hiddenStatuses)
        {
            this.reference = new Reference(reference);
            InitializeWords(text);
            
            // Apply saved hidden statuses
            for (int i = 0; i < Math.Min(hiddenStatuses.Length, words.Count); i++)
            {
                words[i].IsHidden = hiddenStatuses[i];
            }
        }
        
        public Scripture(Scripture other)
        {
            this.reference = new Reference(other.reference.GetReferenceString());
            this.words = new List<Word>();
            foreach (var word in other.words)
            {
                Word newWord = new Word(word.GetOriginalText());
                newWord.IsHidden = word.IsHidden;
                this.words.Add(newWord);
            }
        }
        
        private void InitializeWords(string text)
        {
            words = new List<Word>();
            string[] wordArray = text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string word in wordArray)
            {
                // Clean punctuation but preserve it for display
                string cleanWord = word.Trim();
                words.Add(new Word(cleanWord));
            }
        }
        
        public Reference Reference
        {
            get { return reference; }
        }
        
        public bool AllWordsHidden
        {
            get { return words.All(w => w.IsHidden); }
        }
        
        public void HideRandomWords(int count)
        {
            // Get only words that are not already hidden
            var visibleWords = words.Where(w => !w.IsHidden).ToList();
            
            if (visibleWords.Count == 0)
                return;
            
            // Randomly select from visible words only
            Random random = new Random();
            int wordsToHide = Math.Min(count, visibleWords.Count);
            
            for (int i = 0; i < wordsToHide; i++)
            {
                int index = random.Next(visibleWords.Count);
                visibleWords[index].Hide();
                visibleWords.RemoveAt(index);
            }
        }
        
        public void Display()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{reference}\n");
            Console.ResetColor();
            
            foreach (Word word in words)
            {
                Console.Write(word.GetDisplayText() + " ");
            }
            Console.WriteLine("\n");
        }
        
        public void DisplayWithHints()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{reference} (Hints mode - first letters shown)\n");
            Console.ResetColor();
            
            foreach (Word word in words)
            {
                if (word.IsHidden)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.Write(word.GetDisplayText(true) + " ");
                Console.ResetColor();
            }
            Console.WriteLine("\n");
        }
        
        public bool[] GetHiddenStatuses()
        {
            return words.Select(w => w.IsHidden).ToArray();
        }
        
        public int GetHiddenWordCount()
        {
            return words.Count(w => w.IsHidden);
        }
        
        public int GetTotalWordCount()
        {
            return words.Count;
        }
        
        public string GetOriginalText()
        {
            return string.Join(" ", words.Select(w => w.GetOriginalText()));
        }
    }
}