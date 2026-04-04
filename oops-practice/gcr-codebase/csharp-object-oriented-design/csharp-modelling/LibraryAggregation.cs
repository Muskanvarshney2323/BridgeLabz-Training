using System;
using System.Collections.Generic;

namespace csharp_modelling
{
    // Aggregation: Library "has" Books, but Books can exist independently.
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public override string ToString()
        {
            return $"{Title} by {Author}";
        }
    }

    class Library
    {
        public string Name { get; set; }
        public List<Book> Books { get; private set; }

        public Library(string name)
        {
            Name = name;
            Books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void DisplayBooks()
        {
            Console.WriteLine($"Books in {Name}:");
            foreach (var b in Books)
            {
                Console.WriteLine(" - " + b);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            var b1 = new Book("1984", "George Orwell");
            var b2 = new Book("Clean Code", "Robert C. Martin");

            var libA = new Library("Central Library");
            var libB = new Library("Community Library");

            libA.AddBook(b1);
            libA.AddBook(b2);

            libB.AddBook(b2); // same book object aggregated in another library

            libA.DisplayBooks();
            libB.DisplayBooks();

            Console.WriteLine("\nBook exists independently: " + b1);
        }
    }
}