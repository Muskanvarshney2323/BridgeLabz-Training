using System;

namespace FlightManagement
{
    // Custom exception for flight-related validation errors
    public class InvalidFlightException : Exception
    {
        public InvalidFlightException(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}
