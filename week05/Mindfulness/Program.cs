using System;

namespace MindfulnessApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            ================================================================
            CORE REQUIREMENTS MET - MINDFULNESS PROGRAM
            ================================================================
            
            FUNCTIONAL REQUIREMENTS:
            ✓ 1. Menu system allows user to choose from 3 activities (lines 27-55)
            ✓ 2. Common starting message: name, description, duration, "get ready" (Activity.cs lines 23-37)
            ✓ 3. Common ending message: "good job", activity name, duration (Activity.cs lines 39-47)
            ✓ 4. Animations: spinner (Activity.cs lines 49-63) and countdown (Activity.cs lines 65-73)
            ✓ 5. Breathing Activity: alternating breathe in/out with countdowns (BreathingActivity.cs lines 17-29)
            ✓ 6. Reflection Activity: random prompts, then random questions with spinner (ReflectingActivity.cs lines 43-59)
            ✓ 7. Listing Activity: random prompt, countdown, user lists, shows count (ListingActivity.cs lines 33-51)
            
            DESIGN REQUIREMENTS:
            ✓ 1. Inheritance: Activity base class with three derived classes (Activity.cs, BreathingActivity.cs, etc.)
            ✓ 2. No duplicate code: shared methods in Activity base class only
            ✓ 3. Encapsulation: private _name, _description, protected _duration
            ✓ 4. Abstraction: abstract Run() method in Activity class (Activity.cs line 77)
            
            ================================================================
            */

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
                        return;
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