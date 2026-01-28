using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionRegex.Advanced
{
    /// <summary>
    /// Problem 13: Extract Currency Values from Text
    /// Extracts currency amounts in various formats ($45.99, $10, £100.50, etc.)
    /// </summary>
    class ExtractCurrencyValues
    {
        static void Main()
        {
            Console.WriteLine("=== Extract Currency Values from Text ===\n");

            // Test cases with different currency formats
            string[] texts = new string[]
            {
                "The product costs $99.99 and the shipping is $5.00",
                "I earned £1000 last month and spent £500 on groceries.",
                "The Euro price is €75.50, and the Yen is ¥10000.",
                "Price: $ 45.99, Discount: $10.50, Total: $35.49",
                "Budget: $5000, Actual Spend: $4,500, Remaining: $500",
                "Items: $19.99, $29.99, $99.99, $4.99",
                "No currency values here",
                "Sale prices: $199.99, $299.99, and $499.99",
                "Amount paid: $1000.00 for services",
            };

            Console.WriteLine("Test Cases:\n");
            foreach (string text in texts)
            {
                Console.WriteLine($"Text: {text}");
                Console.WriteLine("Extracted Amounts:");
                ExtractAndDisplayCurrency(text);
                Console.WriteLine();
            }

            // Detailed analysis
            Console.WriteLine("\n--- Detailed Analysis ---\n");
            DetailedCurrencyAnalysis();

            // Calculate totals
            Console.WriteLine("\n--- Currency Total Calculations ---\n");
            CalculateCurrencyTotals();

            // Parse different formats
            Console.WriteLine("\n--- Parse Different Currency Formats ---\n");
            ParseDifferentFormats();
        }

        static void ExtractAndDisplayCurrency(string text)
        {
            // Pattern: optional $ or other currency symbols, optional space, digits, optional comma, decimal part
            string pattern = @"[$£€¥]\s*\d+(?:,\d{3})*(?:\.\d{2})?";

            MatchCollection matches = Regex.Matches(text, pattern);

            if (matches.Count == 0)
            {
                Console.WriteLine("  (no currency values found)");
            }
            else
            {
                foreach (Match match in matches)
                {
                    Console.WriteLine($"  {match.Value}");
                }
            }
        }

        static List<string> ExtractCurrency(string text)
        {
            string pattern = @"[$£€¥]\s*\d+(?:,\d{3})*(?:\.\d{2})?";
            MatchCollection matches = Regex.Matches(text, pattern);

            List<string> results = new List<string>();
            foreach (Match match in matches)
            {
                results.Add(match.Value);
            }
            return results;
        }

        static void DetailedCurrencyAnalysis()
        {
            string text = "I spent $25.50 on coffee, $15.99 on lunch, and $50 on groceries.";

            Console.WriteLine($"Text: {text}\n");

            List<string> amounts = ExtractCurrency(text);

            Console.WriteLine($"Currency Values Found: {amounts.Count}\n");

            for (int i = 0; i < amounts.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {amounts[i]}");
            }

            // Parse and calculate total
            decimal total = 0;
            Console.WriteLine("\n--- Amount Breakdown ---\n");

            foreach (string amount in amounts)
            {
                decimal value = ParseCurrencyValue(amount);
                Console.WriteLine($"  {amount,-10} = ${value:F2}");
                total += value;
            }

            Console.WriteLine($"\n  Total: ${total:F2}");
        }

        static decimal ParseCurrencyValue(string currencyString)
        {
            // Remove currency symbol and spaces
            string numericPart = Regex.Replace(currencyString, @"[$£€¥\s,]", "");
            
            if (decimal.TryParse(numericPart, out decimal value))
            {
                return value / 100m; // Assuming last 2 digits are cents
            }

            return 0;
        }

        static void CalculateCurrencyTotals()
        {
            string[] invoices = new string[]
            {
                "Item 1: $19.99, Item 2: $29.99, Item 3: $49.99",
                "Services: $500, Taxes: $75.50, Discount: -$25.00",
                "Purchase 1: $100.00, Purchase 2: $250.75"
            };

            foreach (string invoice in invoices)
            {
                Console.WriteLine($"Invoice: {invoice}");
                
                List<string> amounts = ExtractCurrency(invoice);
                decimal total = 0;

                foreach (string amount in amounts)
                {
                    decimal value = ParseCurrencyValue(amount);
                    total += value;
                }

                Console.WriteLine($"  Total: ${total:F2}\n");
            }
        }

        static void ParseDifferentFormats()
        {
            // Different currency formats
            string[] currencyFormats = new string[]
            {
                "$99.99",      // Standard US
                "$ 45.50",     // With space
                "$1,500.00",   // With comma separator
                "$10",         // Whole number
                "£100.50",     // British Pound
                "€75.99",      // Euro
                "¥10000",      // Yen (no decimal)
                "$5,000.50",   // Large amount with separator
            };

            Console.WriteLine("Currency Format Examples:\n");

            foreach (string format in currencyFormats)
            {
                decimal value = ParseCurrencyValue(format);
                Console.WriteLine($"  {format,-12} → {value,10:F2} units");
            }
        }

        // Extract amounts and group by currency symbol
        static Dictionary<string, List<decimal>> GroupByCurrency(string text)
        {
            Dictionary<string, List<decimal>> groups = new Dictionary<string, List<decimal>>();

            string pattern = @"([$£€¥])\s*(\d+(?:,\d{3})*(?:\.\d{2})?)";
            MatchCollection matches = Regex.Matches(text, pattern);

            foreach (Match match in matches)
            {
                string symbol = match.Groups[1].Value;
                string amount = match.Groups[2].Value;

                decimal value = ParseCurrencyValue(symbol + amount);

                if (!groups.ContainsKey(symbol))
                {
                    groups[symbol] = new List<decimal>();
                }

                groups[symbol].Add(value);
            }

            return groups;
        }

        // Find minimum and maximum amounts
        static void FindMinMaxCurrency(string text)
        {
            Console.WriteLine("\n--- Min/Max Currency Analysis ---\n");
            Console.WriteLine($"Text: {text}\n");

            List<string> amounts = ExtractCurrency(text);

            if (amounts.Count == 0)
            {
                Console.WriteLine("No currency values found.");
                return;
            }

            decimal min = decimal.MaxValue;
            decimal max = decimal.MinValue;
            string minStr = "", maxStr = "";

            foreach (string amount in amounts)
            {
                decimal value = ParseCurrencyValue(amount);
                
                if (value < min)
                {
                    min = value;
                    minStr = amount;
                }

                if (value > max)
                {
                    max = value;
                    maxStr = amount;
                }
            }

            Console.WriteLine($"Minimum: {minStr} = ${min:F2}");
            Console.WriteLine($"Maximum: {maxStr} = ${max:F2}");
            Console.WriteLine($"Difference: ${(max - min):F2}");
        }

        // Count currency occurrences
        static void CountCurrencyOccurrences(string text)
        {
            Console.WriteLine("\n--- Currency Count by Symbol ---\n");
            Console.WriteLine($"Text: {text}\n");

            var grouped = GroupByCurrency(text);

            foreach (var kvp in grouped)
            {
                Console.WriteLine($"Symbol: {kvp.Key}");
                Console.WriteLine($"  Count: {kvp.Value.Count}");
                Console.WriteLine($"  Total: {string.Join(" + ", kvp.Value)} = {SumList(kvp.Value):F2}\n");
            }
        }

        static decimal SumList(List<decimal> values)
        {
            decimal sum = 0;
            foreach (decimal value in values)
            {
                sum += value;
            }
            return sum;
        }
    }
}
