using System;

public class BookLibraryUtility
{
    private CustomDictionary genreCatalog;
    private CustomHashSet isbns; // To avoid duplication

    public BookLibraryUtility()
    {
        genreCatalog = new CustomDictionary();
        isbns = new CustomHashSet();
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
            genreCatalog.Put(genre, new BookLinkedList());
        }

        genreCatalog.Get(genre).AddBook(book);
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

        BookLinkedList list = genreCatalog.Get(genre);
        Book book = list.SearchBook(title);

        if (book != null && list.RemoveBook(title))
        {
            isbns.Remove(book.GetISBN());
            Console.WriteLine($"Book '{title}' removed successfully from {genre}.");

            if (list.IsEmpty())
            {
                genreCatalog.Remove(genre);
                Console.WriteLine($"Genre '{genre}' removed (no books left).");
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
        genreCatalog.Get(genre).DisplayBooks();
    }

    // Search book by title
    public void SearchBook(string genre, string title)
    {
        if (!genreCatalog.ContainsKey(genre))
        {
            Console.WriteLine($"Genre '{genre}' not found.");
            return;
        }

        Book book = genreCatalog.Get(genre).SearchBook(title);
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
        if (genreCatalog.GetCount() == 0)
        {
            Console.WriteLine("No genres in the catalog.");
            return;
        }

        Console.WriteLine("\n--- All Genres ---");
        string[] genres = genreCatalog.GetAllKeys();

        int index = 1;
        foreach (string genre in genres)
        {
            int count = genreCatalog.Get(genre).GetCount();
            Console.WriteLine($"{index}. {genre} ({count} books)");
            index++;
        }
    }

    // Borrow book
    public void BorrowBook(string genre, string title)
    {
        RemoveBook(genre, title);
    }

    // Return book
    public void ReturnBook(string title, string author, string genre, string isbn)
    {
        AddBook(title, author, genre, isbn);
    }

    // Total book count
    public int GetTotalBookCount()
    {
        int total = 0;
        string[] genres = genreCatalog.GetAllKeys();

        foreach (string genre in genres)
        {
            total += genreCatalog.Get(genre).GetCount();
        }

        return total;
    }

    // Display statistics
    public void DisplayStatistics()
    {
        Console.WriteLine("\n--- Library Statistics ---");
        Console.WriteLine($"Total Books: {GetTotalBookCount()}");

        string[] genres = genreCatalog.GetAllKeys();
        foreach (string genre in genres)
        {
            Console.WriteLine($"  {genre}: {genreCatalog.Get(genre).GetCount()} books");
        }
    }
}
