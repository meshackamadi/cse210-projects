using System.Text.Json.Serialization;

namespace EternalQuest
{
    public class ChecklistGoal : Goal
    {
        [JsonInclude] private int _targetCount;
        [JsonInclude] private int _timesCompleted;
        [JsonInclude] private int _bonusPoints;

        public int TargetCount => _targetCount;
        public int TimesCompleted => _timesCompleted;

        public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints) 
            : base(name, description, points)
        {
            _type = "Checklist";
            _targetCount = targetCount;
            _bonusPoints = bonusPoints;
            _timesCompleted = 0;
            _isComplete = false;
        }

        public override int GetPoints()
        {
            return _points;
        }

        public override void RecordEvent()
        {
            if (!_isComplete)
            {
                _timesCompleted++;
                if (_timesCompleted >= _targetCount)
                {
                    _isComplete = true;
                }
            }
        }

        public override string GetStatus()
        {
            return _isComplete ? "[X]" : "[ ]";
        }

        public override string GetDetails()
        {
            string progress = $"Completed {_timesCompleted}/{_targetCount} times";
            string progressBar = GetProgressBar();
            string bonus = _isComplete ? $" 🎉 +{_bonusPoints} bonus!" : "";
            return $"{GetStatus()} {_name} ({_description}) - {progress} {progressBar} - {_points} pts each{bonus}";
        }

        public string GetProgressBar()
        {
            if (_targetCount == 0) return "";
            int percentage = (int)((_timesCompleted * 100.0) / _targetCount);
            int filled = (int)(percentage / 10);
            string bar = new string('█', filled) + new string('░', 10 - filled);
            return $"[{bar}] {percentage}%";
        }

        public int GetBonusPoints()
        {
            return _isComplete ? _bonusPoints : 0;
        }
    }
}