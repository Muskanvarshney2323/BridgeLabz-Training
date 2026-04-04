using System;
class NumberCheckerDemo
{
    static void Main()
    {
        Console.Write("Enter an integer for checks: ");
        int n = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine($"Digits: {string.Join(",", NumberChecker.Digits(n))}");
        Console.WriteLine($"Digit count: {NumberChecker.DigitCount(n)}");
        Console.WriteLine($"Is Duck: {NumberChecker.IsDuckNumber(n)}");
        Console.WriteLine($"Is Armstrong: {NumberChecker.IsArmstrong(n)}");
        var (l1,l2) = NumberChecker.LargestTwo(n); Console.WriteLine($"Largest: {l1}, Second Largest: {l2}");
        var (s1,s2) = NumberChecker.SmallestTwo(n); Console.WriteLine($"Smallest: {s1}, Second Smallest: {s2}");

        Console.WriteLine($"Sum digits: {NumberChecker.SumOfDigits(n)}");
        Console.WriteLine($"Sum squares: {NumberChecker.SumOfSquaresOfDigits(n)}");
        Console.WriteLine($"Is Harshad: {NumberChecker.IsHarshad(n)}");

        Console.WriteLine($"Is Palindrome: {NumberChecker.IsPalindrome(n)}");
        Console.WriteLine($"Is Prime: {NumberChecker.IsPrime(n)}");
        Console.WriteLine($"Is Neon: {NumberChecker.IsNeon(n)}");
        Console.WriteLine($"Is Spy: {NumberChecker.IsSpy(n)}");
        Console.WriteLine($"Is Automorphic: {NumberChecker.IsAutomorphic(n)}");
        Console.WriteLine($"Is Buzz: {NumberChecker.IsBuzz(n)}");

        Console.WriteLine($"Factors: {string.Join(",", NumberChecker.Factors(n))}");
        Console.WriteLine($"Greatest factor: {NumberChecker.GreatestFactor(n)}");
        Console.WriteLine($"Is Perfect: {NumberChecker.IsPerfect(n)}, Is Abundant: {NumberChecker.IsAbundant(n)}, Is Deficient: {NumberChecker.IsDeficient(n)}");
        Console.WriteLine($"Is Strong: {NumberChecker.IsStrong(n)}");
    }
}