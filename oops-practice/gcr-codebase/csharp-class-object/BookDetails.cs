using System;
class Book
{
    string title;
    string author;
    int price;
    public Book(string bookTitle, string bookAuthor, int bookPrice)
    {
        title = bookTitle;
        author = bookAuthor;
        price = bookPrice;
    }
    public void DisplayDetails()
    {
        Console.WriteLine("Book Title: " + title);
        Console.WriteLine("Book Author: " + author);
        Console.WriteLine("Book Price: " + price);  
    }
    public static void Main()
    {
        Book b1 = new Book("The Great Gatsby", "F. Scott Fitzgerald", 10);
        b1.DisplayDetails();
    }
}