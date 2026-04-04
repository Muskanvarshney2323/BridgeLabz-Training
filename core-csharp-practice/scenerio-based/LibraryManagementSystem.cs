using System;

class BookManager
{
    static string[,] library =
    {
        {"Harry Potter", "J.K. Rowling", "Available"},
        {"Atomic Habits", "James Clear", "Available"},
        {"The Alchemist", "Paulo Coelho", "Checked Out"},
        {"Clean Code", "Robert Martin", "Available"}
    };

    static void Main()
    {
        bool run = true;

        while (run)
        {
            ShowMenu();
            Console.Write("Choose option: ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }

            switch (option)
            {
                case 1:
                    PrintAllBooks();
                    break;

                case 2:
                    FindBookByTitle();
                    break;

                case 3:
                    BorrowBook();
                    break;

                case 4:
                    SubmitBook();
                    break;

                case 5:
                    Console.WriteLine("Program closed.");
                    run = false;
                    break;

                default:
                    Console.WriteLine("Wrong choice!");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n===== Library System =====");
        Console.WriteLine("1. Show all books");
        Console.WriteLine("2. Search book");
        Console.WriteLine("3. Checkout book");
        Console.WriteLine("4. Return book");
        Console.WriteLine("5. Exit");
    }

    static void PrintAllBooks()
    {
        Console.WriteLine("\nBook Name | Author | Status");

        for (int i = 0; i < library.GetLength(0); i++)
        {
            Console.WriteLine($"{library[i, 0]} | {library[i, 1]} | {library[i, 2]}");
        }
    }

    static void FindBookByTitle()
    {
        Console.Write("Enter keyword to search: ");
        string keyword = Console.ReadLine().Trim().ToLower();
        bool matchFound = false;

        for (int i = 0; i < library.GetLength(0); i++)
        {
            if (library[i, 0].ToLower().Contains(keyword))
            {
                Console.WriteLine($"{library[i, 0]} | {library[i, 1]} | {library[i, 2]}");
                matchFound = true;
            }
        }

        if (!matchFound)
        {
            Console.WriteLine("No matching book found.");
        }
    }

    static void BorrowBook()
    {
        Console.Write("Enter exact book title to checkout: ");
        string bookName = Console.ReadLine().ToLower();

        for (int i = 0; i < library.GetLength(0); i++)
        {
            if (library[i, 0].ToLower() == bookName)
            {
                if (library[i, 2] == "Available")
                {
                    library[i, 2] = "Checked Out";
                    Console.WriteLine("Book checked out successfully.");
                }
                else
                {
                    Console.WriteLine("This book is already checked out.");
                }
                return;
            }
        }

        Console.WriteLine("Book does not exist.");
    }

    static void SubmitBook()
    {
        Console.Write("Enter exact book title to return: ");
        string bookName = Console.ReadLine().ToLower();

        for (int i = 0; i < library.GetLength(0); i++)
        {
            if (library[i, 0].ToLower() == bookName)
            {
                library[i, 2] = "Available";
                Console.WriteLine("Book returned successfully.");
                return;
            }
        }

        Console.WriteLine("Book not found in records.");
    }
}
