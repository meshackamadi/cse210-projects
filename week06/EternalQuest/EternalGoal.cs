using System;
using System.Text.Json.Serialization;

namespace EternalQuest
{
    public class EternalGoal : Goal
    {
        [JsonInclude] private int _timesCompleted;
        [JsonInclude] private int _currentStreak;
        [JsonInclude] private DateTime _lastCompletedDate;

        private const int STREAK_BONUS_INTERVAL = 7;
        private const int STREAK_BONUS_POINTS = 50;

        public int CurrentStreak => _currentStreak;
        public int TimesCompleted => _timesCompleted;

        // Parameterless constructor for JSON deserialization
        public EternalGoal() : base()
        {
            _type = "Eternal";
            _timesCompleted = 0;
            _currentStreak = 0;
            _isComplete = false;
            _lastCompletedDate = DateTime.MinValue;
        }

        public EternalGoal(string name, string description, int points) 
            : base(name, description, points)
        {
            _type = "Eternal";
            _timesCompleted = 0;
            _currentStreak = 0;
            _isComplete = false; // Never truly complete
            _lastCompletedDate = DateTime.MinValue;
        }

        public override int GetPoints()
        {
            return _points + CalculateStreakBonus();
        }

        public override void RecordEvent()
        {
            _timesCompleted++;
            _isComplete = false; // Eternal goals never complete
            
            // Check streak
            DateTime today = DateTime.Today;
            if (_lastCompletedDate == DateTime.MinValue)
            {
                _currentStreak = 1;
            }
            else if ((today - _lastCompletedDate).Days == 1)
            {
                _currentStreak++;
            }
            else if ((today - _lastCompletedDate).Days > 1)
            {
                _currentStreak = 1;
            }
            _lastCompletedDate = today;
        }

        private int CalculateStreakBonus()
        {
            if (_currentStreak > 0 && _currentStreak % STREAK_BONUS_INTERVAL == 0)
            {
                return STREAK_BONUS_POINTS;
            }
            return 0;
        }

        public bool HasStreakBonus()
        {
            return _currentStreak > 0 && _currentStreak % STREAK_BONUS_INTERVAL == 0;
        }

        public override string GetStatus()
        {
            return "[∞]"; // Infinity symbol for eternal goals
        }

        public override string GetDetails()
        {
            string bonus = HasStreakBonus() ? $" 🔥 +{STREAK_BONUS_POINTS} bonus!" : "";
            return $"{GetStatus()} {_name} ({_description}) - Completed {_timesCompleted} times | Streak: {_currentStreak} days{bonus}";
        }
    }
}