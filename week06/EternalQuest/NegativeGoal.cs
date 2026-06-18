using System.Text.Json.Serialization;

namespace EternalQuest
{
    public class NegativeGoal : Goal
    {
        [JsonInclude] private int _timesRecorded;
        [JsonInclude] private int _goalCount; // How many times to overcome

        public int TimesRecorded => _timesRecorded;
        public int GoalCount => _goalCount;

        // Parameterless constructor for JSON deserialization
        public NegativeGoal() : base()
        {
            _type = "Negative";
            _timesRecorded = 0;
            _goalCount = 0;
            _isComplete = false;
        }

        public NegativeGoal(string name, string description, int points, int goalCount) 
            : base(name, description, points)
        {
            _type = "Negative";
            _timesRecorded = 0;
            _goalCount = goalCount;
            _isComplete = false;
        }

        public override int GetPoints()
        {
            return -_points; // Negative points
        }

        public override void RecordEvent()
        {
            _timesRecorded++;
            if (_timesRecorded >= _goalCount)
            {
                _isComplete = true;
            }
        }

        public override string GetStatus()
        {
            return _isComplete ? "[X]" : "[ ]";
        }

        public override string GetDetails()
        {
            string progress = $"Recorded {_timesRecorded}/{_goalCount} times";
            return $"{GetStatus()} ⚠️ {_name} ({_description}) - -{_points} pts each | {progress}";
        }
    }
}