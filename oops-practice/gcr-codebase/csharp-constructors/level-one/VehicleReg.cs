using System;
class Vehicle
{
    string ownerName;
    string vehicleType;
    static int registrationFee = 1000;
    public Vehicle(string owner, string type)
    {
        this.ownerName = owner;
        this.vehicleType = type;
    }
    public void DisplayDetails()
    {
        Console.WriteLine("Owner Name: " + ownerName);
        Console.WriteLine("Vehicle Type: " + vehicleType);
        Console.WriteLine("Registration Fee: " + registrationFee);
    }
    public void updateRegistrationFee(int newFee)
    {
        registrationFee = newFee;
        Console.WriteLine("Updated Registration Fee: " + registrationFee);
    }

    public static void Main()
    {
        Vehicle v1 = new Vehicle("John Doe", "car");
        v1.DisplayDetails();
        v1.updateRegistrationFee(1500);
        Vehicle v2 = new Vehicle("Jane Smith", "motorcycle");
        v2.DisplayDetails();
    }
    
}