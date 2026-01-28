using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionRegex.Extraction
{
    /// <summary>
    /// Problem 6: Extract Dates in dd/mm/yyyy Format
    /// Extracts all dates matching the dd/mm/yyyy format
    /// </summary>
    class ExtractDatesInFormat
    {
        static void Main()
        {
            Console.WriteLine("=== Extract Dates in dd/mm/yyyy Format ===\n");

            // Test cases
            string[] texts = new string[]
            {
                "The events are scheduled for 12/05/2023, 15/08/2024, and 29/02/2020.",
                "Important dates: 01/01/2023 (New Year), 25/12/2023 (Christmas), 14/02/2024 (Valentine's Day)",
                "Meeting on 31/12/2023 and 01/01/2024",
                "Invalid dates: 32/01/2023, 29/02/2021, 00/00/0000",
                "Last day of months: 31/01/2023, 28/02/2023, 31/03/2023, 30/04/2023, 31/05/2023",
                "Various dates: 01/02/2020, 05/06/2019, 10/11/2018, 15/12/2017",
                "No dates in this text!",
                "Incorrect formats: 1/5/2023, 012/05/2023, 12/5/2023",
            };

            foreach (string text in texts)
            {
                Console.WriteLine($"Text: {text}");
                Console.WriteLine("Extracted Dates:");
                ExtractAndDisplayDates(text);
                Console.WriteLine();
            }

            // Detailed validation example
            Console.WriteLine("\n--- Detailed Date Validation ---\n");
            DetailedDateValidation();
        }

        static void ExtractAndDisplayDates(string text)
        {
            List<string> dates = ExtractDates(text);

            if (dates.Count == 0)
            {
                Console.WriteLine("  (no valid dates found)");
            }
            else
            {
                for (int i = 0; i < dates.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {dates[i]}");
                }
            }
        }

        static List<string> ExtractDates(string text)
        {
            List<string> validDates = new List<string>();

            // Pattern that matches dd/mm/yyyy format (not validating actual date values)
            // \b              - Word boundary
            // (0[1-9]|[12][0-9]|3[01])  - Day: 01-31
            // /               - Separator
            // (0[1-9]|1[0-2])           - Month: 01-12
            // /               - Separator
            // ([0-9]{4})                - Year: 4 digits
            // \b              - Word boundary

            string pattern = @"\b(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/([0-9]{4})\b";

            MatchCollection matches = Regex.Matches(text, pattern);

            foreach (Match match in matches)
            {
                string date = match.Value;
                
                // Validate the actual date
                if (IsValidDate(date))
                {
                    validDates.Add(date);
                }
            }

            return validDates;
        }

        static bool IsValidDate(string dateString)
        {
            try
            {
                string[] parts = dateString.Split('/');
                int day = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int year = int.Parse(parts[2]);

                // Create a DateTime to validate
                DateTime date = new DateTime(year, month, day);
                return true;
            }
            catch
            {
                return false;
            }
        }

        static void DetailedDateValidation()
        {
            string text = "Events: 15/03/2023, 29/02/2020, 31/04/2023, 30/06/2023";

            Console.WriteLine($"Text: {text}\n");

            // Get all matches (including invalid dates)
            string pattern = @"\b(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/([0-9]{4})\b";
            MatchCollection matches = Regex.Matches(text, pattern);

            Console.WriteLine($"Dates Matching Pattern: {matches.Count}\n");

            int validCount = 0;
            foreach (Match match in matches)
            {
                string dateStr = match.Value;
                bool isValid = IsValidDate(dateStr);

                string[] parts = dateStr.Split('/');
                int day = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int year = int.Parse(parts[2]);

                string validity = isValid ? "✅ Valid" : "❌ Invalid";
                string reason = "";

                if (isValid)
                {
                    DateTime dt = new DateTime(year, month, day);
                    reason = dt.ToString("dddd, MMMM d, yyyy");
                    validCount++;
                }
                else
                {
                    reason = "Invalid calendar date";
                }

                Console.WriteLine($"  {dateStr,-15} {validity,-12} {reason}");
            }

            Console.WriteLine($"\nValid Dates: {validCount}/{matches.Count}");
        }

        // Extract dates and convert to specific format
        static void DemonstrateDateConversion()
        {
            Console.WriteLine("\n\n--- Date Format Conversion ---\n");

            string text = "Appointments: 15/03/2023, 22/06/2023, 08/12/2023";

            List<string> dates = ExtractDates(text);

            Console.WriteLine($"Original: {text}\n");
            Console.WriteLine("Converted Formats:");

            foreach (string dateStr in dates)
            {
                string[] parts = dateStr.Split('/');
                int day = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int year = int.Parse(parts[2]);

                DateTime date = new DateTime(year, month, day);

                Console.WriteLine($"  {dateStr} → {date:MMMM d, yyyy}");
            }
        }

        // Extract different date formats
        static List<string> ExtractMultipleDateFormats(string text)
        {
            List<string> allDates = new List<string>();

            // dd/mm/yyyy
            string pattern1 = @"\b(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/([0-9]{4})\b";
            MatchCollection matches1 = Regex.Matches(text, pattern1);
            foreach (Match match in matches1)
            {
                allDates.Add(match.Value);
            }

            // yyyy-mm-dd
            string pattern2 = @"\b([0-9]{4})-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])\b";
            MatchCollection matches2 = Regex.Matches(text, pattern2);
            foreach (Match match in matches2)
            {
                allDates.Add(match.Value);
            }

            // mm-dd-yyyy
            string pattern3 = @"\b(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])-([0-9]{4})\b";
            MatchCollection matches3 = Regex.Matches(text, pattern3);
            foreach (Match match in matches3)
            {
                allDates.Add(match.Value);
            }

            return allDates;
        }
    }
}
