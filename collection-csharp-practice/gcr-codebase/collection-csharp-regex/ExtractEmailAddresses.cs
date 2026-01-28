using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionRegex.Extraction
{
    /// <summary>
    /// Problem 4: Extract All Email Addresses from a Text
    /// Extracts all valid email addresses from a given text
    /// </summary>
    class ExtractEmailAddresses
    {
        static void Main()
        {
            Console.WriteLine("=== Extract Email Addresses from Text ===\n");

            // Test cases
            string[] texts = new string[]
            {
                "Contact us at support@example.com and info@company.org",
                "Email me at john.doe@company.co.uk or jane_smith@email.com",
                "Invalid emails: test@, @test.com, and user@domain",
                "Multiple contacts: alice@test.com, bob@example.org, charlie@domain.co.uk",
                "No emails in this text!",
                "my-email@test-domain.com and another+tag@email.org are valid",
                "Email: contact@company.co.uk, support@example.com, info@test.org",
            };

            foreach (string text in texts)
            {
                Console.WriteLine($"Text: {text}");
                Console.WriteLine("Extracted Emails:");
                ExtractAndDisplayEmails(text);
                Console.WriteLine();
            }

            // More detailed example
            Console.WriteLine("\n--- Detailed Email Extraction ---\n");
            DetailedEmailExtraction();
        }

        static void ExtractAndDisplayEmails(string text)
        {
            List<string> emails = ExtractEmails(text);

            if (emails.Count == 0)
            {
                Console.WriteLine("  (no emails found)");
            }
            else
            {
                for (int i = 0; i < emails.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {emails[i]}");
                }
            }
        }

        static List<string> ExtractEmails(string text)
        {
            List<string> emails = new List<string>();

            // Regex pattern for email addresses
            // [a-zA-Z0-9._-]+     - Username part (letters, numbers, dot, underscore, hyphen)
            // @                   - @ symbol
            // [a-zA-Z0-9.-]+      - Domain name
            // \.                  - Literal dot
            // [a-zA-Z]{2,}        - Top-level domain (at least 2 letters)

            string pattern = @"[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";

            MatchCollection matches = Regex.Matches(text, pattern);

            foreach (Match match in matches)
            {
                emails.Add(match.Value);
            }

            return emails;
        }

        static void DetailedEmailExtraction()
        {
            string text = "Contact support@example.com or info@company.org for assistance. " +
                         "You can also email john.doe+tag@test.co.uk or jane_smith@domain.com. " +
                         "Invalid emails like user@domain (no extension) will not be extracted.";

            Console.WriteLine($"Text: {text}\n");

            List<string> emails = ExtractEmails(text);

            Console.WriteLine($"Total Emails Found: {emails.Count}\n");

            if (emails.Count > 0)
            {
                Console.WriteLine("Email Breakdown:");
                for (int i = 0; i < emails.Count; i++)
                {
                    string email = emails[i];
                    string[] parts = email.Split('@');
                    string username = parts[0];
                    string domain = parts[1];
                    string[] domainParts = domain.Split('.');

                    Console.WriteLine($"\n  Email {i + 1}: {email}");
                    Console.WriteLine($"    Username: {username}");
                    Console.WriteLine($"    Domain: {domain}");
                    Console.WriteLine($"    TLD: {domainParts[domainParts.Length - 1]}");
                }
            }
        }

        // More strict email validation pattern
        static List<string> ExtractEmailsStrict(string text)
        {
            List<string> emails = new List<string>();

            // Stricter pattern following RFC 5322 (simplified)
            string pattern = @"[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*";

            MatchCollection matches = Regex.Matches(text, pattern);

            foreach (Match match in matches)
            {
                emails.Add(match.Value);
            }

            return emails;
        }
    }
}
