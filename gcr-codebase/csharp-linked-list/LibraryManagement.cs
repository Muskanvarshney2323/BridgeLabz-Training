using System;

class Book
{
    public int BookId;
    public string Title;
    public string Author;
    public string Genre;
    public bool IsAvailable;
    public Book Prev;
    public Book Next;

    public Book(int id, string title, string author, string genre, bool available)
    {
        BookId = id;
        Title = title;
        Author = author;
        Genre = genre;
        IsAvailable = available;
        Prev = null;
        Next = null;
    }
}

class DoublyLibraryList
{
    private Book head;
    private Book tail;

    // Add at Beginning
    public void AddAtBeginning(int id, string title, string author, string genre, bool available)
    {
        Book newNode = new Book(id, title, author, genre, available);

        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            newNode.Next = head;
            head.Prev = newNode;
            head = newNode;
        }
    }

    // Add at End
    public void AddAtEnd(int id, string title, string author, string genre, bool available)
    {
        Book newNode = new Book(id, title, author, genre, available);

        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Prev = tail;
            tail = newNode;
        }
    }

    // Add at Specific Position
    public void AddAtPosition(int pos, int id, string title, string author, string genre, bool available)
    {
        if (pos == 1)
        {
            AddAtBeginning(id, title, author, genre, available);
            return;
        }

        Book temp = head;
        for (int i = 1; i < pos - 1 && temp != null; i++)
            temp = temp.Next;

        if (temp == null || temp.Next == null)
        {
            AddAtEnd(id, title, author, genre, available);
            return;
        }

        Book newNode = new Book(id, title, author, genre, available);
        newNode.Next = temp.Next;
        newNode.Prev = temp;
        temp.Next.Prev = newNode;
        temp.Next = newNode;
    }

    // Remove by Book ID
    public void RemoveById(int id)
    {
        Book temp = head;

        while (temp != null)
        {
            if (temp.BookId == id)
            {
                if (temp == head)
                {
                    head = head.Next;
                    if (head != null)
                        head.Prev = null;
                }
                else if (temp == tail)
                {
                    tail = tail.Prev;
                    tail.Next = null;
                }
                else
                {
                    temp.Prev.Next = temp.Next;
                    temp.Next.Prev = temp.Prev;
                }

                Console.WriteLine("Book removed");
                return;
            }
            temp = temp.Next;
        }
        Console.WriteLine("Book not found");
    }

    // Search by Title
    public void SearchByTitle(string title)
    {
        Book temp = head;
        bool found = false;

        while (temp != null)
        {
            if (temp.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                DisplayBook(temp);
                found = true;
            }
            temp = temp.Next;
        }

        if (!found)
            Console.WriteLine("Book not found");
    }

    // Search by Author
    public void SearchByAuthor(string author)
    {
        Book temp = head;
        bool found = false;

        while (temp != null)
        {
            if (temp.Author.Equals(author, StringComparison.OrdinalIgnoreCase))
            {
                DisplayBook(temp);
                found = true;
            }
            temp = temp.Next;
        }

        if (!found)
            Console.WriteLine("Book not found");
    }

    // Update Availability
    public void UpdateAvailability(int id, bool status)
    {
        Book temp = head;

        while (temp != null)
        {
            if (temp.BookId == id)
            {
                temp.IsAvailable = status;
                Console.WriteLine("Availability updated");
                return;
            }
            temp = temp.Next;
        }
        Console.WriteLine("Book not found");
    }

    // Display Forward
    public void DisplayForward()
    {
        if (head == null)
        {
            Console.WriteLine("Library empty");
            return;
        }

        Book temp = head;
        while (temp != null)
        {
            DisplayBook(temp);
            temp = temp.Next;
        }
    }

    // Display Reverse
    public void DisplayReverse()
    {
        if (tail == null)
        {
            Console.WriteLine("Library empty");
            return;
        }

        Book temp = tail;
        while (temp != null)
        {
            DisplayBook(temp);
            temp = temp.Prev;
        }
    }

    // Count Total Books
    public void CountBooks()
    {
        int count = 0;
        Book temp = head;

        while (temp != null)
        {
            count++;
            temp = temp.Next;
        }

        Console.WriteLine($"Total Books: {count}");
    }

    private void DisplayBook(Book b)
    {
        Console.WriteLine(
            $"ID: {b.BookId}, Title: {b.Title}, Author: {b.Author}, Genre: {b.Genre}, Available: {b.IsAvailable}");
    }
}

class Program
{
    static void Main()
    {
        DoublyLibraryList library = new DoublyLibraryList();

        library.AddAtBeginning(101, "Clean Code", "Robert Martin", "Programming", true);
        library.AddAtEnd(102, "Atomic Habits", "James Clear", "Self Help", true);
        library.AddAtEnd(103, "The Alchemist", "Paulo Coelho", "Fiction", false);

        Console.WriteLine("Library (Forward):");
        library.DisplayForward();

        Console.WriteLine("\nLibrary (Reverse):");
        library.DisplayReverse();

        Console.WriteLine("\nSearch by Author:");
        library.SearchByAuthor("James Clear");

        Console.WriteLine("\nUpdate Availability:");
        library.UpdateAvailability(103, true);

        Console.WriteLine("\nAfter Update:");
        library.DisplayForward();

        library.CountBooks();
    }
}
