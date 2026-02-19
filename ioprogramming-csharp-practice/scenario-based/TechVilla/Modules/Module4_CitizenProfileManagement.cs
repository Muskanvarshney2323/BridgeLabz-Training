using System;
using TechVilla.Models;
using TechVilla.Utils;

namespace TechVilla.Modules
{
    public static class Module4_CitizenProfileManagement
    {
        public static void UpdateProfile(Citizen c)
        {
            Console.WriteLine("Current: " + c);
            var newName = InputHelper.ReadLine("New name (leave empty to keep): ");
            if (!string.IsNullOrWhiteSpace(newName))
            {
                // pass-by-value demonstration: string assignment
                c.Name = StringUtils.FormatName(newName);
            }

            var newEmail = InputHelper.ReadLine("Email (optional): ");
            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                var ok = StringUtils.IsValidEmail(newEmail);
                Console.WriteLine(ok ? "Email looks valid" : "Email invalid format");
            }

            Console.WriteLine("Updated: " + c);
        }
    }
}
