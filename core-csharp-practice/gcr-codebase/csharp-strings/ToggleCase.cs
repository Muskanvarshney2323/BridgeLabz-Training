using System;
class ToggleCase
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string input = Console.ReadLine() ?? string.Empty;
        string toggled = ToggleCaseMethod(input);
        Console.WriteLine("Toggled string: " + toggled);
    }

    public static string ToggleCaseMethod(string s)
    {
        var sb = new System.Text.StringBuilder();
        foreach (char c in s)
        {
            if (char.IsLower(c)) sb.Append(char.ToUpper(c));
            else if (char.IsUpper(c)) sb.Append(char.ToLower(c));
            else sb.Append(c);
        }
        return sb.ToString();
    }
}