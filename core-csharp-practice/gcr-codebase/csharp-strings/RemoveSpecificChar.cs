using System;
class RemoveSpecificChar
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string s = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter character to remove: ");
        string input = Console.ReadLine() ?? string.Empty;
        char ch = input.Length > 0 ? input[0] : '\0';
        string result = RemoveSpecificCharMethod(s, ch);
        Console.WriteLine("Modified String: " + result);
    }

    public static string RemoveSpecificCharMethod(string s, char ch)
    {
        var sb = new System.Text.StringBuilder();
        foreach (char c in s)
        {
            if (c != ch) sb.Append(c);
        }
        return sb.ToString();
    }
}