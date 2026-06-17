/*
 * ETERNAL QUEST PROGRAM - CREATIVITY REPORT
 * 
 * Exceeding Requirements:
 * 1. LEVEL SYSTEM: Users earn levels based on their total points (every 500 points = 1 level)
 *    - Each level up triggers a celebration message with the new level title
 *    - Level titles get progressively more epic (e.g., "Quest Initiate", "Eternal Champion")
 * 
 * 2. STREAK BONUSES: For eternal goals, users get bonus points for maintaining streaks
 *    - Every 7 consecutive days of completing an eternal goal = +50 bonus points
 *    - Streak tracking is saved and loaded with the goal data
 * 
 * 3. ACHIEVEMENT SYSTEM: Special achievements unlocked for milestones
 *    - "First Goal Complete" - Complete first goal
 *    - "Centurion" - Reach 1000 points
 *    - "Goal Master" - Complete 10 goals
 *    - "Perfect Week" - 7-day streak on any eternal goal
 *    - "Persistence" - Complete 50 checklist items
 * 
 * 4. NEGATIVE GOALS: Goals that lose points when recorded (for breaking bad habits)
 *    - Each time recorded, points are deducted
 *    - Counts toward completion when goal is "overcome"
 * 
 * 5. VISUAL PROGRESS: Progress bars in the goal list display for checklist goals
 *    - Shows visual bar: [████████░░░░░░░░] 50%
 * 
 * 6. SAVE/LOAD includes all custom data (levels, streaks, achievements)
 *    - Uses custom JSON converters for proper polymorphism deserialization
 * 
 * 7. COLORFUL CONSOLE OUTPUT with emoji for better user engagement
 */

using System;

namespace EternalQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            GoalManager manager = new GoalManager();
            manager.Start();
        }
    }
}