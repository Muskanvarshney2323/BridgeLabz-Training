
class Program
{
    static void Main()
    {
        Console.Write("Enter your height in centimeters: ");
        double height_cm = Convert.ToDouble(Console.ReadLine());

        double total_inches = height_cm / 2.54;
        int feet = (int)(total_inches / 12);
        double inches = total_inches % 12;

        Console.WriteLine("Your Height in cm is " + height_cm + " while in feet is " + feet + " and inches is " + inches);
    }
}