using System;
class HotelBooking
{
    string guestName;
    string roomType;
    int nights;
    // default constructor
    public HotelBooking()
    {
        guestName = "Chirag";
        roomType = "Standard";
        nights = 2;
    }
    // parameterized constructor
    public HotelBooking(string name, string type, int numNights)
    {
        guestName = name;
        roomType = type;
        nights = numNights;
    }
    // copy constructor
    public HotelBooking(HotelBooking hb)
    {
        guestName = hb.guestName;
        roomType = hb.roomType;
        nights = hb.nights;
    }
    public void DisplayDetails()
    {
        Console.WriteLine("Guest Name: " + guestName);
        Console.WriteLine("Room Type: " + roomType);
        Console.WriteLine("Number of Nights: " + nights);
    }
    public static void Main()
    {
        // Creating object using default constructor
        HotelBooking booking1 = new HotelBooking();
        booking1.DisplayDetails();

        Console.WriteLine();

        // Creating object using parameterized constructor
        HotelBooking booking2 = new HotelBooking("Alice", "Deluxe", 5);
        booking2.DisplayDetails();

        Console.WriteLine();

        // Creating object using copy constructor
        HotelBooking booking3 = new HotelBooking(booking2);
        booking3.DisplayDetails();
    }
}