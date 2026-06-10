using System;

namespace ScriptureMemorizer
{
    public class Reference
    {
        private string book;
        private int startChapter;
        private int startVerse;
        private int? endVerse;
        
        // Constructor for single verse (e.g., "John 3:16")
        public Reference(string book, int chapter, int verse)
        {
            this.book = book;
            this.startChapter = chapter;
            this.startVerse = verse;
            this.endVerse = null;
        }
        
        // Constructor for verse range (e.g., "Proverbs 3:5-6")
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            this.book = book;
            this.startChapter = chapter;
            this.startVerse = startVerse;
            this.endVerse = endVerse;
        }
        
        // Constructor that parses a reference string
        public Reference(string referenceString)
        {
            ParseReference(referenceString);
        }
        
        private void ParseReference(string referenceString)
        {
            string[] parts = referenceString.Split(' ');
            if (parts.Length >= 2)
            {
                book = parts[0];
                string chapterVerse = parts[1];
                
                string[] chapterVerseParts = chapterVerse.Split(':');
                if (chapterVerseParts.Length == 2)
                {
                    startChapter = int.Parse(chapterVerseParts[0]);
                    string versePart = chapterVerseParts[1];
                    
                    if (versePart.Contains('-'))
                    {
                        string[] verses = versePart.Split('-');
                        startVerse = int.Parse(verses[0]);
                        endVerse = int.Parse(verses[1]);
                    }
                    else
                    {
                        startVerse = int.Parse(versePart);
                        endVerse = null;
                    }
                }
            }
        }
        
        public string GetReferenceString()
        {
            if (endVerse.HasValue)
            {
                return $"{book} {startChapter}:{startVerse}-{endVerse}";
            }
            else
            {
                return $"{book} {startChapter}:{startVerse}";
            }
        }
        
        public override string ToString()
        {
            return GetReferenceString();
        }
    }
}