using System;
using TechVilla.Models;
using TechVilla.Utils;

namespace TechVilla.Modules
{
    public static class Module2_ServiceEligibilityChecker
    {
        public static string DeterminePackage(Citizen c)
        {
            // nested conditions
            if (c.Age >= 65 || c.Income < 20000m)
            {
                return "Basic";
            }

            if (c.ResidencyYears >= 10)
            {
                // switch for quick mapping
                switch (c.Income)
                {
                    case < 30000m:
                        return "Silver";
                    case < 70000m:
                        return "Gold";
                    default:
                        return "Platinum";
                }
            }

            // default
            return c.Income > 50000m ? "Gold" : "Silver";
        }

        public static void RegisterFamily()
        {
            var countStr = InputHelper.ReadLine("How many family members to register? ");
            if (!int.TryParse(countStr, out var n) || n <= 0) return;

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Member #{i + 1}");
                var member = Module1_CitizenRegistration.Register();
                var pkg = DeterminePackage(member);
                Console.WriteLine($"Assigned package: {pkg}");
            }
        }
    }
}
