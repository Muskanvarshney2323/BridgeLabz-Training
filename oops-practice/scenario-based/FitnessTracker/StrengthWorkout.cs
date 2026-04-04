using System;

class StrengthWorkout : Workout, ITrackable
{
    public int Sets { get; set; }
    public int Reps { get; set; }
    public double Weight { get; set; }

    public void TrackProgress()
    {
        Console.WriteLine($"Tracking Strength: {Sets} sets x {Reps} reps at {Weight} kg");
    }

    public override void DisplayWorkout()
    {
        base.DisplayWorkout();
        Console.WriteLine($"Sets: {Sets}");
        Console.WriteLine($"Reps: {Reps}");
        Console.WriteLine($"Weight: {Weight} kg");
    }
}
