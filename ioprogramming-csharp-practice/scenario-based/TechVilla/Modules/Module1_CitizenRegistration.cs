using System;
using TechVilla.Models;
using TechVilla.Utils;

namespace TechVilla.Modules
{
    public static class Module1_CitizenRegistration
    {
        public static Citizen Register()
        {
            var name = InputHelper.ReadLine("Name: ");
            var ageStr = InputHelper.ReadLine("Age: ");
            var incomeStr = InputHelper.ReadLine("Income: ");
            var residencyStr = InputHelper.ReadLine("Residency years: ");

            int.TryParse(ageStr, out var age);
            decimal.TryParse(incomeStr, out var income);
            int.TryParse(residencyStr, out var residency);

            // Basic validations
            if (age <= 0) Console.WriteLine("Warning: invalid age provided.");
            if (residency < 0) residency = 0;

            var citizen = new Citizen
            {
                Name = StringUtils.FormatName(name),
                Age = age,
                Income = income,
                ResidencyYears = residency
            };

            // Simple eligibility score
            var score = age / 10 + (int)(income / 10000) + residency / 5;
            Console.WriteLine($"Eligibility score: {score}");

            Console.WriteLine("Registered: " + citizen);
            return citizen;
        }
    }
}
