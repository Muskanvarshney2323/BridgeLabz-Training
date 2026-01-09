using System;
using System.Collections.Generic;

class UserProfile
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }

    public List<Workout> Workouts { get; set; } = new List<Workout>();

    public void AddWorkout(Workout workout)
    {
        Workouts.Add(workout);
    }

    public void ShowWorkoutHistory()
    {
        Console.WriteLine($"\nWorkout History of {Name}:");

        foreach (Workout workout in Workouts)
        {
            workout.DisplayWorkout();

            if (workout is ITrackable trackable)
            {
                trackable.TrackProgress();
            }

            Console.WriteLine("-------------------------");
        }
    }
}
