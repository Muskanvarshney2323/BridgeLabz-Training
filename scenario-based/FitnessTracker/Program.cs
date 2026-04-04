using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("\n\n=======Welcome to Fitness Tracker!========");
        IFitnessLeaderboard utility = new FitnessUtility();
        Menu menu = new Menu(utility);

        menu.ShowMenu();
    }
}
