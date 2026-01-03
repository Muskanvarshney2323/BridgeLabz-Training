using System;

class Book
{
    // Access modifiers
    public string ISBN;        // Public
    protected string title;    // Protected
    private string author;     // Private

    // Constructor
    public Book(string isbn, string t, string a)
    {
        ISBN = isbn;
        title = t;
        author = a;
    }

    // Setter for author
    public void SetAuthor(string a)
    {
        author = a;
    }

    // Getter for author
    public string GetAuthor()
    {
        return author;
    }
}

// Subclass
class EBook : Book
{
    string fileFormat;

    public EBook(string isbn, string t, string a, string format)
        : base(isbn, t, a)
    {
        fileFormat = format;
    }

    public void DisplayEBookDetails()
    {
        Console.WriteLine("ISBN: " + ISBN);     // public → accessible
        Console.WriteLine("Title: " + title);   // protected → accessible
        Console.WriteLine("Author: " + GetAuthor()); // private via method
        Console.WriteLine("Format: " + fileFormat);
    }
}

class Program
{
    static void Main()
    {
        EBook ebook = new EBook(
            "978-0132350884",
            "Clean Code",
            "Robert Martin",
            "PDF"
        );

        ebook.DisplayEBookDetails();

        Console.WriteLine();

        // Modify private member using setter
        ebook.SetAuthor("Uncle Bob");
        Console.WriteLine("Updated Author: " + ebook.GetAuthor());
    }
}
