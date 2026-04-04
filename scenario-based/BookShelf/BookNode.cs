using System;

public class BookNode
{
    public Book book;
    public BookNode next;

    public BookNode(Book book)
    {
        this.book = book;
        this.next = null;
    }
}
