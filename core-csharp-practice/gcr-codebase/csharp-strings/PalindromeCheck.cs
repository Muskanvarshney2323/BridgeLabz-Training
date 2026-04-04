using System;
class PalindromeCheck
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string input = Console.ReadLine() ?? string.Empty;
        bool isPal = IsPalindrome(input);
        Console.WriteLine(isPal ? "The string is a palindrome." : "The string is NOT a palindrome.");
    }

    public static bool IsPalindrome(string s)
    {
        // Normalize: remove spaces and make lowercase
        string cleaned = s.Replace(" ", "").ToLower();
        int i = 0, j = cleaned.Length - 1;
        while (i < j)
        {
            if (cleaned[i] != cleaned[j]) return false;
            i++; j--;
        }
        return true;
    }
}