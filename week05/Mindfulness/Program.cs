using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    abstract class Activity
    {
        private string _name;
        private string _description;
        protected int _duration;

        // Constructor
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
            ShowSpinner(3); // Pause for several seconds with animation
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

    class BreathingActivity : Activity
    {

        public BreathingActivity() : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        { }

        public override void Run()
        {
            DisplayStartingMessage();

            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(_duration);

            while (DateTime.Now < endTime)
            {
                if (DateTime.Now >= endTime) break;
                Console.Write("\nBreathe in...");
                ShowCountdown(4);

                if (DateTime.Now >= endTime) break;
                Console.Write("\nBreathe out...");
                ShowCountdown(4);
            }

            DisplayEndingMessage();
        }
    }

    class ReflectingActivity : Activity
    {
        // PRIVATE member variables for prompts and questions (Encapsulation)
        private List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        private Random _random = new Random();

        public ReflectingActivity() : base(
            "Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        { }

        public override void Run()
        {
            DisplayStartingMessage();

            // Select a random prompt to show the user
            Console.WriteLine("\nConsider this prompt:");
            Console.WriteLine($"\n--- {_prompts[_random.Next(_prompts.Count)]} ---");
            Console.WriteLine("\nWhen you have something in mind, press enter to continue.");
            Console.ReadLine();

            // Show random questions until duration is reached
            DateTime endTime = DateTime.Now.AddSeconds(_duration);

            while (DateTime.Now < endTime)
            {
                string question = _questions[_random.Next(_questions.Count)];
                Console.Write($"\n> {question} ");
                ShowSpinner(8); // Pause with spinner animation
            }

            DisplayEndingMessage();
        }
    }

    class ListingActivity : Activity
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

            // Select a random prompt
            Console.WriteLine("\nList as many responses as you can to the following prompt:");
            Console.WriteLine($"\n--- {_prompts[_random.Next(_prompts.Count)]} ---");

            // Countdown to begin thinking (Functional Requirement)
            Console.Write("\nYou may begin in: ");
            ShowCountdown(5);
            Console.WriteLine();

            // User lists items until duration is reached
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

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness App\n");
                Console.WriteLine("Menu Options:");
                Console.WriteLine("  1. Breathing Activity");
                Console.WriteLine("  2. Reflecting Activity");
                Console.WriteLine("  3. Listing Activity");
                Console.WriteLine("  4. Quit");
                Console.Write("\nSelect a choice from the menu: ");

                string choice = Console.ReadLine();
                Activity activity = null;

                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;
                    case "2":
                        activity = new ReflectingActivity();
                        break;
                    case "3":
                        activity = new ListingActivity();
                        break;
                    case "4":
                        return; // Exit program
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        continue;
                }

                activity.Run();

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
            }
        }
    }
}
