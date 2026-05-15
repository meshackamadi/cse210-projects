using System;

class Program
{
    static void Main()
    {
        Random random = new Random();
        bool playAgain = true;
        
        while (playAgain)
        {
            // Generate random number between 1 and 100
            int magicNumber = random.Next(1, 101);
            int guess = 0;
            int numberOfGuesses = 0;
            
            Console.WriteLine("\n=== GUESS MY NUMBER GAME ===");
            Console.WriteLine("I'm thinking of a number between 1 and 100...");
            
            // Loop until user guesses correctly
            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                numberOfGuesses++;
                
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"You guessed it!");
                    
                    // STRETCH: Show number of guesses
                    if (numberOfGuesses == 1)
                    {
                        Console.WriteLine($"It took you {numberOfGuesses} guess to find the number!");
                    }
                    else
                    {
                        Console.WriteLine($"It took you {numberOfGuesses} guesses to find the number!");
                    }
                }
            }
            
            // STRETCH: Ask if user wants to play again
            Console.Write("\nDo you want to play again? (yes/no): ");
            string response = Console.ReadLine().ToLower();
            
            if (response != "yes" && response != "y")
            {
                playAgain = false;
                Console.WriteLine("\nThanks for playing! Goodbye!");
            }
        }
    }
}