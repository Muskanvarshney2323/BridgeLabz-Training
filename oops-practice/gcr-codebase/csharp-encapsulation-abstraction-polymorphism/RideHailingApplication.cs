using System;
using System.Collections.Generic;

namespace OopsPractice.EncapsulationAbstractionPolymorphism
{
    interface IGPS
    {
        string GetCurrentLocation();
        void UpdateLocation(string location);
    }

    abstract class Vehicle
    {
        private string _vehicleId;
        private string _driverName;
        protected double RatePerKm;

        protected Vehicle(string id, string driver, double rate)
        {
            _vehicleId = id; _driverName = driver; RatePerKm = rate;
        }

        public string VehicleId => _vehicleId;
        public string DriverName => _driverName;

        public abstract double CalculateFare(double distance);
        public void GetVehicleDetails() => Console.WriteLine($"{DriverName} - {VehicleId}");
    }

    class Car : Vehicle, IGPS
    {
        private string _location = "Unknown";
        public Car(string id, string driver) : base(id, driver, 10) { }
        public override double CalculateFare(double distance) => RatePerKm * distance + 50; // base
        public string GetCurrentLocation() => _location;
        public void UpdateLocation(string location) => _location = location;
    }

    class Bike : Vehicle, IGPS
    {
        private string _location = "Unknown";
        public Bike(string id, string driver) : base(id, driver, 6) { }
        public override double CalculateFare(double distance) => RatePerKm * distance;
        public string GetCurrentLocation() => _location;
        public void UpdateLocation(string location) => _location = location;
    }

    class Auto : Vehicle
    {
        public Auto(string id, string driver) : base(id, driver, 5) { }
        public override double CalculateFare(double distance) => RatePerKm * distance + 10;
    }

    class Program
    {
        static void Main()
        {
            var vehicles = new List<Vehicle> { new Car("V1","Karan"), new Bike("V2","Deep"), new Auto("V3","Raju") };
            foreach(var v in vehicles)
            {
                Console.WriteLine($"Fare for 12km by {v.GetType().Name}: {v.CalculateFare(12):C}");
            }
        }
    }
}