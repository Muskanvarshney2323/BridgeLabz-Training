using System;

namespace csharp_inheritance
{
    class Vehicle
    {
        public int MaxSpeed { get; set; }
        public string FuelType { get; set; }

        public Vehicle(int maxSpeed, string fuelType)
        {
            MaxSpeed = maxSpeed;
            FuelType = fuelType;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Max Speed: {MaxSpeed}, Fuel: {FuelType}");
        }
    }

    class Car : Vehicle
    {
        public int SeatCapacity { get; set; }

        public Car(int maxSpeed, string fuelType, int seats) : base(maxSpeed, fuelType)
        {
            SeatCapacity = seats;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Car -> Speed: {MaxSpeed}, Fuel: {FuelType}, Seats: {SeatCapacity}");
        }
    }

    class Truck : Vehicle
    {
        public int PayloadCapacity { get; set; }

        public Truck(int maxSpeed, string fuelType, int payload) : base(maxSpeed, fuelType)
        {
            PayloadCapacity = payload;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Truck -> Speed: {MaxSpeed}, Fuel: {FuelType}, Payload: {PayloadCapacity} kg");
        }
    }

    class Motorcycle : Vehicle
    {
        public bool HasSidecar { get; set; }

        public Motorcycle(int maxSpeed, string fuelType, bool hasSidecar) : base(maxSpeed, fuelType)
        {
            HasSidecar = hasSidecar;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Motorcycle -> Speed: {MaxSpeed}, Fuel: {FuelType}, Sidecar: {HasSidecar}");
        }
    }

    class Program
    {
        static void Main()
        {
            Vehicle[] list = new Vehicle[]
            {
                new Car(180, "Petrol", 5),
                new Truck(120, "Diesel", 2000),
                new Motorcycle(160, "Petrol", false)
            };

            foreach (var v in list)
            {
                v.DisplayInfo();
            }
        }
    }
}