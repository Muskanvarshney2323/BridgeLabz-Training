using System;
using System.Text.RegularExpressions;

namespace CollectionRegex.Basic
{
    /// <summary>
    /// Problem 2: Validate a License Plate Number
    /// Requirements:
    /// - Starts with two uppercase letters
    /// - Followed by four digits
    /// Format: "AB1234"
    /// </summary>
    class ValidateLicensePlate
    {
        static void Main()
        {
            Console.WriteLine("=== Validate License Plate Number ===\n");

            // Test cases
            string[] testCases = new string[]
            {
                "AB1234",       // ✅ Valid
                "XY5678",       // ✅ Valid
                "AA0000",       // ✅ Valid
                "ZZ9999",       // ✅ Valid
                "A12345",       // ❌ Invalid - only one letter
                "ABC1234",      // ❌ Invalid - three letters
                "AB123",        // ❌ Invalid - only three digits
                "AB12345",      // ❌ Invalid - five digits
                "ab1234",       // ❌ Invalid - lowercase letters
                "Ab1234",       // ❌ Invalid - second letter is lowercase
                "AB123a",       // ❌ Invalid - contains a letter in digit section
                "AB ABCD",      // ❌ Invalid - contains space
                "1B1234",       // ❌ Invalid - starts with number
                "AB-1234",      // ❌ Invalid - contains hyphen
                "AB1234 ",      // ❌ Invalid - trailing space
                "AB1234AB",     // ❌ Invalid - extra characters
                "ABCD",         // ❌ Invalid - no digits
                "1234AB",       // ❌ Invalid - wrong format
                "AB1234        " // ❌ Invalid - trailing spaces
            };

            ValidateLicensePlates(testCases);
        }

        static bool IsValidLicensePlate(string licensePlate)
        {
            // Regex pattern explanation:
            // ^[A-Z]{2}  - Must start with exactly two uppercase letters
            // [0-9]{4}   - Followed by exactly four digits
            // $          - End of string

            string pattern = @"^[A-Z]{2}[0-9]{4}$";
            return Regex.IsMatch(licensePlate, pattern);
        }

        static void ValidateLicensePlates(string[] licensePlates)
        {
            Console.WriteLine("{0,-20} {1,-15} {2,-20}", "License Plate", "Validity", "Message");
            Console.WriteLine(new string('-', 55));

            foreach (string plate in licensePlates)
            {
                bool isValid = IsValidLicensePlate(plate);
                string validity = isValid ? "✅ Valid" : "❌ Invalid";
                string message = GetValidationMessage(plate, isValid);

                Console.WriteLine("{0,-20} {1,-15} {2,-20}", plate, validity, message);
            }
        }

        static string GetValidationMessage(string plate, bool isValid)
        {
            if (isValid)
                return "Standard format";

            if (plate.Length != 6)
                return $"Length {plate.Length}, need 6";

            if (!Regex.IsMatch(plate.Substring(0, 2), @"^[A-Z]{2}$"))
                return "Bad letter format";

            if (!Regex.IsMatch(plate.Substring(2), @"^[0-9]{4}$"))
                return "Bad digit format";

            return "Invalid format";
        }

        // Additional example: License plates with custom formats
        static void DemonstrateDifferentFormats()
        {
            Console.WriteLine("\n\n--- License Plate Format Examples ---\n");

            Console.WriteLine("Standard Format (AB1234):");
            Console.WriteLine(IsValidLicensePlate("AB1234") ? "✅ Valid" : "❌ Invalid");

            Console.WriteLine("\nCustom Format with Hyphens (would need different regex):");
            Console.WriteLine("AB-12-34: Would need pattern ^[A-Z]{2}-[0-9]{2}-[0-9]{2}$");

            Console.WriteLine("\nCustom Format with Numbers First (would need different regex):");
            Console.WriteLine("1234AB: Would need pattern ^[0-9]{4}[A-Z]{2}$");
        }
    }
}
