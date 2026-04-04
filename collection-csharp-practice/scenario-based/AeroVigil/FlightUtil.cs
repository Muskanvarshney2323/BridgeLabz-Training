using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class FlightUtil : IFlightUtil
{
    private readonly Dictionary<string, int> flightCapacity = new Dictionary<string, int>()
    {
        { "SpiceJet", 396 },
        { "Vistara", 615 },
        { "IndiGo", 230 },
        { "Air Arabia", 130 }
    };

    private readonly Dictionary<string, double> flightFuelCapacity = new Dictionary<string, double>()
    {
        { "SpiceJet", 200000 },
        { "Vistara", 300000 },
        { "IndiGo", 250000 },
        { "Air Arabia", 150000 }
    };

    public bool ValidateFlightNumber(string flightNumber)
    {
        if (string.IsNullOrWhiteSpace(flightNumber))
            throw new InvalidFlightException($"The flight number {flightNumber} is invalid");

        Regex regex = new Regex(@"^FL-\d{4}$");

        if (!regex.IsMatch(flightNumber))
            throw new InvalidFlightException($"The flight number {flightNumber} is invalid");

        return true;
    }

    public bool ValidateFlightName(string flightName)
    {
        if (!flightCapacity.ContainsKey(flightName))
            throw new InvalidFlightException($"The flight name {flightName} is invalid");

        return true;
    }

    public bool ValidatePassengerCount(int passengerCount, string flightName)
    {
        if (!flightCapacity.ContainsKey(flightName))
            throw new InvalidFlightException($"The flight name {flightName} is invalid");

        int allowedCapacity = flightCapacity[flightName];

        if (passengerCount <= 0 || passengerCount > allowedCapacity)
            throw new InvalidFlightException(
                $"The passenger count {passengerCount} is invalid for {flightName}"
            );

        return true;
    }

    public double CalculateFuelToFillTank(string flightName, double currentFuelLevel)
    {
        if (!flightFuelCapacity.ContainsKey(flightName))
            throw new InvalidFlightException($"Invalid fuel level for {flightName}");

        double maxFuel = flightFuelCapacity[flightName];

        if (currentFuelLevel < 0 || currentFuelLevel > maxFuel)
            throw new InvalidFlightException($"Invalid fuel level for {flightName}");

        return maxFuel - currentFuelLevel;
    }
}
