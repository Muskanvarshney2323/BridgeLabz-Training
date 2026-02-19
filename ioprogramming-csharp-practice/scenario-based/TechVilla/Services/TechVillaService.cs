using System;
using TechVilla.Models;

namespace TechVilla.Services
{
    public class TechVillaService
    {
        public void RegisterCitizen(Citizen citizen)
        {
            if (string.IsNullOrWhiteSpace(citizen.Name))
                throw new ArgumentException("Name is required", nameof(citizen.Name));

            // Placeholder: registration logic (e.g., save to file/db)
            Console.WriteLine($"Citizen '{citizen.Name}' registered successfully.");
        }
    }
}
