using System;

class CinemaMenu
{
    ICinemaUtility utility = new CinemaUtility();

    public void ShowMenu()
    {
        int choice;

        do
        {
            Console.WriteLine("\n--- CINEMA MENU ---");
            Console.WriteLine("1. Add Movie");
            Console.WriteLine("2. Display Movies");
            Console.WriteLine("3. Search Movie");
            Console.WriteLine("4. Exit");
            Console.Write("Enter choice: ");

            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    utility.AddMovie();
                    break;
                case 2:
                    utility.DisplayMovies();
                    break;
                case 3:
                    utility.SearchMovie();
                    break;
                case 4:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

        } while (choice != 4);
    }
}
