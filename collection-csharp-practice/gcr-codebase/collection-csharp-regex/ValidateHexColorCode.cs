using System;
using System.Text.RegularExpressions;

namespace CollectionRegex.Basic
{
    /// <summary>
    /// Problem 3: Validate a Hex Color Code
    /// Requirements:
    /// - Starts with a #
    /// - Followed by exactly 6 hexadecimal characters (0-9, A-F, a-f)
    /// Format: "#FFA500" or "#ff4500"
    /// </summary>
    class ValidateHexColorCode
    {
        static void Main()
        {
            Console.WriteLine("=== Validate Hex Color Code ===\n");

            // Test cases
            string[] testCases = new string[]
            {
                "#FFA500",      // ✅ Valid - Orange
                "#ff4500",      // ✅ Valid - OrangeRed (lowercase)
                "#FF0000",      // ✅ Valid - Red
                "#00FF00",      // ✅ Valid - Green
                "#0000FF",      // ✅ Valid - Blue
                "#FFFFFF",      // ✅ Valid - White
                "#000000",      // ✅ Valid - Black
                "#ABC123",      // ✅ Valid - Mixed case with numbers
                "#aabbcc",      // ✅ Valid - Lowercase
                "#123",         // ❌ Invalid - Too short
                "#12345",       // ❌ Invalid - Only 5 hex digits
                "#1234567",     // ❌ Invalid - Too long
                "FFA500",       // ❌ Invalid - Missing #
                "#FF050",       // ❌ Invalid - Only 5 chars
                "#FF05G0",      // ❌ Invalid - G is not a hex digit
                "#FF050Z",      // ❌ Invalid - Z is not a hex digit
                "# FF0500",     // ❌ Invalid - Space after #
                "#FF 050",      // ❌ Invalid - Space in hex
                "#",            // ❌ Invalid - Just hash
                "#FF050-",      // ❌ Invalid - Special character
                "#00000",       // ❌ Invalid - Only 5 digits
                "#00000g",      // ❌ Invalid - 'g' is not hex
                "#Ff0500",      // ✅ Valid - Mixed case
            };

            ValidateHexColors(testCases);

            Console.WriteLine("\n\n--- Common HTML Colors ---");
            DisplayCommonColors();
        }

        static bool IsValidHexColor(string colorCode)
        {
            // Regex pattern explanation:
            // ^#              - Must start with #
            // [0-9A-Fa-f]{6}  - Followed by exactly 6 hexadecimal characters
            // $               - End of string

            string pattern = @"^#[0-9A-Fa-f]{6}$";
            return Regex.IsMatch(colorCode, pattern);
        }

        static void ValidateHexColors(string[] colorCodes)
        {
            Console.WriteLine("{0,-15} {1,-15} {2,-15}", "Hex Code", "Validity", "Color");
            Console.WriteLine(new string('-', 45));

            foreach (string color in colorCodes)
            {
                bool isValid = IsValidHexColor(color);
                string validity = isValid ? "✅ Valid" : "❌ Invalid";
                string colorName = GetColorName(color);

                Console.WriteLine("{0,-15} {1,-15} {2,-15}", color, validity, colorName);
            }
        }

        static string GetColorName(string hexCode)
        {
            if (!IsValidHexColor(hexCode))
                return "N/A";

            return hexCode.ToUpper() switch
            {
                "#FF0000" => "Red",
                "#00FF00" => "Green",
                "#0000FF" => "Blue",
                "#FFFFFF" => "White",
                "#000000" => "Black",
                "#FFA500" => "Orange",
                "#FF4500" => "OrangeRed",
                "#FFFF00" => "Yellow",
                "#FF00FF" => "Magenta",
                "#00FFFF" => "Cyan",
                "#808080" => "Gray",
                "#AABBCC" => "LightGray",
                _ => "Custom"
            };
        }

        static void DisplayCommonColors()
        {
            Console.WriteLine("{0,-15} {1,-15} {2,-15}", "Color", "Hex Code", "RGB");
            Console.WriteLine(new string('-', 45));

            var commonColors = new[]
            {
                ("Red", "#FF0000", "255, 0, 0"),
                ("Green", "#00FF00", "0, 255, 0"),
                ("Blue", "#0000FF", "0, 0, 255"),
                ("White", "#FFFFFF", "255, 255, 255"),
                ("Black", "#000000", "0, 0, 0"),
                ("Yellow", "#FFFF00", "255, 255, 0"),
                ("Cyan", "#00FFFF", "0, 255, 255"),
                ("Magenta", "#FF00FF", "255, 0, 255"),
                ("Gray", "#808080", "128, 128, 128"),
                ("Orange", "#FFA500", "255, 165, 0"),
                ("Purple", "#800080", "128, 0, 128"),
                ("Pink", "#FFC0CB", "255, 192, 203"),
            };

            foreach (var (name, hex, rgb) in commonColors)
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-15}", name, hex, rgb);
            }
        }

        // Alternative method: Using TryParse approach
        static bool IsValidHexColorAlternative(string colorCode)
        {
            if (string.IsNullOrEmpty(colorCode) || colorCode.Length != 7 || colorCode[0] != '#')
                return false;

            // Try to parse the hex string
            try
            {
                int.Parse(colorCode.Substring(1), System.Globalization.NumberStyles.HexNumber);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
