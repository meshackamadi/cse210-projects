using System;

namespace ExerciseTracking
{
    public class Cycling : Activity
    {
        // Private member variable
        private double _speed; // in mph

        // Constructor
        public Cycling(DateTime date, int minutes, double speed) 
            : base(date, minutes)
        {
            _speed = speed;
        }

        // Override methods
        public override double GetDistance()
        {
            // Distance = speed * (minutes / 60)
            return _speed * (Minutes / 60.0);
        }

        public override double GetSpeed()
        {
            // Speed is stored directly
            return _speed;
        }

        public override double GetPace()
        {
            // Pace = 60 / speed
            return 60 / _speed;
        }
    }
}