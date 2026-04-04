using System;
class CountVowelsAndConsonants
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string input = Console.ReadLine() ?? string.Empty;
        int[] result = CountVowelsAndConsonantsMethod(input);
        Console.WriteLine($"Vowels = {result[0]}, Consonants = {result[1]}");
    }

    public static int[] CountVowelsAndConsonantsMethod(string s)
    {
        int vowels = 0, consonants = 0;
        string vs = "aeiouAEIOU";
        foreach (char c in s)
        {
            if (char.IsLetter(c))
            {
                if (vs.IndexOf(c) >= 0) vowels++;
                else consonants++;
            }
        }
        return new int[] { vowels, consonants };
    }
}