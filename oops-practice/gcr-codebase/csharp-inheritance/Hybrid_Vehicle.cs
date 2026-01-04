using System;

namespace csharp_inheritance
{
    class Vehicle
    {
        public int MaxSpeed { get; set; }
        public string Model { get; set; }

        public Vehicle(int speed, string model)
        {
            MaxSpeed = speed;
            Model = model;
        }
    }

    interface Refuelable
    {
        void Refuel(double liters);
    }

    class PetrolVehicle : Vehicle, Refuelable
    {
        public double FuelLevel { get; set; }

        public PetrolVehicle(int speed, string model) : base(speed, model)
        {
            FuelLevel = 0;
        }

        public void Refuel(double liters)
        {
            FuelLevel += liters;
            Console.WriteLine($"Refueled {liters}L. Fuel level: {FuelLevel}L");
        }
    }

    class ElectricVehicle : Vehicle
    {
        public int BatteryPercent { get; set; }

        public ElectricVehicle(int speed, string model) : base(speed, model)
        {
            BatteryPercent = 100;
        }

        public void Charge(int percent)
        {
            BatteryPercent = Math.Min(100, BatteryPercent + percent);
            Console.WriteLine($"Charged {percent}%. Battery: {BatteryPercent}%");
        }
    }

    class Program
    {
        static void Main()
        {
            var pv = new PetrolVehicle(160, "Sedan");
            pv.Refuel(40);

            var ev = new ElectricVehicle(150, "Hatchback");
            ev.Charge(10);
        }
    }
}