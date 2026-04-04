using System;

public class UserInterface
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the Goods Transport details");
        string input = Console.ReadLine();

        Utility utility = new Utility();
        GoodsTransport transport = utility.ParseDetails(input);

        if (transport == null)
        {
            return;
        }

        string type = utility.FindObjectType(transport);

        if (type == "BrickTransport")
        {
            BrickTransport brick = (BrickTransport)transport;
            Console.WriteLine("Transporter id : " + brick.TransportId);
            Console.WriteLine("Date of transport : " + brick.TransportDate);
            Console.WriteLine("Rating of the transport : " + brick.TransportRating);
            Console.WriteLine("Quantity of bricks : " + brick.BrickQuantity);
            Console.WriteLine("Brick price : " + brick.BrickPrice);
            Console.WriteLine("Vehicle for transport : " + brick.VehicleSelection());
            Console.WriteLine("Total charge : " + brick.CalculateTotalCharge());
        }
        else if (type == "TimberTransport")
        {
            TimberTransport timber = (TimberTransport)transport;
            Console.WriteLine("Transporter id : " + timber.TransportId);
            Console.WriteLine("Date of transport : " + timber.TransportDate);
            Console.WriteLine("Rating of the transport : " + timber.TransportRating);
            Console.WriteLine("Type of the timber : " + timber.TimberType);
            Console.WriteLine("Timber price per kilo : " + timber.TimberPrice);
            Console.WriteLine("Vehicle for transport : " + timber.VehicleSelection());
            Console.WriteLine("Total charge : " + timber.CalculateTotalCharge());
        }
    }
}