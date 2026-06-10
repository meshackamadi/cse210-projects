// ScriptureLibrary.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    public class ScriptureLibrary
    {
        private List<Scripture> scriptures;
        private Random random;
        
        public ScriptureLibrary()
        {
            scriptures = new List<Scripture>();
            random = new Random();
        }
        
        public int ScriptureCount
        {
            get { return scriptures.Count; }
        }
        
        public void AddScripture(string reference, string text)
        {
            scriptures.Add(new Scripture(reference, text));
        }
        
        public void AddScripture(Reference reference, string text)
        {
            scriptures.Add(new Scripture(reference, text));
        }
        
        public Scripture GetScripture(int index)
        {
            if (index >= 0 && index < scriptures.Count)
            {
                return scriptures[index];
            }
            return null;
        }
        
        public Scripture GetRandomScripture()
        {
            if (scriptures.Count == 0)
                return null;
                
            int index = random.Next(scriptures.Count);
            return new Scripture(scriptures[index]);
        }
        
        public void RemoveScripture(int index)
        {
            if (index >= 0 && index < scriptures.Count)
            {
                scriptures.RemoveAt(index);
            }
        }
        
        public void Clear()
        {
            scriptures.Clear();
        }
    }
}