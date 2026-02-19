using System;
using TechVilla.Models;
using TechVilla.Utils;
using TechVilla.Exceptions;

namespace TechVilla.Modules
{
    public static class Module5_RobustRegistrationSystem
    {
        public static void Register(Citizen c)
        {
            try
            {
                if (c.Age < 0 || c.Age > 120)
                    throw new InvalidAgeException(c.Age);

                // Dummy duplicate check (in real code, check DB/file)
                if (c.Name == "duplicate")
                    throw new DuplicateCitizenException(c.Name);

                Console.WriteLine("Citizen registered (robust): " + c.Name);
            }
            catch (InvalidAgeException iae)
            {
                Console.WriteLine("Invalid age: " + iae.Message);
            }
            catch (DuplicateCitizenException dce)
            {
                Console.WriteLine("Duplicate: " + dce.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled error: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Registration attempt finished.");
            }
        }
    }
}
