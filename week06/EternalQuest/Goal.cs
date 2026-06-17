using System.Text.Json.Serialization;

namespace EternalQuest
{
    public abstract class Goal
    {
        [JsonInclude] protected string _name;
        [JsonInclude] protected string _description;
        [JsonInclude] protected int _points;
        [JsonInclude] protected bool _isComplete;
        [JsonInclude] protected string _type;

        public string Name => _name;
        public string Description => _description;
        public int Points => _points;
        public bool IsComplete => _isComplete;
        public string Type => _type;

        public Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
            _isComplete = false;
            _type = "Base";
        }

        public virtual void RecordEvent()
        {
            _isComplete = true;
        }

        public virtual string GetStatus()
        {
            return _isComplete ? "[X]" : "[ ]";
        }

        public virtual string GetDetails()
        {
            return $"{GetStatus()} {_name} ({_description}) - {_points} pts";
        }

        public abstract int GetPoints();
        public virtual bool IsCompleted() => _isComplete;
    }
}