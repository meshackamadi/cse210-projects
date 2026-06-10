using System;

namespace MindfulnessApp
{
    public class BreathingActivity : Activity
    {
        public BreathingActivity() : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        { }
        
        public override void Run()
        {
            DisplayStartingMessage();
            
            DateTime endTime = DateTime.Now.AddSeconds(_duration);
            
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
}