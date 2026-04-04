using System;
class CompareStrings
{
    static void Main()
    {
        Console.Write("Enter first string: ");
        string s1 = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter second string: ");
        string s2 = Console.ReadLine() ?? string.Empty;
        int cmp = CompareStringsMethod(s1, s2);
        if (cmp < 0) Console.WriteLine($"\"{s1}\" comes before \"{s2}\" in lexicographical order");
        else if (cmp > 0) Console.WriteLine($"\"{s1}\" comes after \"{s2}\" in lexicographical order");
        else Console.WriteLine("The two strings are equal in lexicographical order.");
    }

    // returns -1 if s1 < s2, 0 if equal, 1 if s1 > s2
    public static int CompareStringsMethod(string s1, string s2)
    {
        string a = s1.ToLower();
        string b = s2.ToLower();
        int min = Math.Min(a.Length, b.Length);
        for (int i = 0; i < min; i++)
        {
            if (a[i] < b[i]) return -1;
            if (a[i] > b[i]) return 1;
        }
        if (a.Length < b.Length) return -1;
        if (a.Length > b.Length) return 1;
        return 0;
    }
}