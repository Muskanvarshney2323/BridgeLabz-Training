using System;
using System.Collections.Generic;

namespace LinkedListProblems
{
    /// <summary>
    /// Problem 5: Doubly Linked List - Library Management System
    /// 
    /// Design a library management system using a doubly linked list. Each node represents a book 
    /// and contains: Book Title, Author, Genre, Book ID, and Availability Status.
    /// </summary>
    public class BookNode
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }
        public BookNode Next { get; set; }
        public BookNode Previous { get; set; }

        public BookNode(int bookID, string title, string author, string genre, bool isAvailable)
        {
            BookID = bookID;
            Title = title;
            Author = author;
            Genre = genre;
            IsAvailable = isAvailable;
            Next = null;
            Previous = null;
        }

        public override string ToString()
        {
            return $"[ID: {BookID}, Title: {Title}, Author: {Author}, Genre: {Genre}, Available: {(IsAvailable ? "Yes" : "No")}]";
        }
    }

    public class LibraryManagementSystem
    {
        private BookNode head;

        public LibraryManagementSystem()
        {
            head = null;
        }

        /// <summary>
        /// Add a new book at the beginning
        /// </summary>
        public void AddAtBeginning(int bookID, string title, string author, string genre, bool isAvailable)
        {
            BookNode newNode = new BookNode(bookID, title, author, genre, isAvailable);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }
            Console.WriteLine($"Book '{title}' added at the beginning");
        }

        /// <summary>
        /// Add a new book at the end
        /// </summary>
        public void AddAtEnd(int bookID, string title, string author, string genre, bool isAvailable)
        {
            BookNode newNode = new BookNode(bookID, title, author, genre, isAvailable);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                BookNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                newNode.Previous = current;
                current.Next = newNode;
            }
            Console.WriteLine($"Book '{title}' added at the end");
        }

        /// <summary>
        /// Add a new book at a specific position
        /// </summary>
        public void AddAtPosition(int bookID, string title, string author, string genre, bool isAvailable, int position)
        {
            if (position == 0)
            {
                AddAtBeginning(bookID, title, author, genre, isAvailable);
                return;
            }

            BookNode newNode = new BookNode(bookID, title, author, genre, isAvailable);
            BookNode current = head;
            int count = 0;

            while (current != null && count < position - 1)
            {
                current = current.Next;
                count++;
            }

            if (current == null)
            {
                Console.WriteLine("Position out of bounds");
                return;
            }

            newNode.Next = current.Next;
            newNode.Previous = current;
            if (current.Next != null)
            {
                current.Next.Previous = newNode;
            }
            current.Next = newNode;
            Console.WriteLine($"Book '{title}' added at position {position}");
        }

        /// <summary>
        /// Remove a book by Book ID
        /// </summary>
        public void RemoveByBookID(int bookID)
        {
            if (head == null)
            {
                Console.WriteLine("Library is empty");
                return;
            }

            if (head.BookID == bookID)
            {
                head = head.Next;
                if (head != null)
                {
                    head.Previous = null;
                }
                Console.WriteLine($"Book with ID {bookID} removed");
                return;
            }

            BookNode current = head;
            while (current != null && current.BookID != bookID)
            {
                current = current.Next;
            }

            if (current != null)
            {
                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                Console.WriteLine($"Book with ID {bookID} removed");
            }
            else
            {
                Console.WriteLine($"Book with ID {bookID} not found");
            }
        }

        /// <summary>
        /// Search for a book by Book Title
        /// </summary>
        public BookNode SearchByTitle(string title)
        {
            BookNode current = head;
            while (current != null)
            {
                if (current.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        /// <summary>
        /// Search for a book by Author
        /// </summary>
        public List<BookNode> SearchByAuthor(string author)
        {
            List<BookNode> results = new List<BookNode>();
            BookNode current = head;

            while (current != null)
            {
                if (current.Author.Equals(author, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(current);
                }
                current = current.Next;
            }

            return results;
        }

        /// <summary>
        /// Update a book's Availability Status
        /// </summary>
        public void UpdateAvailabilityStatus(int bookID, bool isAvailable)
        {
            BookNode current = head;
            while (current != null)
            {
                if (current.BookID == bookID)
                {
                    current.IsAvailable = isAvailable;
                    Console.WriteLine($"Book '{current.Title}' availability status updated to {(isAvailable ? "Available" : "Not Available")}");
                    return;
                }
                current = current.Next;
            }
            Console.WriteLine($"Book with ID {bookID} not found");
        }

        /// <summary>
        /// Display all books in forward order
        /// </summary>
        public void DisplayForward()
        {
            if (head == null)
            {
                Console.WriteLine("No books in the library");
                return;
            }

            Console.WriteLine("\n--- Library Books (Forward) ---");
            BookNode current = head;
            int count = 1;

            while (current != null)
            {
                Console.WriteLine($"{count}. {current}");
                current = current.Next;
                count++;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Display all books in reverse order
        /// </summary>
        public void DisplayReverse()
        {
            if (head == null)
            {
                Console.WriteLine("No books in the library");
                return;
            }

            // Find the last node
            BookNode current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            Console.WriteLine("\n--- Library Books (Reverse) ---");
            int count = 1;

            while (current != null)
            {
                Console.WriteLine($"{count}. {current}");
                current = current.Previous;
                count++;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Count the total number of books in the library
        /// </summary>
        public int CountTotalBooks()
        {
            int count = 0;
            BookNode current = head;

            while (current != null)
            {
                count++;
                current = current.Next;
            }

            return count;
        }

        /// <summary>
        /// Count available books
        /// </summary>
        public int CountAvailableBooks()
        {
            int count = 0;
            BookNode current = head;

            while (current != null)
            {
                if (current.IsAvailable)
                {
                    count++;
                }
                current = current.Next;
            }

            return count;
        }
    }

    // Example Usage
    public class LibraryManagementSystemExample
    {
        public static void Main()
        {
            LibraryManagementSystem library = new LibraryManagementSystem();

            // Add books
            library.AddAtEnd(1, "To Kill a Mockingbird", "Harper Lee", "Fiction", true);
            library.AddAtEnd(2, "1984", "George Orwell", "Dystopian", true);
            library.AddAtEnd(3, "Pride and Prejudice", "Jane Austen", "Romance", false);
            library.AddAtEnd(4, "The Great Gatsby", "F. Scott Fitzgerald", "Fiction", true);
            library.AddAtEnd(5, "Brave New World", "Aldous Huxley", "Dystopian", true);

            library.AddAtBeginning(0, "The Catcher in the Rye", "J.D. Salinger", "Fiction", true);

            library.DisplayForward();
            library.DisplayReverse();

            // Search by author
            Console.WriteLine("Books by George Orwell:");
            var orwellBooks = library.SearchByAuthor("George Orwell");
            foreach (var book in orwellBooks)
            {
                Console.WriteLine(book);
            }

            // Update availability
            library.UpdateAvailabilityStatus(3, true);

            // Count books
            Console.WriteLine($"\nTotal books: {library.CountTotalBooks()}");
            Console.WriteLine($"Available books: {library.CountAvailableBooks()}");

            // Remove a book
            library.RemoveByBookID(5);

            library.DisplayForward();
        }
    }
}
