using System;

namespace FlightManagement
{
    public interface IFlightUtil
    {
        // Validates the flight number format
        bool ValidateFlightNumber(string flightNumber);

        // Checks whether the flight name is valid
        bool ValidateFlightName(string flightName);

        // Verifies passenger count based on flight capacity
        bool ValidatePassengerCount(int passengerCount, string flightName);

        // Calculates remaining fuel required to fill the tank
        double CalculateFuelToFillTank(string flightName, double currentFuelLevel);
    }
}
