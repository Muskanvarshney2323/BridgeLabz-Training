using System;
public class DoublyLinkedList : IHistory
{
    private HistoryNode head;
    private HistoryNode tail;
    private HistoryNode current;
    public DoublyLinkedList() //Constructor
    {
        this.head = null;
        this.tail = null;
        this.current = null;
    }
    public void VisitPage(string url)
    {
        HistoryNode newNode = new HistoryNode(url);
        if(head == null) //if history is empty
        {
            head = newNode;
            tail = newNode;
            current = newNode;
            return;
        }
        else //if history is not empty
        {
            if(current.next!= null) // if there are forward pages, remove them
            {
                current.next.prev = null;
                current.next = null;
                tail = current;
            }
            current.next = newNode; // add new page
            newNode.prev = current; // link back to current
            current = newNode;  // move current to new page
            tail = newNode;  // update tail
        }
    }
    // method to go back
    public void GoBack()
    {
        if(current == null && current.prev == null) // no previous page
        {
            Console.WriteLine("No previous page to go back to.");
            return; 
        }
        current = current.prev; // move to the previous page
        Console.WriteLine("Went back to: " + current.url);

    }
// method to go forward 
    public void GoForward()
    {
        if(current == null && current.next == null) //no forward page
        {
            Console.WriteLine("No forward page to go to....");
            return;
        }
        current = current.next; // move to the next page
        Console.WriteLine("Went forward to: " + current.url);
    }
// method to get current page
    public void GetCurrentPage()
    {
        if(current == null) // no current page
        {
            Console.WriteLine("No current page....");
            return;
        }
        Console.WriteLine("Current page: " + current.url); // display current page url
    }
}