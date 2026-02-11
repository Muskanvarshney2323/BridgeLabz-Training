using System;

namespace FlightManagement
{
    public class UserInterface
    {
        public static void Start()
        {
            IFlightUtil util = new FlightUtil();

            Console.WriteLine("Enter flight details");
            Console.WriteLine("Format: <FlightNumber>:<FlightName>:<PassengerCount>:<CurrentFuelLevel>");

            string userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Input cannot be empty.");
                return;
            }

            try
            {
                ProcessInput(userInput, util);
            }
            catch (InvalidFlightException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input: Passenger count and Fuel level must be numeric");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        private static void ProcessInput(string input, IFlightUtil util)
        {
            string[] parts = input.Split(':');

            if (parts.Length != 4)
            {
                Console.WriteLine("Invalid input format.");
                return;
            }

            string flightNo = parts[0];
            string flightName = parts[1];
            int passengers = Convert.ToInt32(parts[2]);
            double fuelLevel = Convert.ToDouble(parts[3]);

            util.ValidateFlightNumber(flightNo);
            util.ValidateFlightName(flightName);
            util.ValidatePassengerCount(passengers, flightName);

            double requiredFuel = util.CalculateFuelToFillTank(flightName, fuelLevel);

            Console.WriteLine($"Fuel required to fill the tank: {requiredFuel} liters");
        }
    }
}
