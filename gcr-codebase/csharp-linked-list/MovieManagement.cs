using System;

class Movie
{
    public string Title;
    public string Director;
    public int Year;
    public double Rating;
    public Movie Prev;
    public Movie Next;

    public Movie(string title, string director, int year, double rating)
    {
        Title = title;
        Director = director;
        Year = year;
        Rating = rating;
        Prev = null;
        Next = null;
    }
}

class DoublyMovieList
{
    private Movie head;
    private Movie tail;

    // Add at Beginning
    public void AddAtBeginning(string title, string director, int year, double rating)
    {
        Movie newNode = new Movie(title, director, year, rating);

        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            newNode.Next = head;
            head.Prev = newNode;
            head = newNode;
        }
    }

    // Add at End
    public void AddAtEnd(string title, string director, int year, double rating)
    {
        Movie newNode = new Movie(title, director, year, rating);

        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Prev = tail;
            tail = newNode;
        }
    }

    // Add at Specific Position
    public void AddAtPosition(int pos, string title, string director, int year, double rating)
    {
        if (pos == 1)
        {
            AddAtBeginning(title, director, year, rating);
            return;
        }

        Movie temp = head;
        for (int i = 1; i < pos - 1 && temp != null; i++)
            temp = temp.Next;

        if (temp == null || temp.Next == null)
        {
            AddAtEnd(title, director, year, rating);
            return;
        }

        Movie newNode = new Movie(title, director, year, rating);
        newNode.Next = temp.Next;
        newNode.Prev = temp;
        temp.Next.Prev = newNode;
        temp.Next = newNode;
    }

    // Remove by Movie Title
    public void RemoveByTitle(string title)
    {
        Movie temp = head;

        while (temp != null)
        {
            if (temp.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                if (temp == head)
                {
                    head = head.Next;
                    if (head != null)
                        head.Prev = null;
                }
                else if (temp == tail)
                {
                    tail = tail.Prev;
                    tail.Next = null;
                }
                else
                {
                    temp.Prev.Next = temp.Next;
                    temp.Next.Prev = temp.Prev;
                }

                Console.WriteLine("Movie Removed");
                return;
            }
            temp = temp.Next;
        }
        Console.WriteLine("Movie not found");
    }

    // Search by Director
    public void SearchByDirector(string director)
    {
        Movie temp = head;
        bool found = false;

        while (temp != null)
        {
            if (temp.Director.Equals(director, StringComparison.OrdinalIgnoreCase))
            {
                DisplayMovie(temp);
                found = true;
            }
            temp = temp.Next;
        }

        if (!found)
            Console.WriteLine("No movies found for this director");
    }

    // Search by Rating
    public void SearchByRating(double rating)
    {
        Movie temp = head;
        bool found = false;

        while (temp != null)
        {
            if (temp.Rating >= rating)
            {
                DisplayMovie(temp);
                found = true;
            }
            temp = temp.Next;
        }

        if (!found)
            Console.WriteLine("No movies found with given rating");
    }

    // Update Rating by Title
    public void UpdateRating(string title, double newRating)
    {
        Movie temp = head;

        while (temp != null)
        {
            if (temp.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                temp.Rating = newRating;
                Console.WriteLine("Rating Updated");
                return;
            }
            temp = temp.Next;
        }
        Console.WriteLine("Movie not found");
    }

    // Display Forward
    public void DisplayForward()
    {
        if (head == null)
        {
            Console.WriteLine("List is empty");
            return;
        }

        Movie temp = head;
        while (temp != null)
        {
            DisplayMovie(temp);
            temp = temp.Next;
        }
    }

    // Display Reverse
    public void DisplayReverse()
    {
        if (tail == null)
        {
            Console.WriteLine("List is empty");
            return;
        }

        Movie temp = tail;
        while (temp != null)
        {
            DisplayMovie(temp);
            temp = temp.Prev;
        }
    }

    private void DisplayMovie(Movie m)
    {
        Console.WriteLine($"Title: {m.Title}, Director: {m.Director}, Year: {m.Year}, Rating: {m.Rating}");
    }
}

class Program
{
    static void Main()
    {
        DoublyMovieList movies = new DoublyMovieList();

        movies.AddAtBeginning("Inception", "Christopher Nolan", 2010, 8.8);
        movies.AddAtEnd("Interstellar", "Christopher Nolan", 2014, 8.6);
        movies.AddAtPosition(2, "Avatar", "James Cameron", 2009, 7.9);

        Console.WriteLine("Movies (Forward):");
        movies.DisplayForward();

        Console.WriteLine("\nMovies (Reverse):");
        movies.DisplayReverse();

        Console.WriteLine("\nSearch by Director:");
        movies.SearchByDirector("Christopher Nolan");

        Console.WriteLine("\nUpdate Rating:");
        movies.UpdateRating("Avatar", 8.2);

        Console.WriteLine("\nAfter Update:");
        movies.DisplayForward();

        Console.WriteLine("\nRemove Movie:");
        movies.RemoveByTitle("Inception");

        Console.WriteLine("\nFinal List:");
        movies.DisplayForward();
    }
}
