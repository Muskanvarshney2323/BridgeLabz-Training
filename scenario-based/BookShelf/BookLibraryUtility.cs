using System;
using System.Collections.Generic;

public class BookLibraryUtility
{
    private Dictionary<string, BookLinkedList> genreCatalog;
    private HashSet<string> isbns; // To avoid duplication

    public BookLibraryUtility()
    {
        this.genreCatalog = new Dictionary<string, BookLinkedList>();
        this.isbns = new HashSet<string>();
    }

    // Add book to the catalog
    public void AddBook(string title, string author, string genre, string isbn)
    {
        if (isbns.Contains(isbn))
        {
            Console.WriteLine("Error: Book with this ISBN already exists.");
            return;
        }

        Book book = new Book(title, author, genre, isbn);

        if (!genreCatalog.ContainsKey(genre))
        {
            genreCatalog[genre] = new BookLinkedList();
        }

        genreCatalog[genre].AddBook(book);
        isbns.Add(isbn);
        Console.WriteLine($"Book '{title}' added successfully to {genre} genre.");
    }

    // Remove book from catalog
    public void RemoveBook(string genre, string title)
    {
        if (!genreCatalog.ContainsKey(genre))
        {
            Console.WriteLine("Genre not found in catalog.");
            return;
        }

        BookLinkedList list = genreCatalog[genre];
        Book book = list.SearchBook(title);

        if (book != null && list.RemoveBook(title))
        {
            isbns.Remove(book.GetISBN());
            Console.WriteLine($"Book '{title}' removed successfully from {genre}.");

            // Remove genre if list is empty
            if (list.IsEmpty())
            {
                genreCatalog.Remove(genre);
                Console.WriteLine($"Genre '{genre}' has been removed (no books left).");
            }
        }
        else
        {
            Console.WriteLine($"Book '{title}' not found in {genre}.");
        }
    }

    // Display all books in a genre
    public void DisplayGenreBooks(string genre)
    {
        if (!genreCatalog.ContainsKey(genre))
        {
            Console.WriteLine($"Genre '{genre}' not found in catalog.");
            return;
        }

        Console.WriteLine($"\n--- Books in {genre} ---");
        genreCatalog[genre].DisplayBooks();
    }

    // Search book by title in a specific genre
    public void SearchBook(string genre, string title)
    {
        if (!genreCatalog.ContainsKey(genre))
        {
            Console.WriteLine($"Genre '{genre}' not found.");
            return;
        }

        Book book = genreCatalog[genre].SearchBook(title);
        if (book != null)
        {
            Console.WriteLine("Book found:");
            book.DisplayInfo();
        }
        else
        {
            Console.WriteLine($"Book '{title}' not found in {genre}.");
        }
    }

    // Display all genres
    public void DisplayAllGenres()
    {
        if (genreCatalog.Count == 0)
        {
            Console.WriteLine("No genres in the catalog.");
            return;
        }

        Console.WriteLine("\n--- All Genres in Catalog ---");
        int index = 1;
        foreach (var genre in genreCatalog.Keys)
        {
            int bookCount = genreCatalog[genre].GetCount();
            Console.WriteLine($"{index}. {genre} ({bookCount} books)");
            index++;
        }
    }

    // Borrow book (remove from catalog)
    public void BorrowBook(string genre, string title)
    {
        RemoveBook(genre, title);
        Console.WriteLine($"Book '{title}' borrowed from {genre}.");
    }

    // Return book (add to catalog)
    public void ReturnBook(string title, string author, string genre, string isbn)
    {
        AddBook(title, author, genre, isbn);
        Console.WriteLine($"Book '{title}' returned to {genre}.");
    }

    // Get total book count
    public int GetTotalBookCount()
    {
        int total = 0;
        foreach (var list in genreCatalog.Values)
        {
            total += list.GetCount();
        }
        return total;
    }

    // Display catalog statistics
    public void DisplayStatistics()
    {
        Console.WriteLine("\n--- Library Statistics ---");
        Console.WriteLine($"Total Genres: {genreCatalog.Count}");
        Console.WriteLine($"Total Books: {GetTotalBookCount()}");
        foreach (var genre in genreCatalog.Keys)
        {
            Console.WriteLine($"  {genre}: {genreCatalog[genre].GetCount()} books");
        }
    }
}
