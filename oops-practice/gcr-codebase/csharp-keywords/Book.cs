using System;

namespace csharp_keywords
{
    class Book
    {
        public static string LibraryName = "City Library";

        public readonly string ISBN;
        public string Title;
        public string Author;

        public Book(string Title, string Author, string ISBN)
        {
            this.Title = Title;
            this.Author = Author;
            this.ISBN = ISBN; // readonly assigned
        }

        public static void DisplayLibraryName()
        {
            Console.WriteLine($"Library: {LibraryName}");
        }

        public void DisplayDetails(object obj)
        {
            if (obj is Book b)
            {
                Console.WriteLine("--- Book ---");
                DisplayLibraryName();
                Console.WriteLine($"Title: {b.Title}");
                Console.WriteLine($"Author: {b.Author}");
                Console.WriteLine($"ISBN: {b.ISBN}");
            }
            else
            {
                Console.WriteLine("Object is not a Book instance.");
            }
        }

        static void Main()
        {
            var book = new Book("The Alchemist", "Paulo Coelho", "ISBN-001");
            book.DisplayDetails(book);
        }
    }
}