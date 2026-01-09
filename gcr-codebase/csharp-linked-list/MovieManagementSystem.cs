using System;
using System.Collections.Generic;

namespace LinkedListProblems
{
    /// <summary>
    /// Problem 2: Doubly Linked List - Movie Management System
    /// 
    /// Implement a movie management system using a doubly linked list. 
    /// Each node will represent a movie and contain Movie Title, Director, Year of Release, and Rating.
    /// </summary>
    public class MovieNode
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }
        public MovieNode Next { get; set; }
        public MovieNode Previous { get; set; }

        public MovieNode(string title, string director, int year, double rating)
        {
            Title = title;
            Director = director;
            Year = year;
            Rating = rating;
            Next = null;
            Previous = null;
        }

        public override string ToString()
        {
            return $"[Title: {Title}, Director: {Director}, Year: {Year}, Rating: {Rating}]";
        }
    }

    public class MovieManagementSystem
    {
        private MovieNode head;

        public MovieManagementSystem()
        {
            head = null;
        }

        /// <summary>
        /// Add a movie record at the beginning
        /// </summary>
        public void AddAtBeginning(string title, string director, int year, double rating)
        {
            MovieNode newNode = new MovieNode(title, director, year, rating);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }
            Console.WriteLine($"Movie '{title}' added at the beginning");
        }

        /// <summary>
        /// Add a movie record at the end
        /// </summary>
        public void AddAtEnd(string title, string director, int year, double rating)
        {
            MovieNode newNode = new MovieNode(title, director, year, rating);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                MovieNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                newNode.Previous = current;
                current.Next = newNode;
            }
            Console.WriteLine($"Movie '{title}' added at the end");
        }

        /// <summary>
        /// Add a movie record at a specific position
        /// </summary>
        public void AddAtPosition(string title, string director, int year, double rating, int position)
        {
            if (position == 0)
            {
                AddAtBeginning(title, director, year, rating);
                return;
            }

            MovieNode newNode = new MovieNode(title, director, year, rating);
            MovieNode current = head;
            int count = 0;

            while (current != null && count < position - 1)
            {
                current = current.Next;
                count++;
            }

            if (current == null)
            {
                Console.WriteLine("Position out of bounds");
                return;
            }

            newNode.Next = current.Next;
            newNode.Previous = current;
            if (current.Next != null)
            {
                current.Next.Previous = newNode;
            }
            current.Next = newNode;
            Console.WriteLine($"Movie '{title}' added at position {position}");
        }

        /// <summary>
        /// Remove a movie record by Movie Title
        /// </summary>
        public void RemoveByTitle(string title)
        {
            if (head == null)
            {
                Console.WriteLine("List is empty");
                return;
            }

            if (head.Title == title)
            {
                head = head.Next;
                if (head != null)
                {
                    head.Previous = null;
                }
                Console.WriteLine($"Movie '{title}' removed");
                return;
            }

            MovieNode current = head;
            while (current != null && current.Title != title)
            {
                current = current.Next;
            }

            if (current != null)
            {
                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                Console.WriteLine($"Movie '{title}' removed");
            }
            else
            {
                Console.WriteLine($"Movie '{title}' not found");
            }
        }

        /// <summary>
        /// Search for a movie record by Director
        /// </summary>
        public List<MovieNode> SearchByDirector(string director)
        {
            List<MovieNode> results = new List<MovieNode>();
            MovieNode current = head;

            while (current != null)
            {
                if (current.Director.Equals(director, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(current);
                }
                current = current.Next;
            }

            return results;
        }

        /// <summary>
        /// Search for a movie record by Rating
        /// </summary>
        public List<MovieNode> SearchByRating(double rating)
        {
            List<MovieNode> results = new List<MovieNode>();
            MovieNode current = head;

            while (current != null)
            {
                if (current.Rating == rating)
                {
                    results.Add(current);
                }
                current = current.Next;
            }

            return results;
        }

        /// <summary>
        /// Update a movie's Rating based on the Movie Title
        /// </summary>
        public void UpdateRating(string title, double newRating)
        {
            MovieNode current = head;
            while (current != null)
            {
                if (current.Title == title)
                {
                    current.Rating = newRating;
                    Console.WriteLine($"Rating for movie '{title}' updated to {newRating}");
                    return;
                }
                current = current.Next;
            }
            Console.WriteLine($"Movie '{title}' not found");
        }

        /// <summary>
        /// Display all movie records in forward order
        /// </summary>
        public void DisplayForward()
        {
            if (head == null)
            {
                Console.WriteLine("No movies found");
                return;
            }

            Console.WriteLine("\n--- Movies (Forward) ---");
            MovieNode current = head;
            int count = 1;
            while (current != null)
            {
                Console.WriteLine($"{count}. {current}");
                current = current.Next;
                count++;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Display all movie records in reverse order
        /// </summary>
        public void DisplayReverse()
        {
            if (head == null)
            {
                Console.WriteLine("No movies found");
                return;
            }

            // Find the last node
            MovieNode current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            Console.WriteLine("\n--- Movies (Reverse) ---");
            int count = 1;
            while (current != null)
            {
                Console.WriteLine($"{count}. {current}");
                current = current.Previous;
                count++;
            }
            Console.WriteLine();
        }
    }

    // Example Usage
    public class MovieManagementSystemExample
    {
        public static void Main()
        {
            MovieManagementSystem system = new MovieManagementSystem();

            // Add movies
            system.AddAtEnd("Inception", "Christopher Nolan", 2010, 8.8);
            system.AddAtEnd("The Dark Knight", "Christopher Nolan", 2008, 9.0);
            system.AddAtEnd("Interstellar", "Christopher Nolan", 2014, 8.6);
            system.AddAtEnd("Titanic", "James Cameron", 1997, 7.8);

            system.AddAtBeginning("Avatar", "James Cameron", 2009, 7.9);

            system.DisplayForward();
            system.DisplayReverse();

            // Search by director
            Console.WriteLine("Movies by Christopher Nolan:");
            var nolanMovies = system.SearchByDirector("Christopher Nolan");
            foreach (var movie in nolanMovies)
            {
                Console.WriteLine(movie);
            }

            // Update rating
            system.UpdateRating("Titanic", 8.1);

            // Remove a movie
            system.RemoveByTitle("Avatar");

            system.DisplayForward();
        }
    }
}
