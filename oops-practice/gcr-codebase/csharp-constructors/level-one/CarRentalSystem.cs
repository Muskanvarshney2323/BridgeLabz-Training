using System;
class CarRental
{
    string customerName;
    string carModel;
    int rentalDays;
    // parameterized constructor
    public CarRental(string name, string model, int days)
    {
        this.customerName = name;
        this.carModel = model;
        this.rentalDays = days;
    }
    public void DisplayRentalDetails()
    {
        Console.WriteLine("Customer Name: " + customerName);
        Console.WriteLine("Car Model: " + carModel);
        Console.WriteLine("Rental Days: " + rentalDays);
    }
    public void TotalCost(double dailyRate)
    {
        double total = rentalDays * dailyRate;
        Console.WriteLine("Total Rental Cost for " + rentalDays + " days: " + total);
    }
    public static void Main()
    {
        // Creating object using parameterized constructor
        CarRental rental1 = new CarRental("John Doe", "Toyota Camry", 5);
        rental1.DisplayRentalDetails();
        rental1.TotalCost(40.0);
    }
}
