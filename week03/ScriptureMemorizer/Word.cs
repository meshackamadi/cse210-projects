using System;
using System.Linq;

namespace ScriptureMemorizer
{
    public class Word
    {
        private string originalText;
        private bool isHidden;
        
        public Word(string text)
        {
            originalText = text;
            isHidden = false;
        }
        
        public bool IsHidden
        {
            get { return isHidden; }
            set { isHidden = value; }
        }
        
        public string GetDisplayText(bool showHint = false)
        {
            if (isHidden)
            {
                if (showHint && originalText.Length > 0)
                {
                    // Show first letter as a hint
                    return originalText[0] + new string('_', originalText.Length - 1);
                }
                return new string('_', originalText.Length);
            }
            return originalText;
        }
        
        public string GetOriginalText()
        {
            return originalText;
        }
        
        public void Hide()
        {
            isHidden = true;
        }
        
        public void Reveal()
        {
            isHidden = false;
        }
    }
}