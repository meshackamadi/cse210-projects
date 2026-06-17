using System;

namespace EternalQuest
{
    public class LevelSystem
    {
        private const int POINTS_PER_LEVEL = 500;
        private string[] _titles = {
            "Quest Initiate",
            "Adventurer",
            "Hero",
            "Champion",
            "Legend",
            "Mythic",
            "Eternal Guardian",
            "Celestial Being"
        };

        public int GetLevel(int points)
        {
            return points / POINTS_PER_LEVEL + 1;
        }

        public string GetTitle(int points)
        {
            int level = GetLevel(points);
            int index = Math.Min(level - 1, _titles.Length - 1);
            return _titles[index];
        }

        public int GetPointsToNextLevel(int points)
        {
            int nextLevel = (points / POINTS_PER_LEVEL + 1) * POINTS_PER_LEVEL;
            return nextLevel - points;
        }
    }
}