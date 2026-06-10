using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    public abstract class Activity
    {
        private string _name;
        private string _description;
        protected int _duration;
        
        public Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }
        
        public void DisplayStartingMessage()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to the {_name}.\n");
            Console.WriteLine(_description);
            Console.Write("\nHow long, in seconds, would you like for your session? ");
            _duration = int.Parse(Console.ReadLine());
            
            Console.Clear();
            Console.WriteLine("Get ready...");
            ShowSpinner(3);
        }
        
        public void DisplayEndingMessage()
        {
            Console.WriteLine("\nWell done!!");
            ShowSpinner(2);
            Console.WriteLine($"You have completed another {_duration} seconds of the {_name}.");
            ShowSpinner(3);
        }
        
        protected void ShowSpinner(int seconds)
        {
            List<string> spinnerChars = new List<string> { "|", "/", "-", "\\" };
            DateTime endTime = DateTime.Now.AddSeconds(seconds);
            int i = 0;
            
            while (DateTime.Now < endTime)
            {
                Console.Write(spinnerChars[i % spinnerChars.Count]);
                Thread.Sleep(200);
                Console.Write("\b \b");
                i++;
            }
        }
        
        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }
        
        public abstract void Run();
    }
}