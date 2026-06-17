using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace EternalQuest
{
    public class GoalManager
    {
        private List<Goal> _goals;
        private int _totalPoints;
        private LevelSystem _levelSystem;
        private List<Achievement> _achievements;
        private int _goalsCompleted;
        private int _checklistItemsCompleted;

        public GoalManager()
        {
            _goals = new List<Goal>();
            _totalPoints = 0;
            _levelSystem = new LevelSystem();
            _achievements = new List<Achievement>();
            _goalsCompleted = 0;
            _checklistItemsCompleted = 0;
            InitializeAchievements();
        }

        private void InitializeAchievements()
        {
            _achievements = new List<Achievement>
            {
                new Achievement("First Goal!", "Complete your first goal"),
                new Achievement("Centurion", "Reach 1000 total points"),
                new Achievement("Goal Master", "Complete 10 goals total"),
                new Achievement("Perfect Week", "Maintain a 7-day streak on any eternal goal"),
                new Achievement("Persistence", "Complete 50 checklist items")
            };
        }

        public void Start()
        {
            Console.Clear();
            Console.WriteLine("🌟 ETERNAL QUEST - Personal Goal Tracker 🌟");
            Console.WriteLine("============================================");
            Console.WriteLine();
            
            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        CreateGoal();
                        break;
                    case "2":
                        RecordEvent();
                        break;
                    case "3":
                        ListGoals();
                        break;
                    case "4":
                        DisplayScore();
                        break;
                    case "5":
                        DisplayAchievements();
                        break;
                    case "6":
                        SaveData();
                        break;
                    case "7":
                        LoadData();
                        break;
                    case "8":
                        running = false;
                        Console.WriteLine("🌟 Keep pursuing your eternal quest! 🌟");
                        break;
                    default:
                        Console.WriteLine("❌ Invalid option. Please try again.");
                        break;
                }
                Console.WriteLine();
            }
        }

        private void DisplayMenu()
        {
            DisplayHeader();
            Console.WriteLine("1. ✨ Create New Goal");
            Console.WriteLine("2. 📝 Record Goal Event");
            Console.WriteLine("3. 📋 List All Goals");
            Console.WriteLine("4. 👑 Display Score");
            Console.WriteLine("5. 🏆 Show Achievements");
            Console.WriteLine("6. 💾 Save Goals");
            Console.WriteLine("7. 📂 Load Goals");
            Console.WriteLine("8. 🚪 Exit");
            Console.Write("\nChoose an option: ");
        }

        private void DisplayHeader()
        {
            int level = _levelSystem.GetLevel(_totalPoints);
            string title = _levelSystem.GetTitle(_totalPoints);
            int pointsToNext = _levelSystem.GetPointsToNextLevel(_totalPoints);
            
            Console.WriteLine($"👑 {title} (Level {level})");
            Console.WriteLine($"⭐ Points: {_totalPoints}");
            if (pointsToNext > 0)
                Console.WriteLine($"📈 Next level in {pointsToNext} points");
            Console.WriteLine($"📊 Goals: {_goals.Count} total, {_goalsCompleted} completed");
            Console.WriteLine("─────────────────────────────");
        }

        private void DisplayScore()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine();
            Console.WriteLine("📊 DETAILED STATS");
            Console.WriteLine("─────────────────");
            Console.WriteLine($"Total Points: {_totalPoints}");
            Console.WriteLine($"Level: {_levelSystem.GetLevel(_totalPoints)} - {_levelSystem.GetTitle(_totalPoints)}");
            Console.WriteLine($"Goals Completed: {_goalsCompleted}");
            
            // Show goal type breakdown
            var simple = _goals.OfType<SimpleGoal>().Count();
            var eternal = _goals.OfType<EternalGoal>().Count();
            var checklist = _goals.OfType<ChecklistGoal>().Count();
            var negative = _goals.OfType<NegativeGoal>().Count();
            
            Console.WriteLine($"\nGoal Breakdown:");
            Console.WriteLine($"  Simple: {simple}");
            Console.WriteLine($"  Eternal: {eternal}");
            Console.WriteLine($"  Checklist: {checklist}");
            Console.WriteLine($"  Negative: {negative}");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void CreateGoal()
        {
            Console.Clear();
            Console.WriteLine("📝 CREATE NEW GOAL");
            Console.WriteLine("==================");
            Console.WriteLine();
            Console.WriteLine("Select goal type:");
            Console.WriteLine("1. Simple (complete once)");
            Console.WriteLine("2. Eternal (never complete, repeatable)");
            Console.WriteLine("3. Checklist (complete multiple times)");
            Console.WriteLine("4. Negative (lose points for bad habits)");
            Console.Write("\nChoose type: ");

            string type = Console.ReadLine();
            Console.Write("Goal name: ");
            string name = Console.ReadLine();
            Console.Write("Description: ");
            string desc = Console.ReadLine();

            try
            {
                switch (type)
                {
                    case "1":
                        Console.Write("Points when completed: ");
                        int points = int.Parse(Console.ReadLine());
                        _goals.Add(new SimpleGoal(name, desc, points));
                        break;
                    case "2":
                        Console.Write("Points per completion: ");
                        int ePoints = int.Parse(Console.ReadLine());
                        _goals.Add(new EternalGoal(name, desc, ePoints));
                        break;
                    case "3":
                        Console.Write("Points per completion: ");
                        int cPoints = int.Parse(Console.ReadLine());
                        Console.Write("Target completions: ");
                        int target = int.Parse(Console.ReadLine());
                        Console.Write("Bonus points for completion: ");
                        int bonus = int.Parse(Console.ReadLine());
                        _goals.Add(new ChecklistGoal(name, desc, cPoints, target, bonus));
                        break;
                    case "4":
                        Console.Write("Points lost per occurrence: ");
                        int nPoints = int.Parse(Console.ReadLine());
                        Console.Write("How many times to overcome? ");
                        int goalCount = int.Parse(Console.ReadLine());
                        _goals.Add(new NegativeGoal(name, desc, nPoints, goalCount));
                        break;
                    default:
                        Console.WriteLine("❌ Invalid type");
                        return;
                }

                Console.WriteLine($"✅ Goal '{name}' created successfully!");
            }
            catch (FormatException)
            {
                Console.WriteLine("❌ Invalid input. Please enter numbers where required.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void RecordEvent()
        {
            Console.Clear();
            Console.WriteLine("📝 RECORD GOAL EVENT");
            Console.WriteLine("====================");
            Console.WriteLine();

            if (_goals.Count == 0)
            {
                Console.WriteLine("❌ No goals created yet!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            // Show goals with numbers
            int index = 1;
            foreach (var goal in _goals)
            {
                Console.WriteLine($"{index}. {goal.GetDetails()}");
                index++;
            }

            Console.Write("\nSelect goal number to record: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > _goals.Count)
            {
                Console.WriteLine("❌ Invalid choice");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Goal selectedGoal = _goals[choice - 1];
            int points = selectedGoal.GetPoints();
            
            if (selectedGoal is SimpleGoal && selectedGoal.IsCompleted())
            {
                Console.WriteLine("⚠️ This goal is already completed!");
            }
            else
            {
                selectedGoal.RecordEvent();
                _totalPoints += points;

                // Check for streak bonuses
                if (selectedGoal is EternalGoal eternal)
                {
                    if (eternal.HasStreakBonus())
                    {
                        Console.WriteLine($"🔥 STREAK BONUS! +50 points for {eternal.CurrentStreak}-day streak!");
                        _totalPoints += 50;
                    }
                    
                    // Check Perfect Week achievement
                    if (eternal.CurrentStreak >= 7)
                    {
                        UnlockAchievement("Perfect Week");
                    }
                }

                // Track completed goals
                if (selectedGoal.IsCompleted())
                {
                    _goalsCompleted++;
                    UnlockAchievement("First Goal!");
                }

                // Track checklist completions
                if (selectedGoal is ChecklistGoal checklist)
                {
                    _checklistItemsCompleted += 1;
                    if (_checklistItemsCompleted >= 50)
                    {
                        UnlockAchievement("Persistence");
                    }
                }

                // Check for achievements
                if (_totalPoints >= 1000)
                {
                    UnlockAchievement("Centurion");
                }
                if (_goalsCompleted >= 10)
                {
                    UnlockAchievement("Goal Master");
                }

                Console.WriteLine($"✅ Recorded! Earned {points} points!");
                
                // Level up check
                int newLevel = _levelSystem.GetLevel(_totalPoints);
                Console.WriteLine($"🌟 Current total: {_totalPoints} points - {_levelSystem.GetTitle(_totalPoints)} (Level {newLevel})");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void ListGoals()
        {
            Console.Clear();
            Console.WriteLine("📋 GOALS LIST");
            Console.WriteLine("==============");
            Console.WriteLine();

            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals created yet.");
            }
            else
            {
                foreach (var goal in _goals)
                {
                    Console.WriteLine(goal.GetDetails());
                    if (goal is ChecklistGoal cl)
                    {
                        Console.WriteLine($"   {cl.GetProgressBar()}");
                    }
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void DisplayAchievements()
        {
            Console.Clear();
            Console.WriteLine("🏆 ACHIEVEMENTS");
            Console.WriteLine("================");
            Console.WriteLine();

            var unlocked = _achievements.Where(a => a.Unlocked).ToList();
            var locked = _achievements.Where(a => !a.Unlocked).ToList();

            Console.WriteLine("UNLOCKED:");
            if (unlocked.Count == 0)
                Console.WriteLine("  (None yet)");
            else
            {
                foreach (var a in unlocked)
                    Console.WriteLine($"  ✅ {a.Name} - {a.Description} (Unlocked: {a.UnlockedDate?.ToString("MM/dd/yyyy")})");
            }

            Console.WriteLine("\nLOCKED:");
            if (locked.Count == 0)
                Console.WriteLine("  All unlocked!");
            else
            {
                foreach (var a in locked)
                    Console.WriteLine($"  🔒 {a.Name} - {a.Description}");
            }

            Console.WriteLine($"\nProgress: {unlocked.Count}/{_achievements.Count} achievements unlocked");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void UnlockAchievement(string name)
        {
            var ach = _achievements.Find(a => a.Name == name && !a.Unlocked);
            if (ach != null)
            {
                ach.Unlocked = true;
                ach.UnlockedDate = DateTime.Now;
                Console.WriteLine($"🏆 ACHIEVEMENT UNLOCKED: {ach.Name}! - {ach.Description}");
            }
        }

        private void SaveData()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new GoalConverter() }
                };

                var data = new SaveData
                {
                    Goals = _goals,
                    TotalPoints = _totalPoints,
                    GoalsCompleted = _goalsCompleted,
                    ChecklistItemsCompleted = _checklistItemsCompleted,
                    Achievements = _achievements
                };
                
                string json = JsonSerializer.Serialize(data, options);
                File.WriteAllText("eternal_quest_save.json", json);
                Console.WriteLine("✅ Data saved successfully!");
                Console.WriteLine($"   Saved {_goals.Count} goals and {_totalPoints} points");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error saving data: {ex.Message}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void LoadData()
        {
            try
            {
                if (!File.Exists("eternal_quest_save.json"))
                {
                    Console.WriteLine("❌ No save file found.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }

                var options = new JsonSerializerOptions
                {
                    Converters = { new GoalConverter() }
                };

                string json = File.ReadAllText("eternal_quest_save.json");
                var data = JsonSerializer.Deserialize<SaveData>(json, options);
                
                if (data != null)
                {
                    _goals = data.Goals ?? new List<Goal>();
                    _totalPoints = data.TotalPoints;
                    _goalsCompleted = data.GoalsCompleted;
                    _checklistItemsCompleted = data.ChecklistItemsCompleted;
                    _achievements = data.Achievements ?? new List<Achievement>();
                    
                    Console.WriteLine($"✅ Data loaded successfully!");
                    Console.WriteLine($"   {_goals.Count} goals loaded");
                    Console.WriteLine($"   Score: {_totalPoints} points");
                    Console.WriteLine($"   Goals completed: {_goalsCompleted}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error loading data: {ex.Message}");
                Console.WriteLine("   The save file may be corrupted or from an older version.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}