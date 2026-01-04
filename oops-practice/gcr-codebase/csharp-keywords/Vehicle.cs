using System;

namespace csharp_keywords
{
    class Vehicle
    {
        public static double RegistrationFee = 50.0;

        public readonly string RegistrationNumber;
        public string OwnerName;
        public string VehicleType;

        public Vehicle(string OwnerName, string VehicleType, string RegistrationNumber)
        {
            this.OwnerName = OwnerName;
            this.VehicleType = VehicleType;
            this.RegistrationNumber = RegistrationNumber; // readonly
        }

        public static void UpdateRegistrationFee(double newFee)
        {
            RegistrationFee = newFee;
        }

        public void DisplayDetails(object obj)
        {
            if (obj is Vehicle v)
            {
                Console.WriteLine("--- Vehicle ---");
                Console.WriteLine($"Owner: {v.OwnerName}");
                Console.WriteLine($"Type: {v.VehicleType}");
                Console.WriteLine($"Reg No: {v.RegistrationNumber}");
                Console.WriteLine($"Fee: {RegistrationFee}");
            }
            else
            {
                Console.WriteLine("Object is not a Vehicle instance.");
            }
        }

        static void Main()
        {
            var veh = new Vehicle("Rita", "Car", "REG-9001");
            veh.DisplayDetails(veh);
            Vehicle.UpdateRegistrationFee(75.0);
            Console.WriteLine("Updated fee: " + Vehicle.RegistrationFee);
        }
    }
}