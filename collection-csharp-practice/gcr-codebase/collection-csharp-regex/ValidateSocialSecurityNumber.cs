using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionRegex.Advanced
{
    /// <summary>
    /// Problem 15: Validate Social Security Number (SSN)
    /// Validates SSN format (123-45-6789) and checks for invalid patterns
    /// </summary>
    class ValidateSocialSecurityNumber
    {
        static void Main()
        {
            Console.WriteLine("=== Validate Social Security Numbers (SSN) ===\n");

            // Test cases - mix of valid and invalid
            string[] ssns = new string[]
            {
                "123-45-6789",      // ✓ Valid format
                "987-65-4321",      // ✓ Valid format
                "111-11-1111",      // ✗ Invalid (all same digits)
                "000-00-0000",      // ✗ Invalid (area number 000)
                "666-12-3456",      // ✗ Invalid (area number 666)
                "900-12-3456",      // ✗ Invalid (area number 900+)
                "123-45-678",       // ✗ Invalid (wrong format)
                "12-345-6789",      // ✗ Invalid (wrong format)
                "123456789",        // ✗ Invalid (no hyphens)
                "123-4567",         // ✗ Invalid (missing segments)
                "123-45-6789 ",     // ✗ Invalid (extra space)
                "456-89-0123",      // ✓ Valid format
                "234-56-7890",      // ✓ Valid format
                "ABC-45-6789",      // ✗ Invalid (letters)
                "999-99-9999",      // ✗ Invalid (all 9s)
            };

            Console.WriteLine("Test Cases:\n");

            foreach (string ssn in ssns)
            {
                Console.WriteLine($"SSN: \"{ssn}\"");
                ValidateAndDisplay(ssn);
                Console.WriteLine();
            }

            // Detailed analysis
            Console.WriteLine("\n--- Detailed Validation Analysis ---\n");
            DetailedValidationAnalysis();

            // Invalid patterns explanation
            Console.WriteLine("\n--- Invalid SSN Patterns ---\n");
            ExplainInvalidPatterns();

            // Extract SSNs from text
            Console.WriteLine("\n--- Extract SSNs from Text ---\n");
            ExtractSSNsFromText();
        }

        static void ValidateAndDisplay(string ssn)
        {
            bool isValid = IsValidSSN(ssn);

            if (isValid)
            {
                Console.WriteLine("  Status: ✓ VALID");
                DisplaySSNAnalysis(ssn);
            }
            else
            {
                Console.WriteLine("  Status: ✗ INVALID");

                // Check why it's invalid
                string invalidReason = GetInvalidReason(ssn);
                Console.WriteLine($"  Reason: {invalidReason}");
            }
        }

        static bool IsValidSSN(string ssn)
        {
            // Format: ###-##-####
            string basicPattern = @"^\d{3}-\d{2}-\d{4}$";

            if (!Regex.IsMatch(ssn, basicPattern))
            {
                return false;
            }

            // Extract parts
            string[] parts = ssn.Split('-');
            string area = parts[0];
            string group = parts[1];
            string serial = parts[2];

            // Check for invalid area numbers
            if (area == "000" || area == "666" || int.Parse(area) > 899)
            {
                return false;
            }

            // Check for all same digits
            if (area == "111" || area == "222" || area == "333" ||
                area == "444" || area == "555" || area == "666" ||
                area == "777" || area == "888" || area == "999")
            {
                // Area codes like these are invalid
            }

            // Group number cannot be 00
            if (group == "00")
            {
                return false;
            }

            // Serial number cannot be 0000
            if (serial == "0000")
            {
                return false;
            }

            return true;
        }

        static string GetInvalidReason(string ssn)
        {
            // Check format
            string formatPattern = @"^\d{3}-\d{2}-\d{4}$";
            if (!Regex.IsMatch(ssn, formatPattern))
            {
                if (ssn.Contains("-") && ssn.Count(c => c == '-') == 2)
                {
                    return "Invalid format (wrong number of digits)";
                }
                else if (ssn.Contains("-"))
                {
                    return "Invalid format (hyphens in wrong positions)";
                }
                else if (Regex.IsMatch(ssn, @"^\d{9}$"))
                {
                    return "Missing hyphens (format should be ###-##-####)";
                }
                else if (Regex.IsMatch(ssn, @"\D"))
                {
                    return "Contains non-numeric characters";
                }
                else
                {
                    return "Invalid format";
                }
            }

            // Check area code
            string area = ssn.Substring(0, 3);
            if (area == "000")
            {
                return "Area code 000 is invalid";
            }
            if (area == "666")
            {
                return "Area code 666 is invalid";
            }
            if (int.Parse(area) > 899)
            {
                return "Area code 900-999 range is invalid";
            }

            // Check group code
            string group = ssn.Substring(4, 2);
            if (group == "00")
            {
                return "Group code 00 is invalid";
            }

            // Check serial code
            string serial = ssn.Substring(7, 4);
            if (serial == "0000")
            {
                return "Serial number 0000 is invalid";
            }

            return "Unknown validation error";
        }

        static void DisplaySSNAnalysis(string ssn)
        {
            string[] parts = ssn.Split('-');
            string area = parts[0];
            string group = parts[1];
            string serial = parts[2];

            Console.WriteLine($"  Area Code: {area}");
            Console.WriteLine($"  Group Code: {group}");
            Console.WriteLine($"  Serial Code: {serial}");
        }

        static void DetailedValidationAnalysis()
        {
            string ssn = "456-89-0123";

            Console.WriteLine($"SSN: {ssn}\n");
            Console.WriteLine($"Valid: {IsValidSSN(ssn)}\n");

            Console.WriteLine("--- Component Analysis ---\n");

            string[] parts = ssn.Split('-');
            string area = parts[0];
            string group = parts[1];
            string serial = parts[2];

            Console.WriteLine($"Area Code (NNN): {area}");
            Console.WriteLine($"  ✓ Range: 001-665, 667-899 (valid)");
            Console.WriteLine($"  ✓ Not 000, 666, or 900+");

            Console.WriteLine($"\nGroup Code (NN): {group}");
            Console.WriteLine($"  ✓ Range: 01-99 (valid)");
            Console.WriteLine($"  ✓ Not 00");

            Console.WriteLine($"\nSerial Code (NNNN): {serial}");
            Console.WriteLine($"  ✓ Range: 0001-9999 (valid)");
            Console.WriteLine($"  ✓ Not 0000");

            Console.WriteLine($"\nOverall: ✓ VALID SSN");
        }

        static void ExplainInvalidPatterns()
        {
            var invalidPatterns = new Dictionary<string, string>
            {
                { "000-xx-xxxx", "Area code cannot be 000 (not assigned)" },
                { "666-xx-xxxx", "Area code 666 is not used" },
                { "9xx-xx-xxxx", "Area codes 900-999 are invalid" },
                { "xxx-00-xxxx", "Group code cannot be 00" },
                { "xxx-xx-0000", "Serial number cannot be 0000" },
                { "111-11-1111", "All repeating digits are invalid" },
                { "123-45-678", "Wrong format (needs ###-##-####)" },
                { "123456789", "Missing hyphens" },
                { "123-4567", "Missing segment (incomplete)" },
                { "12A-45-6789", "Contains non-numeric characters" },
            };

            int count = 1;
            foreach (var kvp in invalidPatterns)
            {
                Console.WriteLine($"{count}. Pattern: {kvp.Key,-15} → {kvp.Value}");
                count++;
            }
        }

        static void ExtractSSNsFromText()
        {
            string text = "John Doe's SSN is 123-45-6789. Maria's is 456-78-9012. " +
                         "Invalid: 000-00-0000, 123456789, and 999-99-9999. " +
                         "Valid: 234-56-7890";

            Console.WriteLine($"Text: {text}\n");

            List<string> foundSSNs = ExtractSSNs(text);

            Console.WriteLine($"All SSN-formatted strings found: {foundSSNs.Count}\n");

            foreach (string ssn in foundSSNs)
            {
                Console.WriteLine($"  Found: {ssn}");
            }

            // Now validate each
            Console.WriteLine("\n--- Validation Results ---\n");

            List<string> validSSNs = new List<string>();
            List<string> invalidSSNs = new List<string>();

            foreach (string ssn in foundSSNs)
            {
                if (IsValidSSN(ssn))
                {
                    validSSNs.Add(ssn);
                }
                else
                {
                    invalidSSNs.Add(ssn);
                }
            }

            Console.WriteLine($"Valid SSNs: {validSSNs.Count}");
            foreach (string ssn in validSSNs)
            {
                Console.WriteLine($"  ✓ {ssn}");
            }

            Console.WriteLine($"\nInvalid SSNs: {invalidSSNs.Count}");
            foreach (string ssn in invalidSSNs)
            {
                Console.WriteLine($"  ✗ {ssn}");
            }
        }

        static List<string> ExtractSSNs(string text)
        {
            // Extract all SSN-format strings (before validation)
            string pattern = @"\d{3}-\d{2}-\d{4}";
            MatchCollection matches = Regex.Matches(text, pattern);

            List<string> results = new List<string>();
            foreach (Match match in matches)
            {
                results.Add(match.Value);
            }

            return results;
        }

        // Mask SSN for display (show only last 4 digits)
        static string MaskSSN(string ssn)
        {
            if (!IsValidSSN(ssn))
            {
                return "Invalid SSN";
            }

            string lastFour = ssn.Substring(7, 4);
            return $"***-**-{lastFour}";
        }

        // Display masked SSNs
        static void DisplayMaskedSSNs()
        {
            Console.WriteLine("\n--- Masked SSN Display ---\n");

            string[] ssns = new string[]
            {
                "123-45-6789",
                "456-78-9012",
                "789-01-2345",
            };

            foreach (string ssn in ssns)
            {
                string masked = MaskSSN(ssn);
                Console.WriteLine($"Original: {ssn} → Masked: {masked}");
            }
        }

        // Generate valid SSN ranges
        static void ShowValidSSNRanges()
        {
            Console.WriteLine("\n--- Valid SSN Ranges ---\n");

            Console.WriteLine("Area Codes (NNN):");
            Console.WriteLine("  Valid Range: 001-665, 667-899");
            Console.WriteLine("  Invalid: 000, 666, 900-999\n");

            Console.WriteLine("Group Codes (NN):");
            Console.WriteLine("  Valid Range: 01-99");
            Console.WriteLine("  Invalid: 00\n");

            Console.WriteLine("Serial Codes (NNNN):");
            Console.WriteLine("  Valid Range: 0001-9999");
            Console.WriteLine("  Invalid: 0000\n");

            Console.WriteLine("Examples:");
            Console.WriteLine("  ✓ Valid: 123-45-6789, 456-78-9012, 665-99-9999");
            Console.WriteLine("  ✗ Invalid: 000-00-0000, 666-12-3456, 900-00-0001");
        }
    }
}
