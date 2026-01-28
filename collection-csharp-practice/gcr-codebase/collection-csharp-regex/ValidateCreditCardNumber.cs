using System;
using System.Text.RegularExpressions;

namespace CollectionRegex.Advanced
{
    /// <summary>
    /// Problem 11: Validate a Credit Card Number
    /// Visa: Starts with 4 and has 16 digits
    /// MasterCard: Starts with 5 and has 16 digits
    /// </summary>
    class ValidateCreditCardNumber
    {
        static void Main()
        {
            Console.WriteLine("=== Validate Credit Card Number ===\n");

            // Test cases
            string[] testCases = new string[]
            {
                "4532015112830366",      // ✅ Valid Visa
                "4111111111111111",      // ✅ Valid Visa (test card)
                "5425233010103442",      // ✅ Valid MasterCard
                "5555555555554444",      // ✅ Valid MasterCard (test card)
                "378282246310005",       // ❌ American Express (starts with 3)
                "4532015112830",         // ❌ Visa but too short
                "45320151128303661",     // ❌ Visa but too long
                "5425233010103441",      // ❌ MasterCard with invalid checksum
                "1234567890123456",      // ❌ Doesn't start with 4 or 5
                "45320a5112830366",      // ❌ Contains letter
                "4532-0151-1283-0366",   // ❌ Contains hyphens
                "4532 0151 1283 0366",   // ❌ Contains spaces
                "0532015112830366",      // ❌ Starts with 0
                "3532015112830366",      // ❌ Starts with 3
                "6532015112830366",      // ❌ Starts with 6
            };

            ValidateCreditCards(testCases);

            // Detailed validation with checksum
            Console.WriteLine("\n\n--- Detailed Card Validation ---\n");
            DetailedCardValidation();

            // Luhn algorithm validation
            Console.WriteLine("\n\n--- Luhn Algorithm Check ---\n");
            LuhnAlgorithmDemo();
        }

        static (bool isValid, string cardType) ValidateCreditCard(string cardNumber)
        {
            // Remove spaces and hyphens
            cardNumber = Regex.Replace(cardNumber, @"[\s\-]", "");

            // Check if it contains only digits
            if (!Regex.IsMatch(cardNumber, @"^\d+$"))
                return (false, "Invalid");

            // Visa: starts with 4 and has 16 digits
            if (Regex.IsMatch(cardNumber, @"^4\d{15}$"))
                return (true, "Visa");

            // MasterCard: starts with 5 and has 16 digits
            if (Regex.IsMatch(cardNumber, @"^5\d{15}$"))
                return (true, "MasterCard");

            return (false, "Unknown/Invalid");
        }

        static void ValidateCreditCards(string[] cardNumbers)
        {
            Console.WriteLine("{0,-20} {1,-15} {2,-20}", "Card Number", "Validity", "Type");
            Console.WriteLine(new string('-', 55));

            foreach (string cardNumber in cardNumbers)
            {
                var (isValid, cardType) = ValidateCreditCard(cardNumber);
                string validity = isValid ? "✅ Valid" : "❌ Invalid";

                Console.WriteLine("{0,-20} {1,-15} {2,-20}", cardNumber, validity, cardType);
            }
        }

        static void DetailedCardValidation()
        {
            string cardNumber = "4532015112830366";

            Console.WriteLine($"Card Number: {cardNumber}\n");

            var (isValid, cardType) = ValidateCreditCard(cardNumber);

            if (isValid)
            {
                Console.WriteLine($"Status: ✅ Valid {cardType} Card\n");

                // Break down the card
                Console.WriteLine("Card Breakdown:");
                Console.WriteLine($"  First digit: {cardNumber[0]} (Issuer Identifier)");
                Console.WriteLine($"  Issuer code: {cardNumber.Substring(0, 6)}");
                Console.WriteLine($"  Account number: {cardNumber.Substring(6, 9)}");
                Console.WriteLine($"  Check digit: {cardNumber[15]}");

                // Luhn verification
                bool luhnValid = LuhnCheck(cardNumber);
                Console.WriteLine($"\nLuhn Check: {(luhnValid ? "✅ Valid" : "❌ Invalid")}");
            }
            else
            {
                Console.WriteLine($"Status: ❌ Invalid Card ({cardType})");
            }
        }

        // Luhn Algorithm for card validation
        static bool LuhnCheck(string cardNumber)
        {
            // Remove spaces and hyphens
            cardNumber = Regex.Replace(cardNumber, @"[\s\-]", "");

            // Check if all characters are digits
            if (!Regex.IsMatch(cardNumber, @"^\d+$"))
                return false;

            int sum = 0;
            bool isSecond = false;

            // Process digits from right to left
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = cardNumber[i] - '0';

                if (isSecond)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }

                sum += digit;
                isSecond = !isSecond;
            }

            return sum % 10 == 0;
        }

        static void LuhnAlgorithmDemo()
        {
            string[] cardNumbers = new string[]
            {
                "4111111111111111",  // Valid test card
                "4532015112830366",  // Valid card
                "5555555555554444",  // Valid test MasterCard
                "4532015112830365",  // Invalid (changed last digit)
            };

            Console.WriteLine("{0,-20} {1,-20} {2,-15}", "Card Number", "Format Valid", "Luhn Valid");
            Console.WriteLine(new string('-', 55));

            foreach (string card in cardNumbers)
            {
                var (formatValid, cardType) = ValidateCreditCard(card);
                bool luhnValid = LuhnCheck(card);

                string formatStatus = formatValid ? "✅ Yes" : "❌ No";
                string luhnStatus = luhnValid ? "✅ Yes" : "❌ No";

                Console.WriteLine("{0,-20} {1,-20} {2,-15}", card, formatStatus, luhnStatus);
            }
        }

        // Display card information
        static void DisplayCardInfo()
        {
            Console.WriteLine("\n\n--- Card Type Information ---\n");

            var cardInfo = new[]
            {
                ("Visa", "Starts with 4", "13 or 16 digits"),
                ("MasterCard", "Starts with 5", "16 digits"),
                ("American Express", "Starts with 3", "15 digits"),
                ("Discover", "Starts with 6", "16 digits"),
                ("Diners Club", "Starts with 3", "14 digits"),
            };

            Console.WriteLine("{0,-20} {1,-25} {2,-15}", "Card Type", "Identifier", "Length");
            Console.WriteLine(new string('-', 60));

            foreach (var (type, id, length) in cardInfo)
            {
                Console.WriteLine("{0,-20} {1,-25} {2,-15}", type, id, length);
            }
        }

        // Additional card types (extended validation)
        static (bool isValid, string cardType) ValidateCreditCardExtended(string cardNumber)
        {
            // Remove formatting
            cardNumber = Regex.Replace(cardNumber, @"[\s\-]", "");

            // Check if it contains only digits
            if (!Regex.IsMatch(cardNumber, @"^\d+$"))
                return (false, "Invalid");

            // Visa: starts with 4 (13 or 16 digits)
            if (Regex.IsMatch(cardNumber, @"^4\d{12}(\d{3})?$"))
                return (true, "Visa");

            // MasterCard: starts with 51-55 or 2221-2720 (16 digits)
            if (Regex.IsMatch(cardNumber, @"^(5[1-5]\d{14}|2(22[1-9]|2[3-9]\d|[3-6]\d{2}|7[01]\d|720)\d{12})$"))
                return (true, "MasterCard");

            // American Express: starts with 34 or 37 (15 digits)
            if (Regex.IsMatch(cardNumber, @"^3[47]\d{13}$"))
                return (true, "American Express");

            // Discover: starts with 6011, 622126-622925, 644, 645, 646, 647, 648, 649, 65 (16 digits)
            if (Regex.IsMatch(cardNumber, @"^(6011|65\d{2}|64[4-9]\d|6[0-9]{3})\d{12}$"))
                return (true, "Discover");

            return (false, "Unknown");
        }
    }
}
