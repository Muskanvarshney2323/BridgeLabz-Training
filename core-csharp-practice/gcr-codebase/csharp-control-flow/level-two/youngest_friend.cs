using System;
class YoungestFriend
{
    static void Main()
    {
       
        Console.Write("Enter Amar's age: ");
        int amarAge = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Akbar's age: ");
        int akbarAge = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Anthony's age: ");
        int anthonyAge = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Amar's height (in cm): ");
        int amarHeight = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Akbar's height (in cm): ");
        int akbarHeight = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Anthony's height (in cm): ");
        int anthonyHeight = Convert.ToInt32(Console.ReadLine());

        // Find youngest friend
        if (amarAge < akbarAge && amarAge < anthonyAge)
        {
            Console.WriteLine("Amar is the youngest friend.");
        }
        else if (akbarAge < amarAge && akbarAge < anthonyAge)
        {
            Console.WriteLine("Akbar is the youngest friend.");
        }
        else if (anthonyAge < amarAge && anthonyAge < akbarAge)
        {
            Console.WriteLine("Anthony is the youngest friend.");
        }
        else
        {
            Console.WriteLine("There is a tie for the youngest friend.");
        }

        // Find tallest friend
        if (amarHeight > akbarHeight && amarHeight > anthonyHeight)
        {
            Console.WriteLine("Amar is the tallest friend.");
        }
        else if (akbarHeight > amarHeight && akbarHeight > anthonyHeight)
        {
            Console.WriteLine("Akbar is the tallest friend.");
        }
        else if (anthonyHeight > amarHeight && anthonyHeight > akbarHeight)
        {
            Console.WriteLine("Anthony is the tallest friend.");
        }
        else
        {
            Console.WriteLine("There is a tie for the tallest friend.");
        }
    }
}