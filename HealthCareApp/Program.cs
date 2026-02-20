﻿using System;
//using HealthCareClinicSystem.Utilities;
//using HealthCareClinicSystem.Views;

namespace HealthCareClinicSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Healthcare Clinic Management System";

            // Test database connection
            DatabaseConnection.TestConnection();

            Console.WriteLine("\nPress any key to start the application...");
            Console.ReadKey();

            try
            {
                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}