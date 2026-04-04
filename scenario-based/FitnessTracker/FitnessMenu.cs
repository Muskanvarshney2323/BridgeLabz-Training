using System;

public class Menu
{
    private IFitnessLeaderboard leaderboard;

    public Menu(IFitnessLeaderboard leaderboard)
    {
        this.leaderboard = leaderboard;
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\n📋 Menu");
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. Update Steps");
            Console.WriteLine("3. View Leaderboard");
            Console.WriteLine("4. Exit");
            Console.Write("Choose option: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddUser();
                    break;
                case 2:
                    UpdateSteps();
                    break;
                case 3:
                    leaderboard.DisplayLeaderboard();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    private void AddUser()
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine();

        Console.Write("Enter steps: ");
        int steps = Convert.ToInt32(Console.ReadLine());

        leaderboard.AddUser(name, steps);
        Console.WriteLine("User added successfully!");
    }

    private void UpdateSteps()
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine();

        Console.Write("Enter updated steps: ");
        int steps = Convert.ToInt32(Console.ReadLine());

        leaderboard.UpdateSteps(name, steps);
        Console.WriteLine("Steps updated successfully!");
    }
}
