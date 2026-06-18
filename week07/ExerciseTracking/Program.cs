using System;
using System.Collections.Generic;

namespace ExerciseTracking
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🏋️‍♂️ EXERCISE TRACKING APP 🏋️‍♂️");
            Console.WriteLine("===============================");
            Console.WriteLine();

            // Create a list to hold all activities
            List<Activity> activities = new List<Activity>();

            // Create at least one activity of each type
            // Running activity (3 miles in 30 minutes)
            activities.Add(new Running(
                new DateTime(2022, 11, 3),
                30,
                3.0
            ));

            // Cycling activity (15 mph for 45 minutes)
            activities.Add(new Cycling(
                new DateTime(2022, 11, 4),
                45,
                15.0
            ));

            // Swimming activity (20 laps in 40 minutes)
            activities.Add(new Swimming(
                new DateTime(2022, 11, 5),
                40,
                20
            ));

            // Add a second running activity for variety
            activities.Add(new Running(
                new DateTime(2022, 11, 6),
                25,
                2.5
            ));

            // Add a second cycling activity
            activities.Add(new Cycling(
                new DateTime(2022, 11, 7),
                60,
                12.5
            ));

            // Display all activities
            Console.WriteLine("📊 EXERCISE LOG");
            Console.WriteLine("===============");
            Console.WriteLine();

            foreach (Activity activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}