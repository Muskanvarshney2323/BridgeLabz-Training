using System;
class ReverseString
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string input = Console.ReadLine() ?? string.Empty;
        string reversed = ReverseStringMethod(input);
        Console.WriteLine("Reversed string: " + reversed);
    }

    public static string ReverseStringMethod(string s)
    {
        char[] arr = new char[s.Length];
        int j = 0;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            arr[j++] = s[i];
        }
        return new string(arr);
    }
}