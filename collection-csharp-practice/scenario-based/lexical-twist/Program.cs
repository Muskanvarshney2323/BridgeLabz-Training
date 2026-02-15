using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the first word");
        string first = Console.ReadLine();

        if (first.Contains(" "))
        {
            Console.WriteLine(first + " is an invalid word");
            return;
        }

        Console.WriteLine("Enter the second word");
        string second = Console.ReadLine();

        if (second.Contains(" "))
        {
            Console.WriteLine(second + " is an invalid word");
            return;
        }

        // Check if second is reverse of first (case insensitive)
        string reversedFirst = new string(first.ToLower().ToCharArray().Reverse().ToArray());
        if (second.ToLower() == reversedFirst)
        {
            // Reverse first, lowercase, replace vowels with @
            string transformed = new string(first.ToCharArray().Reverse().ToArray()).ToLower();
            transformed = transformed.Replace("a", "@").Replace("e", "@").Replace("i", "@").Replace("o", "@").Replace("u", "@");
            Console.WriteLine(transformed);
        }
        else
        {
            // Combine, uppercase
            string combined = (first + second).ToUpper();

            // Count vowels and consonants
            int vowelCount = 0;
            int consonantCount = 0;
            foreach (char c in combined)
            {
                if (char.IsLetter(c))
                {
                    char lower = char.ToLower(c);
                    if ("aeiou".IndexOf(lower) != -1)
                    {
                        vowelCount++;
                    }
                    else
                    {
                        consonantCount++;
                    }
                }
            }

            if (vowelCount > consonantCount)
            {
                // First 2 distinct vowels
                string vowels = "";
                foreach (char c in combined)
                {
                    char upper = char.ToUpper(c);
                    if ("AEIOU".IndexOf(upper) != -1 && vowels.IndexOf(upper) == -1)
                    {
                        vowels += upper;
                        if (vowels.Length == 2) break;
                    }
                }
                Console.WriteLine(vowels);
            }
            else if (consonantCount > vowelCount)
            {
                // First 2 distinct consonants
                string consonants = "";
                foreach (char c in combined)
                {
                    if (char.IsLetter(c) && "AEIOU".IndexOf(c) == -1 && consonants.IndexOf(c) == -1)
                    {
                        consonants += c;
                        if (consonants.Length == 2) break;
                    }
                }
                Console.WriteLine(consonants);
            }
            else
            {
                Console.WriteLine("Vowels and consonants are equal");
            }
        }
    }
}