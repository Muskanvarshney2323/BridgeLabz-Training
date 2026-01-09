using System;

abstract class Workout
{
    public int WorkoutId { get; set; }
    public string WorkoutName { get; set; }
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public double CaloriesBurned { get; set; }

    public virtual void DisplayWorkout()
    {
        Console.WriteLine($"Workout ID: {WorkoutId}");
        Console.WriteLine($"Workout Name: {WorkoutName}");
        Console.WriteLine($"Date: {Date.ToShortDateString()}");
        Console.WriteLine($"Duration: {Duration} minutes");
        Console.WriteLine($"Calories Burned: {CaloriesBurned}");
    }
}
