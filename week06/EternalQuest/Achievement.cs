using System;
using System.Text.Json.Serialization;

namespace EternalQuest
{
    public class Achievement
    {
        [JsonInclude] public string Name { get; set; }
        [JsonInclude] public string Description { get; set; }
        [JsonInclude] public bool Unlocked { get; set; }
        [JsonInclude] public DateTime? UnlockedDate { get; set; }

        public Achievement(string name, string description)
        {
            Name = name;
            Description = description;
            Unlocked = false;
            UnlockedDate = null;
        }
    }
}