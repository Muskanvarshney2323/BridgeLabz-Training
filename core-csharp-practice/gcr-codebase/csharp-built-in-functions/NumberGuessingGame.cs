using System;

class NumberGuessingGame
{
    static void Main()
    {
        Console.WriteLine("Think of a number between 1 and 100 (inclusive). I will try to guess it.");
        Console.WriteLine("When ready, press Enter.");
        Console.ReadLine();

        int min = 1, max = 100;
        Random rand = new Random();

        while (min <= max)
        {
            int guess = GenerateGuess(min, max, rand);
            string feedback = GetFeedback(guess);
            if (feedback == "c")
            {
                Console.WriteLine($"Yay! I guessed your number: {guess}");
                return;
            }
            else if (feedback == "h")
            {
                max = Math.Min(max, guess - 1);
            }
            else if (feedback == "l")
            {
                min = Math.Max(min, guess + 1);
            }

            if (min > max)
            {
                Console.WriteLine("Inconsistent feedback detected. Are you sure about your responses?");
                return;
            }
        }

        Console.WriteLine("Couldn't determine the number. Exiting.");
    }

    static int GenerateGuess(int min, int max, Random r)
    {
        return r.Next(min, max + 1);
    }

    static string GetFeedback(int guess)
    {
        Console.Write($"My guess: {guess}. Is it (h)igh, (l)ow, or (c)orrect? ");
        while (true)
        {
            string? input = Console.ReadLine();
            if (input == null) continue;
            string s = input.Trim().ToLower();
            if (s == "h" || s == "high") return "h";
            if (s == "l" || s == "low") return "l";
            if (s == "c" || s == "correct") return "c";
            Console.Write("Please enter 'h', 'l', or 'c': ");
        }
    }
}