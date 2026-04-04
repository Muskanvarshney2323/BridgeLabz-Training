using System;
using TechVilla.Services;

namespace TechVilla.Modules
{
    public static class Module6_CityServicesFramework
    {
        public static void Demo()
        {
            Console.WriteLine("Module 6: City Services Framework");

            var h = new HealthcareService("CityHealth");
            var e = new EducationService("CityEdu");

            h.Register();
            e.Register();

            Console.WriteLine($"Total services created: {Service.TotalServices}");
        }
    }
}
