using System;
using System.Collections.Generic;

namespace OopsPractice.EncapsulationAbstractionPolymorphism
{
    interface IInsurable
    {
        double CalculateInsurance(int days);
        string GetInsuranceDetails();
    }

    abstract class Vehicle
    {
        private string _vehicleNumber;
        private string _type;
        protected double RentalRate;

        protected Vehicle(string number, string type, double rate)
        {
            _vehicleNumber = number; _type = type; RentalRate = rate;
        }

        public string VehicleNumber => _vehicleNumber;
        public string Type => _type;

        public abstract double CalculateRentalCost(int days);
    }

    class Car : Vehicle, IInsurable
    {
        public Car(string num) : base(num, "Car", 100) { }
        public override double CalculateRentalCost(int days) => RentalRate * days;
        public double CalculateInsurance(int days) => 50 + 10 * days;
        public string GetInsuranceDetails() => "Car insurance: base + per-day";
    }

    class Bike : Vehicle, IInsurable
    {
        public Bike(string num) : base(num, "Bike", 40) { }
        public override double CalculateRentalCost(int days) => RentalRate * days;
        public double CalculateInsurance(int days) => 20 + 5 * days;
        public string GetInsuranceDetails() => "Bike insurance";
    }

    class Truck : Vehicle
    {
        public Truck(string num) : base(num, "Truck", 200) { }
        public override double CalculateRentalCost(int days) => RentalRate * days;
    }

    class Program
    {
        static void Main()
        {
            var vehicles = new List<Vehicle>
            {
                new Car("C123"), new Bike("B456"), new Truck("T789")
            };

            foreach (var v in vehicles)
            {
                Console.WriteLine($"{v.Type} {v.VehicleNumber}: Rental for 3 days = {v.CalculateRentalCost(3):C}");
                if (v is IInsurable ins)
                {
                    Console.WriteLine($"  Insurance for 3 days = {ins.CalculateInsurance(3):C}");
                }
            }
        }
    }
}