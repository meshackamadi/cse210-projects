using System;

namespace ExerciseTracking
{
    public class Swimming : Activity
    {
        // Private member variables
        private int _laps;
        private const double LAP_LENGTH_METERS = 50;
        private const double METERS_TO_MILES = 0.000621371; // 1 meter = 0.000621371 miles

        // Constructor
        public Swimming(DateTime date, int minutes, int laps)
            : base(date, minutes)
        {
            _laps = laps;
        }

        // Override methods
        public override double GetDistance()
        {
            // Distance (miles) = swimming laps * 50 meters * meters->miles
            return _laps * LAP_LENGTH_METERS * METERS_TO_MILES;
        }

        public override double GetSpeed()
        {
            // Speed = (distance / minutes) * 60
            return (GetDistance() / Minutes) * 60;
        }

        public override double GetPace()
        {
            // Pace = minutes / distance
            return Minutes / GetDistance();
        }
    }
}