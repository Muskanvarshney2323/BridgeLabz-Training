using System;

class BoardGame
{
    static Random randomGen = new Random();

    static void Main()
    {
        int totalPlayers = ReadPlayerCount();

        string[] playerNames = new string[totalPlayers];
        int[] playerPositions = new int[totalPlayers];

        for (int idx = 0; idx < totalPlayers; idx++)
        {
            Console.Write("Enter name of Player " + (idx + 1) + ": ");
            playerNames[idx] = Console.ReadLine();
            playerPositions[idx] = 0;
        }

        // Snakes and ladders positions
        int[] triggerPoints = { 4, 9, 17, 20, 28, 40, 51, 54, 62, 64, 71, 87, 93, 95, 99 };
        int[] targetPoints  = {14,31, 7, 38, 84, 59, 67, 34, 19, 60, 91, 24, 73, 75, 78 };

        bool isFinished = false;

        while (!isFinished)
        {
            for (int p = 0; p < totalPlayers; p++)
            {
                Console.WriteLine("\n " + playerNames[p] + "'s turn (Press Enter)");
                Console.ReadLine();

                int diceValue = GenerateDice();
                int previousPos = playerPositions[p];

                Console.WriteLine("Dice rolled: " + diceValue);

                if (previousPos + diceValue > 100)
                {
                    Console.WriteLine("Move skipped (exceeds 100)");
                    continue;
                }

                playerPositions[p] += diceValue;

                // Check for snake or ladder
                for (int s = 0; s < triggerPoints.Length; s++)
                {
                    if (playerPositions[p] == triggerPoints[s])
                    {
                        string message = targetPoints[s] > triggerPoints[s]
                            ? " Ladder! Move up!"
                            : " Snake! Move down!";

                        Console.WriteLine(message);
                        playerPositions[p] = targetPoints[s];
                        break;
                    }
                }

                Console.WriteLine(playerNames[p] + ": " + previousPos + " → " + playerPositions[p]);

                if (playerPositions[p] == 100)
                {
                    Console.WriteLine("\n " + playerNames[p] + " WINS THE GAME! ");
                    isFinished = true;
                    break;
                }
            }
        }

        Console.WriteLine("\nGame Over. Thank you for playing!");
    }

    static int ReadPlayerCount()
    {
        int num;
        do
        {
            Console.Write("Enter number of players (2–4): ");
            num = int.Parse(Console.ReadLine());
        } while (num < 2 || num > 4);

        return num;
    }

    static int GenerateDice()
    {
        return randomGen.Next(1, 7);
    }
}
