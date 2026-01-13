using System;

class CinemaUtility : ICinemaUtility
{
    Cinema[] movies = new Cinema[5];
    int count = 0;

    public void AddMovie()
    {
        if (count >= movies.Length)
        {
            Console.WriteLine("Movie list is full");
            return;
        }

        Console.Write("Enter movie name: ");
        string name = Console.ReadLine();

        Console.Write("Enter show time (HH:MM): ");
        string time = Console.ReadLine();

        if (!IsValidTime(time))
        {
            Console.WriteLine("Invalid time format");
            return;
        }

        movies[count] = new Cinema();
        movies[count].MovieName = name;
        movies[count].ShowTime = time;
        count++;

        Console.WriteLine("Movie added successfully");
    }

    bool IsValidTime(string time)
    {
        if (time.Length != 5 || time[2] != ':')
            return false;

        int h = int.Parse(time.Substring(0, 2));
        int m = int.Parse(time.Substring(3, 2));

        return h <= 23 && m <= 59;
    }

    public void DisplayMovies()
    {
        if (count == 0)
        {
            Console.WriteLine("No movies available");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(
                i + " : " + movies[i].MovieName + " at " + movies[i].ShowTime
            );
        }
    }

    public void SearchMovie()
    {
        Console.Write("Enter index: ");
        int index = int.Parse(Console.ReadLine());

        if (index < 0 || index >= count)
        {
            Console.WriteLine("Invalid index");
            return;
        }

        Console.WriteLine(
            movies[index].MovieName + " at " + movies[index].ShowTime
        );
    }
}
