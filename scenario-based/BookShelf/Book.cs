using System;

public class Book : IBook
{
    private string title;
    private string author;
    private string genre;
    private string isbn;

    public Book(string title, string author, string genre, string isbn)
    {
        this.title = title;
        this.author = author;
        this.genre = genre;
        this.isbn = isbn;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetAuthor()
    {
        return author;
    }

    public string GetGenre()
    {
        return genre;
    }

    public string GetISBN()
    {
        return isbn;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Title: {title}, Author: {author}, Genre: {genre}, ISBN: {isbn}");
    }

    public override bool Equals(object obj)
    {
        if (obj is Book book)
        {
            return this.isbn == book.isbn;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return isbn.GetHashCode();
    }

    public override string ToString()
    {
        return $"{title} by {author}";
    }
}
