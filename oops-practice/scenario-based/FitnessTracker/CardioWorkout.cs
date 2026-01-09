using System;

class CardioWorkout : Workout, ITrackable
{
    public double Distance { get; set; }
    public string CardioType { get; set; }

    public void TrackProgress()
    {
        Console.WriteLine($"Tracking Cardio: {Distance} km - {CardioType}");
    }

    public override void DisplayWorkout()
    {
        base.DisplayWorkout();
        Console.WriteLine($"Cardio Type: {CardioType}");
        Console.WriteLine($"Distance: {Distance} km");
    }
}
