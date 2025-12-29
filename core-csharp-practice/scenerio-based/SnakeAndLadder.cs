using System;

class SnakeAndLadder
{
    static Random rand = new Random();

    static void Main()
    {
        int playerCount = GetPlayerCount();

        string[] players = new string[playerCount];
        int[] positions = new int[playerCount];

        for (int i = 0; i < playerCount; i++)
        {
            Console.Write("Enter name of Player " + (i + 1) + ": ");
            players[i] = Console.ReadLine();
            positions[i] = 0;
        }

        // Snakes and ladders using arrays
        int[] start = { 4, 9, 17, 20, 28, 40, 51, 54, 62, 64, 71, 87, 93, 95, 99 };
        int[] end   = {14,31, 7, 38, 84, 59, 67, 34, 19, 60, 91, 24, 73, 75, 78 };

        bool gameOver = false;

        while (!gameOver)
        {
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine("\n " + players[i] + "'s turn (Press Enter)");
                Console.ReadLine();

                int dice = RollDice();
                int oldPos = positions[i];

                Console.WriteLine("Dice rolled: " + dice);

                if (oldPos + dice > 100)
                {
                    Console.WriteLine("Move skipped (exceeds 100)");
                    continue;
                }

                positions[i] += dice;

                // Check snake or ladder
                for (int j = 0; j < start.Length; j++)
                {
                    if (positions[i] == start[j])
                    {
                        string msg = end[j] > start[j]
                            ? " Ladder! Move up!"
                            : " Snake! Move down!";

                        Console.WriteLine(msg);
                        positions[i] = end[j];
                        break;
                    }
                }

                Console.WriteLine(players[i] + ": " + oldPos + " → " + positions[i]);

                if (positions[i] == 100)
                {
                    Console.WriteLine("\n " + players[i] + " WINS THE GAME! ");
                    gameOver = true;
                    break;
                }
            }
        }

        Console.WriteLine("\nGame Over. Thank you for playing!");
    }

    static int GetPlayerCount()
    {
        int count;
        do
        {
            Console.Write("Enter number of players (2–4): ");
            count = int.Parse(Console.ReadLine());
        } while (count < 2 || count > 4);

        return count;
    }

    static int RollDice()
    {
        return rand.Next(1, 7);
    }
}