using System;

public class BookLinkedList
{
    private BookNode head;
    private int count;

    public BookLinkedList()
    {
        this.head = null;
        this.count = 0;
    }

    // Add book to the list
    public void AddBook(Book book)
    {
        if (book == null)
        {
            Console.WriteLine("Book cannot be null.");
            return;
        }

        BookNode newNode = new BookNode(book);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            BookNode current = head;
            while (current.next != null)
            {
                current = current.next;
            }
            current.next = newNode;
        }
        count++;
    }

    // Remove book by title
    public bool RemoveBook(string title)
    {
        if (head == null)
        {
            return false;
        }

        if (head.book.GetTitle().Equals(title))
        {
            head = head.next;
            count--;
            return true;
        }

        BookNode current = head;
        while (current.next != null)
        {
            if (current.next.book.GetTitle().Equals(title))
            {
                current.next = current.next.next;
                count--;
                return true;
            }
            current = current.next;
        }
        return false;
    }

    // Display all books in the list
    public void DisplayBooks()
    {
        if (head == null)
        {
            Console.WriteLine("No books in this genre.");
            return;
        }

        BookNode current = head;
        int index = 1;
        while (current != null)
        {
            Console.WriteLine($"{index}. {current.book}");
            current = current.next;
            index++;
        }
    }

    // Search book by title
    public Book SearchBook(string title)
    {
        BookNode current = head;
        while (current != null)
        {
            if (current.book.GetTitle().Equals(title))
            {
                return current.book;
            }
            current = current.next;
        }
        return null;
    }

    // Get count of books
    public int GetCount()
    {
        return count;
    }

    // Check if list is empty
    public bool IsEmpty()
    {
        return count == 0;
    }
}
