using System;
using System.Text.RegularExpressions;

namespace CollectionRegex.Basic
{
    /// <summary>
    /// Problem 1: Validate a Username
    /// Requirements:
    /// - Can only contain letters (a-z, A-Z), numbers (0-9), and underscores (_)
    /// - Must start with a letter
    /// - Must be between 5 to 15 characters long
    /// </summary>
    class ValidateUsername
    {
        static void Main()
        {
            Console.WriteLine("=== Validate Username ===\n");

            // Test cases
            string[] testCases = new string[]
            {
                "user_123",      // ✅ Valid
                "john_doe",      // ✅ Valid
                "Alice123",      // ✅ Valid
                "a_b_c_d",       // ✅ Valid
                "User_99",       // ✅ Valid
                "123user",       // ❌ Invalid - starts with number
                "_user123",      // ❌ Invalid - starts with underscore
                "us",            // ❌ Invalid - too short
                "this_is_a_very_long_username_test", // ❌ Invalid - too long
                "user@123",      // ❌ Invalid - contains special character
                "user 123",      // ❌ Invalid - contains space
                "userñame",      // ❌ Invalid - contains non-ASCII character
                "User_1234",     // ✅ Valid (exactly 10 chars)
                "a12345678901",  // ❌ Invalid - too long (starts with letter but exceeds 15)
                "abcde",         // ✅ Valid (5 chars, exactly at minimum)
                "abcdefghijklmno" // ✅ Valid (15 chars, exactly at maximum)
            };

            ValidateUsernames(testCases);
        }

        static bool IsValidUsername(string username)
        {
            // Regex pattern explanation:
            // ^[a-zA-Z]    - Must start with a letter
            // [a-zA-Z0-9_]{4,14}  - Followed by 4-14 more characters (letters, numbers, underscore)
            // $            - End of string
            // Total length: 1 + 4-14 = 5-15 characters

            string pattern = @"^[a-zA-Z][a-zA-Z0-9_]{4,14}$";
            return Regex.IsMatch(username, pattern);
        }

        static void ValidateUsernames(string[] usernames)
        {
            Console.WriteLine("{0,-25} {1,-15} {2,-10}", "Username", "Validity", "Message");
            Console.WriteLine(new string('-', 50));

            foreach (string username in usernames)
            {
                bool isValid = IsValidUsername(username);
                string validity = isValid ? "✅ Valid" : "❌ Invalid";
                string message = GetValidationMessage(username, isValid);

                Console.WriteLine("{0,-25} {1,-15} {2,-10}", username, validity, message);
            }
        }

        static string GetValidationMessage(string username, bool isValid)
        {
            if (isValid)
                return "OK";

            if (username.Length < 5)
                return "Too short";
            if (username.Length > 15)
                return "Too long";
            if (!char.IsLetter(username[0]))
                return "No letter start";
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
                return "Bad chars";

            return "Invalid";
        }
    }
}
