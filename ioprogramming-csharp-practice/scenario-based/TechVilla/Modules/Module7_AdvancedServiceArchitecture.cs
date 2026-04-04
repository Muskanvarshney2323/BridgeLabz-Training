using System;
using TechVilla.Services;

namespace TechVilla.Modules
{
    public static class Module7_AdvancedServiceArchitecture
    {
        public static void Demo()
        {
            Console.WriteLine("Module 7: Advanced Service Architecture");

            // this usage: constructor assigns instance name via this
            var premium = new HealthcareService("PremiumHealth");

            // instanceof / is operator
            if (premium is Service svc)
            {
                Console.WriteLine($"Object is a Service: {svc}");
            }

            // factory
            var f = ServiceFactory.Create("education", "EduFactory");
            f.Register();

            Console.WriteLine($"Total services so far: {Service.TotalServices}");
        }
    }
}
