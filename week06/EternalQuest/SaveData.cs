using System.Collections.Generic;

namespace EternalQuest
{
    public class SaveData
    {
        public List<Goal> Goals { get; set; }
        public int TotalPoints { get; set; }
        public int GoalsCompleted { get; set; }
        public int ChecklistItemsCompleted { get; set; }
        public List<Achievement> Achievements { get; set; }

        // Parameterless constructor for JSON deserialization
        public SaveData()
        {
            Goals = new List<Goal>();
            Achievements = new List<Achievement>();
        }
    }
}