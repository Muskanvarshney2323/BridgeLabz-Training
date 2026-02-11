using System;

public class FitnessUtility : IFitnessLeaderboard
{
    private User[] users;
    private int count;

    public FitnessUtility()
    {
        users = new User[20];   // array declaration
        count = 0;
    }

    public void AddUser(string name, int steps)
    {
        users[count++] = new User(name, steps);
        BubbleSort();
    }

    public void UpdateSteps(string name, int steps)
    {
        for (int i = 0; i < count; i++)
        {
            if (users[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                users[i].Steps = steps;
                break;
            }
        }
        BubbleSort();
    }

    //  BUBBLE SORT implementation
    private void BubbleSort()
    {
        for (int i = 0; i < count - 1; i++)
        {
            for (int j = 0; j < count - i - 1; j++)
            {
                if (users[j].Steps < users[j + 1].Steps)
                {
                    // manual swap
                    User temp = users[j];
                    users[j] = users[j + 1];
                    users[j + 1] = temp;
                }
            }
        }
    }

    public void DisplayLeaderboard()
    {
        Console.WriteLine("\n Leaderboard");
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"{i + 1}. {users[i].Name} - {users[i].Steps} steps");
        }
    }
}
