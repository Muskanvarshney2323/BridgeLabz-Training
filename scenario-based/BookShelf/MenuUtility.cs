using System;

public class MenuUtility
{
    private BookLibraryUtility library;

    public MenuUtility()
    {
        this.library = new BookLibraryUtility();
    }

    public void ShowMenu()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n========== BOOKSHELF LIBRARY MANAGEMENT SYSTEM ==========");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. View Books by Genre");
            Console.WriteLine("4. Search Book");
            Console.WriteLine("5. Display All Genres");
            Console.WriteLine("6. Borrow Book");
            Console.WriteLine("7. Return Book");
            Console.WriteLine("8. View Library Statistics");
            Console.WriteLine("9. Exit");
            Console.WriteLine("=======================================================");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    RemoveBook();
                    break;
                case "3":
                    ViewBooksByGenre();
                    break;
                case "4":
                    SearchBook();
                    break;
                case "5":
                    library.DisplayAllGenres();
                    break;
                case "6":
                    BorrowBook();
                    break;
                case "7":
                    ReturnBook();
                    break;
                case "8":
                    library.DisplayStatistics();
                    break;
                case "9":
                    isRunning = false;
                    Console.WriteLine("Exiting Library Management System. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void AddBook()
    {
        Console.Write("Enter book title: ");
        string title = Console.ReadLine();

        Console.Write("Enter author name: ");
        string author = Console.ReadLine();

        Console.Write("Enter genre: ");
        string genre = Console.ReadLine();

        Console.Write("Enter ISBN: ");
        string isbn = Console.ReadLine();

        library.AddBook(title, author, genre, isbn);
    }

    private void RemoveBook()
    {
        Console.Write("Enter genre: ");
        string genre = Console.ReadLine();

        Console.Write("Enter book title to remove: ");
        string title = Console.ReadLine();

        library.RemoveBook(genre, title);
    }

    private void ViewBooksByGenre()
    {
        Console.Write("Enter genre to view: ");
        string genre = Console.ReadLine();

        library.DisplayGenreBooks(genre);
    }

    private void SearchBook()
    {
        Console.Write("Enter genre to search in: ");
        string genre = Console.ReadLine();

        Console.Write("Enter book title to search: ");
        string title = Console.ReadLine();

        library.SearchBook(genre, title);
    }

    private void BorrowBook()
    {
        Console.Write("Enter genre: ");
        string genre = Console.ReadLine();

        Console.Write("Enter book title to borrow: ");
        string title = Console.ReadLine();

        library.BorrowBook(genre, title);
    }

    private void ReturnBook()
    {
        Console.Write("Enter book title: ");
        string title = Console.ReadLine();

        Console.Write("Enter author name: ");
        string author = Console.ReadLine();

        Console.Write("Enter genre: ");
        string genre = Console.ReadLine();

        Console.Write("Enter ISBN: ");
        string isbn = Console.ReadLine();

        library.ReturnBook(title, author, genre, isbn);
    }
}
