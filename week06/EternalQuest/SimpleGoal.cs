namespace EternalQuest
{
    public class SimpleGoal : Goal
    {
        public SimpleGoal(string name, string description, int points) 
            : base(name, description, points)
        {
            _type = "Simple";
        }

        public override int GetPoints()
        {
            return _isComplete ? _points : 0;
        }

        public override void RecordEvent()
        {
            if (!_isComplete)
            {
                base.RecordEvent();
            }
        }
    }
}