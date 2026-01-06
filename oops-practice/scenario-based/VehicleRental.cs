using System;

// Interface 
interface IRentable
{
    double CalculateRent(int days);
}

// Base Vehicle class with protected fields
abstract class Vehicle : IRentable
{
    protected string Make;
    protected string Model;
    protected int Year;
    protected double DailyRate;

    public Vehicle(string make, string model, int year, double dailyRate)
    {
        Make = make;
        Model = model;
        Year = year;
        DailyRate = dailyRate;
    }

    public string GetDescription()
    {
        return $"{Year} {Make} {Model}";
    }

    public virtual double CalculateRent(int days)
    {
        if (days <= 0) return 0;
        return DailyRate * days;
    }
}

class Bike : Vehicle
{
    public Bike(string make, string model, int year, double dailyRate)
        : base(make, model, year, dailyRate) { }

    public override double CalculateRent(int days)
    {
        // Bikes get a 10% discount for rentals longer than 3 days
        double baseRent = base.CalculateRent(days);
        if (days > 3) return baseRent * 0.9;
        return baseRent;
    }
}

class Car : Vehicle
{
    public Car(string make, string model, int year, double dailyRate)
        : base(make, model, year, dailyRate) { }

    public override double CalculateRent(int days)
    {
        // Cars get a 15% discount for rentals longer than 7 days
        double baseRent = base.CalculateRent(days);
        if (days > 7) return baseRent * 0.85;
        return baseRent;
    }
}

class Truck : Vehicle
{
    protected double LoadFeePerDay;

    public Truck(string make, string model, int year, double dailyRate, double loadFeePerDay)
        : base(make, model, year, dailyRate)
    {
        LoadFeePerDay = loadFeePerDay;
    }

    public override double CalculateRent(int days)
    {
        
        return base.CalculateRent(days) + (LoadFeePerDay * days);
    }
}

class Customer
{
    public string Name { get; set; }

    public Customer(string name)
    {
        Name = name;
    }

    public void Rent(IRentable rentable, int days)
    {
        double amount = rentable.CalculateRent(days);
        Console.WriteLine($"{Name} rented for {days} day(s). Total rent: {amount:C}");
    }
}

class Program
{
    static void Main()
    {
        Vehicle[] vehicles = new Vehicle[]
        {
            new Bike("Hero", "Splendor", 2020, 150),
            new Car("Toyota", "Corolla", 2019, 1200),
            new Truck("Tata", "407", 2018, 2500, 500)
        };

        Customer customer = new Customer("Ravi");

        foreach (var v in vehicles)
        {
            Console.WriteLine(v.GetDescription());
            Console.WriteLine($"Daily Rate (1 day): {v.CalculateRent(1):C}");

            int days = v is Bike ? 4 : v is Car ? 8 : 3;
            customer.Rent(v, days);
            Console.WriteLine();
        }
    }
}    