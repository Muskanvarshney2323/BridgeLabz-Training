using System;
using TechVilla.Models;
using TechVilla.Utils;
using TechVilla.Services;

namespace TechVilla
{
    public class TechVillaMain
    {
        public void Run()
        {
            Console.WriteLine("Starting TechVilla module...");
            var service = new TechVillaService();
            var name = InputHelper.ReadLine("Enter citizen name: ");
            var citizen = new Citizen { Name = name };
            try
            {
                service.RegisterCitizen(citizen);
                Console.WriteLine("Registered: " + citizen.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
