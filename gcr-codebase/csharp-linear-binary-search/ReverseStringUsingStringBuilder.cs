using System;
using System.Text;

// Reverse a string using StringBuilder
// Best practice: Use StringBuilder when performing many string manipulations
public static class ReverseStringUsingStringBuilder
{
    public static string Reverse(string input)
    {
        if (input == null) return null;
        var sb = new StringBuilder(input);
        int i = 0, j = sb.Length - 1;
        while (i < j)
        {
            char tmp = sb[i];
            sb[i] = sb[j];
            sb[j] = tmp;
            i++; j--;
        }
        return sb.ToString();
    }

    // Sample usage
    public static void Main()
    {
        var s = "hello";
        Console.WriteLine(Reverse(s)); // prints "olleh"
    }
}