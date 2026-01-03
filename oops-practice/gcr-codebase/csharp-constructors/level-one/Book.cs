using System;
class Book
{
    string title;
    string author;
    double price;
    //default constructor
    public Book()
    {
        title = "Unknown";
        author = "Unknown";
        price = 0.0;
    }
    //parameterized constructor
    public Book(string t, string a, double p)
    {
        this.title = t;
        this.author = a;
        this.price = p;
    }
    public void DisplayBooks()
    {
        Console.WriteLine("Book Title: " + title);
        Console.WriteLine("Book Author: " + author);
        Console.WriteLine("Book Price: " + price);
    }
    public static void Main()
    {
        //creating object using default constructor
        Book book1 = new Book();
        book1.DisplayBooks();

        Console.WriteLine();

        //creating object using parameterized constructor
        Book book2 = new Book("1984", "George Orwell", 15.99);
        book2.DisplayBooks();
    }
}