using System;

class PalindromeCheckerBuiltin
{
    static void Main()
    {
        Console.Write("Enter a string or phrase: ");
        string s = Console.ReadLine() ?? string.Empty;
        bool isPal = IsPalindrome(s);
        Console.WriteLine(isPal ? "The input is a palindrome." : "The input is NOT a palindrome.");
    }

    static bool IsPalindrome(string s)
    {
        var sb = new System.Text.StringBuilder();
        foreach (char c in s)
        {
            if (char.IsLetterOrDigit(c)) sb.Append(char.ToLower(c));
        }
        string cleaned = sb.ToString();
        int i = 0, j = cleaned.Length - 1;
        while (i < j)
        {
            if (cleaned[i++] != cleaned[j--]) return false;
        }
        return true;
    }
}