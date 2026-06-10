using System;
using System.Collections.Generic;

namespace MindfulnessApp
{
    public class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
        
        private Random _random = new Random();
        
        public ListingActivity() : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        { }
        
        public override void Run()
        {
            DisplayStartingMessage();
            
            Console.WriteLine("\nList as many responses as you can to the following prompt:");
            Console.WriteLine($"\n--- {_prompts[_random.Next(_prompts.Count)]} ---");
            
            Console.Write("\nYou may begin in: ");
            ShowCountdown(5);
            Console.WriteLine();
            
            List<string> items = new List<string>();
            DateTime endTime = DateTime.Now.AddSeconds(_duration);
            
            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                items.Add(Console.ReadLine());
            }
            
            Console.WriteLine($"\nYou listed {items.Count} items!");
            
            DisplayEndingMessage();
        }
    }
}