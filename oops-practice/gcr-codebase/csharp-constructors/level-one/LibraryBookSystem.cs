using System;

class LibraryBookSystem
{
    public string title;
    public string author;
    public int price;
    public bool isAvailable;

    // Parameterized constructor
    public LibraryBookSystem(string t, string a, int p)
    {
        title = t;
        author = a;
        price = p;
        isAvailable = true; // default available
    }

    public void BorrowBook()
    {
        if (isAvailable)
        {
            Console.WriteLine("Successfully borrowed: " + title);
            isAvailable = false;
        }
        else
        {
            Console.WriteLine("Sorry, " + title + " is not available.");
        }
    }

    public static void Main()
    {
        LibraryBookSystem[] books = new LibraryBookSystem[4];

        books[0] = new LibraryBookSystem("Atomic Habits", "James Clear", 450);
        books[1] = new LibraryBookSystem("The Alchemist", "Paulo Coelho", 300);
        books[2] = new LibraryBookSystem("Clean Code", "Robert Martin", 550);
        books[3] = new LibraryBookSystem("Rich Dad Poor Dad", "Robert Kiyosaki", 400);

        Console.WriteLine("Books:");
        for (int i = 0; i < books.Length; i++)
        {
            Console.WriteLine((i + 1) + ". " + books[i].title);
        }

        Console.Write("\nEnter book number to borrow: ");
        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice >= 1 && choice <= books.Length)
        {
            books[choice - 1].BorrowBook();
        }
        else
        {
            Console.WriteLine("Invalid choice!");
        }
    }
}
