using System;

class Program
{
    static void Main()
    {
        UserProfile user = new UserProfile
        {
            UserId = 1,
            Name = "Muskan",
            Age = 21,
            Weight = 55
        };

        CardioWorkout cardio = new CardioWorkout
        {
            WorkoutId = 101,
            WorkoutName = "Morning Run",
            Date = DateTime.Now,
            Duration = 30,
            CaloriesBurned = 250,
            Distance = 4.5,
            CardioType = "Running"
        };

        StrengthWorkout strength = new StrengthWorkout
        {
            WorkoutId = 102,
            WorkoutName = "Strength Training",
            Date = DateTime.Now,
            Duration = 45,
            CaloriesBurned = 300,
            Sets = 4,
            Reps = 12,
            Weight = 20
        };

        user.AddWorkout(cardio);
        user.AddWorkout(strength);

        user.ShowWorkoutHistory();
    }
}
