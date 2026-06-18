using System;

namespace ExerciseTracking
{
    public abstract class Activity
    {
        // Private member variables (encapsulation)
        private DateTime _date;
        private int _minutes;

        // Properties to access private variables
        public DateTime Date => _date;
        public int Minutes => _minutes;

        // Constructor
        public Activity(DateTime date, int minutes)
        {
            _date = date;
            _minutes = minutes;
        }

        // Abstract methods - must be overridden by derived classes
        public abstract double GetDistance();
        public abstract double GetSpeed();
        public abstract double GetPace();

        // Virtual method that can be overridden but provides default implementation
        public virtual string GetSummary()
        {
            // Use the overridden methods to get calculations
            return $"{_date:dd MMM yyyy} {GetType().Name} ({_minutes} min) - " +
                   $"Distance: {GetDistance():F1} miles, " +
                   $"Speed: {GetSpeed():F1} mph, " +
                   $"Pace: {GetPace():F2} min per mile";
        }
    }
}