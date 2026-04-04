using System;
class SpringSeason
{
    static void Main()
    {
        Console.Write("Enter month (1-12): "); int month = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter day (1-31): "); int day = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(IsSpringSeason(month, day) ? "Its a Spring Season" : "Not a Spring Season");
    }

    public static bool IsSpringSeason(int month, int day)
    {
        try
        {
            DateTime dt = new DateTime(2000, month, day);
            DateTime start = new DateTime(2000, 3, 20);
            DateTime end = new DateTime(2000, 6, 20);
            return dt >= start && dt <= end;
        }
        catch
        {
            return false;
        }
    }
}