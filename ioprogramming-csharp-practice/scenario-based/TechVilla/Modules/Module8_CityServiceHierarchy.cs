using System;
using TechVilla.Services;

namespace TechVilla.Modules
{
    public static class Module8_CityServiceHierarchy
    {
        public static void Demo()
        {
            Console.WriteLine("Module 8: City Service Hierarchy");

            var emerg = new EmergencyService("RapidResponse");
            emerg.Register(); // override

            var s1 = new HealthcareService("H1");
            var s2 = new HealthcareService("H1");
            Console.WriteLine($"s1 equals s2: {s1.Equals(s2)}");
            Console.WriteLine($"s1 hash: {s1.GetHashCode()}, s2 hash: {s2.GetHashCode()}");
        }
    }
}
