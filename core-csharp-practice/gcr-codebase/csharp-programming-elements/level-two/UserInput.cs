
class Program
{
    static void Main()
    {
        // Taking user inputs
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        Console.Write("Enter the starting city (fromCity): ");
        string fromCity = Console.ReadLine();

        Console.Write("Enter the via city (viaCity): ");
        string viaCity = Console.ReadLine();

        Console.Write("Enter the destination city (toCity): ");
        string toCity = Console.ReadLine();

        Console.Write("Enter the distance from " + fromCity + " to " + viaCity + " in miles: ");
        double fromToVia = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the distance from " + viaCity + " to " + toCity + " in miles: ");
        double viaToFinalCity = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the total time taken for the journey in hours: ");
        double timeTaken = Convert.ToDouble(Console.ReadLine());

        // Calculating total distance and average speed
        double totalDistance = fromToVia + viaToFinalCity;
        double averageSpeed = totalDistance / timeTaken;

        // Printing the results
        Console.WriteLine("The results of the trip are: Name: " + name + ", Total Distance: " + totalDistance + " miles, Average Speed: " + averageSpeed + " miles/hour.");
    }
}