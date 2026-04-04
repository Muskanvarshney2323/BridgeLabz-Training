using System;

namespace csharp_inheritance
{
    class Book
    {
        public string Title { get; set; }
        public int PublicationYear { get; set; }

        public Book(string title, int year)
        {
            Title = title;
            PublicationYear = year;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Book: {Title} ({PublicationYear})");
        }
    }

    class Author : Book
    {
        public string Name { get; set; }
        public string Bio { get; set; }

        public Author(string title, int year, string name, string bio) : base(title, year)
        {
            Name = name;
            Bio = bio;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Author: {Name}");
            Console.WriteLine($"Bio: {Bio}");
        }
    }

    class Program
    {
        static void Main()
        {
            var a = new Author("The Tale", 2020, "Dana", "Fiction writer.");
            a.DisplayInfo();
        }
    }
}