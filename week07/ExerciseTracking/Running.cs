using System;

namespace ExerciseTracking
{
    public class Running : Activity
    {
        // Private member variable
        private double _distance; // in miles

        // Constructor
        public Running(DateTime date, int minutes, double distance) 
            : base(date, minutes)
        {
            _distance = distance;
        }

        // Override methods
        public override double GetDistance()
        {
            return _distance;
        }

        public override double GetSpeed()
        {
            // Speed = (distance / minutes) * 60
            return (_distance / Minutes) * 60;
        }

        public override double GetPace()
        {
            // Pace = minutes / distance
            return Minutes / _distance;
        }
    }
}
