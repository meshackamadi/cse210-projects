using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptureMemorizer
{
    /*
     * EXCEEDING REQUIREMENTS REPORT:
     * 
     * 1. LIBRARY OF SCRIPTURES: Added a scripture library that can store multiple scriptures.
     *    Users can choose which scripture to memorize or get a random one.
     * 
     * 2. FILE LOADING: Scriptures can be loaded from a text file ("scriptures.txt"), allowing
     *    users to add their own scriptures without modifying the code. The file format is simple:
     *    "Reference|Text" (e.g., "John 3:16|For God so loved the world...")
     * 
     * 3. PROGRESS SAVING/LOADING: The program automatically saves memorization progress to a file.
     *    If you exit and restart, you can continue from where you left off. This solves the real-world
     *    challenge of losing progress when practicing over multiple sessions.
     * 
     * 4. REVEAL FEATURE: Users can type "reveal" to show a hint (the first letter of each hidden word)
     *    without fully showing the word. This helps when they're stuck but don't want to give up completely.
     * 
     * 5. DIFFICULTY LEVELS: Users can choose difficulty levels (Easy, Medium, Hard) that control
     *    how many words are hidden each time (1-2, 3-4, or 5-6 words respectively).
     * 
     * 6. STATISTICS TRACKING: The program tracks and displays how many words you've memorized
     *    and your progress percentage after each attempt.
     * 
     * 7. WORD SELECTION IMPROVEMENT: Randomly selects only from words that are not already hidden,
     *    making the memorization process more efficient.
     */

    class Program
    {
        private static ScriptureLibrary library;
        private static Scripture currentScripture;
        private static string saveFile = "memorization_progress.txt";
        private static DifficultyLevel difficulty = DifficultyLevel.Medium;

        static void Main(string[] args)
        {
            Console.Title = "Scripture Memorization Helper";

            // Load scriptures from file
            library = new ScriptureLibrary();
            LoadScripturesFromFile("scriptures.txt");

            // Add default scriptures if library is empty
            if (library.ScriptureCount == 0)
            {
                AddDefaultScriptures();
            }

            // Show welcome message and instructions
            ShowWelcomeScreen();

            // Choose difficulty
            ChooseDifficulty();

            // Check for saved progress
            if (File.Exists(saveFile))
            {
                Console.Write("\nSaved progress found. Load it? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    LoadProgress();
                }
                else
                {
                    ChooseScripture();
                }
            }
            else
            {
                ChooseScripture();
            }

            // Start memorization
            RunMemorization();
        }

        static void ShowWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║         SCRIPTURE MEMORIZATION HELPER                    ║");
            Console.WriteLine("║                                                          ║");
            Console.WriteLine("║  Commands:                                               ║");
            Console.WriteLine("║    [Enter] - Hide more words                             ║");
            Console.WriteLine("║    'quit'   - Exit and save progress                     ║");
            Console.WriteLine("║    'reveal' - Show first letters of hidden words         ║");
            Console.WriteLine("║    'stats'  - Show memorization statistics               ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void ChooseDifficulty()
        {
            Console.Clear();
            Console.WriteLine("Select Difficulty Level:");
            Console.WriteLine("1. Easy (Hide 1-2 words at a time)");
            Console.WriteLine("2. Medium (Hide 3-4 words at a time)");
            Console.WriteLine("3. Hard (Hide 5-6 words at a time)");
            Console.Write("\nYour choice (1-3): ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    difficulty = DifficultyLevel.Easy;
                    break;
                case "3":
                    difficulty = DifficultyLevel.Hard;
                    break;
                default:
                    difficulty = DifficultyLevel.Medium;
                    break;
            }

            Console.WriteLine($"\nDifficulty set to {difficulty}. Press any key to continue...");
            Console.ReadKey();
        }

        static void ChooseScripture()
        {
            Console.Clear();
            Console.WriteLine("Available Scriptures:\n");

            for (int i = 0; i < library.ScriptureCount; i++)
            {
                Console.WriteLine($"{i + 1}. {library.GetScripture(i).Reference}");
            }

            Console.WriteLine($"{library.ScriptureCount + 1}. Random Scripture");
            Console.Write("\nChoose a scripture (enter number): ");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= library.ScriptureCount + 1)
            {
                if (choice == library.ScriptureCount + 1)
                {
                    currentScripture = library.GetRandomScripture();
                }
                else
                {
                    currentScripture = new Scripture(library.GetScripture(choice - 1));
                }
            }
            else
            {
                currentScripture = library.GetRandomScripture();
            }

            Console.Clear();
            Console.WriteLine($"Selected: {currentScripture.Reference}");
            Console.WriteLine("\nPress any key to begin memorization...");
            Console.ReadKey();
        }

        static void LoadScripturesFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    string[] lines = File.ReadAllLines(filename);
                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                            continue;

                        string[] parts = line.Split('|');
                        if (parts.Length == 2)
                        {
                            string reference = parts[0].Trim();
                            string text = parts[1].Trim();
                            library.AddScripture(reference, text);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading scriptures: {ex.Message}");
                }
            }
        }

        static void AddDefaultScriptures()
        {
            library.AddScripture("John 3:16", "For God so loved the world that he gave his only Son, that whoever believes in him should not perish but have eternal life.");
            library.AddScripture("Proverbs 3:5-6", "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.");
            library.AddScripture("Philippians 4:13", "I can do all things through Christ who strengthens me.");
            library.AddScripture("Psalm 23:1", "The Lord is my shepherd; I shall not want.");
            library.AddScripture("Jeremiah 29:11", "For I know the plans I have for you, declares the Lord, plans to prosper you and not to harm you, plans to give you hope and a future.");
        }

        static void SaveProgress()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(saveFile))
                {
                    writer.WriteLine(currentScripture.Reference.GetReferenceString());
                    writer.WriteLine(currentScripture.GetOriginalText());
                    writer.WriteLine(string.Join("|", currentScripture.GetHiddenStatuses()));
                    writer.WriteLine(difficulty.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving progress: {ex.Message}");
            }
        }

        static void LoadProgress()
        {
            try
            {
                string[] lines = File.ReadAllLines(saveFile);
                if (lines.Length >= 3)
                {
                    string reference = lines[0];
                    string text = lines[1];
                    bool[] hiddenStatuses = lines[2].Split('|').Select(bool.Parse).ToArray();
                    difficulty = (DifficultyLevel)Enum.Parse(typeof(DifficultyLevel), lines[3]);

                    currentScripture = new Scripture(reference, text, hiddenStatuses);
                    Console.WriteLine("Progress loaded successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading progress: {ex.Message}");
                ChooseScripture();
            }
        }

        static void RunMemorization()
        {
            Random random = new Random();
            bool isRunning = true;

            while (isRunning && !currentScripture.AllWordsHidden)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Difficulty: {difficulty} | Type 'quit' to exit, 'reveal' for hint, 'stats' for progress\n");
                Console.ResetColor();

                currentScripture.Display();

                if (!currentScripture.AllWordsHidden)
                {
                    Console.Write("\nPress Enter to hide more words, or type a command: ");
                    string input = Console.ReadLine()?.ToLower().Trim();

                    switch (input)
                    {
                        case "quit":
                            SaveProgress();
                            Console.WriteLine("\nProgress saved. Goodbye!");
                            isRunning = false;
                            break;

                        case "reveal":
                            Console.Clear();
                            currentScripture.DisplayWithHints();
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                            break;

                        case "stats":
                            ShowStats();
                            break;

                        case "":
                            int wordsToHide = GetWordsToHide(difficulty, random);
                            currentScripture.HideRandomWords(wordsToHide);
                            break;

                        default:
                            Console.WriteLine("Unknown command. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
            }

            if (currentScripture.AllWordsHidden && isRunning)
            {
                Console.Clear();
                currentScripture.Display();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\n🎉 CONGRATULATIONS! You've memorized the entire scripture! 🎉");
                Console.ResetColor();
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();

                // Delete save file when complete
                if (File.Exists(saveFile))
                    File.Delete(saveFile);
            }
        }

        static int GetWordsToHide(DifficultyLevel difficulty, Random random)
        {
            switch (difficulty)
            {
                case DifficultyLevel.Easy:
                    return random.Next(1, 3);
                case DifficultyLevel.Hard:
                    return random.Next(5, 7);
                default:
                    return random.Next(3, 5);
            }
        }

        static void ShowStats()
        {
            int totalWords = currentScripture.GetTotalWordCount();
            int hiddenWords = currentScripture.GetHiddenWordCount();
            int visibleWords = totalWords - hiddenWords;
            double percentage = (double)hiddenWords / totalWords * 100;

            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    MEMORIZATION STATS                    ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║  Total Words:     {totalWords,37} ║");
            Console.WriteLine($"║  Memorized:       {hiddenWords,37} ║");
            Console.WriteLine($"║  Remaining:       {visibleWords,37} ║");
            Console.WriteLine($"║  Progress:        {percentage,37:F1}% ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
}