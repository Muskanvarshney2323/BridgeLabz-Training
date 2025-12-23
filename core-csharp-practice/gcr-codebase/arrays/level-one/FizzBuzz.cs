using System;
class FizzBuzz
{
    static void Main()
    {
        Console.WriteLine("Enter a positive integer: ");
        int number = Convert.ToInt32(Console.ReadLine());

        if (number < 1)
        {
            Console.WriteLine("Error: Please enter a positive integer greater than 0.");
            return;
        }

        string[] results = new string[number];

        for (int i = 0; i < number; i++)
        {
            int currentNumber = i + 1;

            if (currentNumber % 3 == 0 && currentNumber % 5 == 0)
            {
                results[i] = "FizzBuzz";
            }
            else if (currentNumber % 3 == 0)
            {
                results[i] = "Fizz";
            }
            else if (currentNumber % 5 == 0)
            {
                results[i] = "Buzz";
            }
            else
            {
                results[i] = currentNumber.ToString();
            }
        }

        for (int i = 0; i < results.Length; i++)
        {
            Console.WriteLine("Position {0} = {1}", i + 1, results[i]);
        }
    }
}