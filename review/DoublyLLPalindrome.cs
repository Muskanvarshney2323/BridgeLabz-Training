using System;
class Node //Creating Node
{
    public int data;
    public Node next;
    public Node prev;
    public Node(int d) //constructor
    {
        this.data = d;
        this.next = null;
        this.prev = null;
    }
}
class DoublyLinkedList //DoublyLinkedList 
{
    private Node head;
    public void Add(int data) //method to add node
    {
        Node newNode = new Node(data);
        if (head == null) // if list is empty then create head = newNode
        {
            head = newNode;
            return;
        }
        Node temp = head;
        while(temp.next != null) 
        {
            temp = temp.next;
        }
        temp.next = newNode;
        newNode.prev = temp; 
    }
    public bool IsPalindrome()  //checking DLL is palindrome or not 
    {
        if (head == null)
        {
            return true;
        }
        Node tail = head;
        while (tail.next != null)
        {
            tail = tail.next;
        }
        return IsPalindromeRecursive(head, tail);

    }
    public bool IsPalindromeRecursive(Node left, Node right) //recursive method
    {
        if(left==null || right == null)
        {
            return true;
        }
        if(left==right || left.prev == right)
        {
            return true;
        }
        if (left.data != right.data)
        {
            return false;
        }
        return IsPalindromeRecursive(left.next, right.prev);
    }


}
class Program
{
    static void Main()
    {
        DoublyLinkedList d = new DoublyLinkedList();
        d.Add(1);
        d.Add(2);
        d.Add(1); 
        Console.WriteLine(d.IsPalindrome()? "Palindrome" : "Not Palindrome");
    }
}
