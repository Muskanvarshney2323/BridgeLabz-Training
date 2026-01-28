using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionRegex.Advanced
{
    /// <summary>
    /// Problem 10: Validate an IP Address
    /// A valid IPv4 address consists of four groups of numbers (0-255) separated by dots
    /// </summary>
    class ValidateIPAddress
    {
        static void Main()
        {
            Console.WriteLine("=== Validate IP Address ===\n");

            // Test cases
            string[] testCases = new string[]
            {
                "192.168.1.1",      // ✅ Valid
                "10.0.0.1",         // ✅ Valid
                "255.255.255.255",  // ✅ Valid
                "0.0.0.0",          // ✅ Valid
                "127.0.0.1",        // ✅ Valid (localhost)
                "172.16.0.1",       // ✅ Valid
                "256.1.1.1",        // ❌ Invalid - first octet > 255
                "192.168.1",        // ❌ Invalid - only 3 octets
                "192.168.1.1.1",    // ❌ Invalid - 5 octets
                "192.168.a.1",      // ❌ Invalid - letter instead of number
                "192.168.01.1",     // ❌ Invalid - leading zero (some systems)
                "192.168.-1.1",     // ❌ Invalid - negative number
                "192 .168.1.1",     // ❌ Invalid - space
                ".192.168.1.1",     // ❌ Invalid - leading dot
                "192.168.1.1.",     // ❌ Invalid - trailing dot
                "192..168.1.1",     // ❌ Invalid - consecutive dots
                "192.168.1.1a",     // ❌ Invalid - trailing character
            };

            ValidateIPAddresses(testCases);

            // Detailed validation
            Console.WriteLine("\n\n--- Detailed IP Validation ---\n");
            DetailedIPValidation();
        }

        static bool IsValidIPAddress(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
                return false;

            // Regex pattern explanation:
            // ^                    - Start of string
            // ([0-9]{1,3}\.){3}    - Three octets followed by dots (each 1-3 digits)
            // [0-9]{1,3}           - Fourth octet (1-3 digits)
            // $                    - End of string
            // This checks format, but not range (0-255)

            string pattern = @"^([0-9]{1,3}\.){3}[0-9]{1,3}$";

            if (!Regex.IsMatch(ipAddress, pattern))
                return false;

            // Validate each octet is 0-255
            string[] octets = ipAddress.Split('.');
            foreach (string octet in octets)
            {
                if (int.TryParse(octet, out int num))
                {
                    if (num < 0 || num > 255)
                        return false;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        static void ValidateIPAddresses(string[] ipAddresses)
        {
            Console.WriteLine("{0,-20} {1,-15} {2,-30}", "IP Address", "Validity", "Message");
            Console.WriteLine(new string('-', 65));

            foreach (string ip in ipAddresses)
            {
                bool isValid = IsValidIPAddress(ip);
                string validity = isValid ? "✅ Valid" : "❌ Invalid";
                string message = GetValidationMessage(ip);

                Console.WriteLine("{0,-20} {1,-15} {2,-30}", ip, validity, message);
            }
        }

        static string GetValidationMessage(string ip)
        {
            if (IsValidIPAddress(ip))
                return "Valid IPv4 address";

            if (string.IsNullOrEmpty(ip))
                return "Empty input";

            string pattern = @"^([0-9]{1,3}\.){3}[0-9]{1,3}$";
            if (!Regex.IsMatch(ip, pattern))
            {
                int dotCount = ip.Split('.').Length - 1;
                if (dotCount != 3)
                    return $"Wrong octet count ({dotCount + 1})";
                return "Invalid format";
            }

            // Check ranges
            string[] octets = ip.Split('.');
            for (int i = 0; i < octets.Length; i++)
            {
                if (int.TryParse(octets[i], out int num))
                {
                    if (num > 255)
                        return $"Octet {i + 1} > 255";
                    if (num < 0)
                        return $"Octet {i + 1} < 0";
                }
            }

            return "Invalid";
        }

        static void DetailedIPValidation()
        {
            string testIP = "192.168.1.100";

            Console.WriteLine($"IP Address: {testIP}\n");

            if (IsValidIPAddress(testIP))
            {
                string[] octets = testIP.Split('.');

                Console.WriteLine("Octet Breakdown:");
                for (int i = 0; i < octets.Length; i++)
                {
                    int octet = int.Parse(octets[i]);
                    Console.WriteLine($"  Octet {i + 1}: {octet} (0-255 range: ✓)");
                }

                Console.WriteLine($"\nIP Class: {GetIPClass(octets[0])}");
                Console.WriteLine($"IP Type: {GetIPType(testIP)}");
            }
        }

        static string GetIPClass(string firstOctet)
        {
            int octet = int.Parse(firstOctet);

            return octet switch
            {
                >= 1 and <= 126 => "Class A",
                >= 128 and <= 191 => "Class B",
                >= 192 and <= 223 => "Class C",
                >= 224 and <= 239 => "Class D (Multicast)",
                >= 240 and <= 255 => "Class E (Reserved)",
                _ => "Unknown"
            };
        }

        static string GetIPType(string ip)
        {
            return ip switch
            {
                "127.0.0.1" => "Localhost",
                "0.0.0.0" => "Any/Unknown address",
                "255.255.255.255" => "Broadcast",
                _ when ip.StartsWith("10.") => "Private (Class A)",
                _ when ip.StartsWith("172.16.") || ip.StartsWith("172.17.") || 
                       ip.StartsWith("172.18.") || ip.StartsWith("172.19.") ||
                       ip.StartsWith("172.20.") || ip.StartsWith("172.21.") ||
                       ip.StartsWith("172.22.") || ip.StartsWith("172.23.") ||
                       ip.StartsWith("172.24.") || ip.StartsWith("172.25.") ||
                       ip.StartsWith("172.26.") || ip.StartsWith("172.27.") ||
                       ip.StartsWith("172.28.") || ip.StartsWith("172.29.") ||
                       ip.StartsWith("172.30.") || ip.StartsWith("172.31.") => "Private (Class B)",
                _ when ip.StartsWith("192.168.") => "Private (Class C)",
                _ => "Public"
            };
        }

        // More comprehensive IP validation pattern
        static bool IsValidIPAddressComprehensive(string ip)
        {
            // Pattern that validates range in one go
            // (?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}
            // This pattern checks:
            // - 25[0-5] matches 250-255
            // - 2[0-4][0-9] matches 200-249
            // - [01]?[0-9][0-9]? matches 0-199

            string pattern = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
            return Regex.IsMatch(ip, pattern);
        }

        // Display common IP address examples
        static void DisplayCommonIPs()
        {
            Console.WriteLine("\n\n--- Common IP Address Examples ---\n");

            var commonIPs = new[]
            {
                ("127.0.0.1", "Localhost"),
                ("192.168.1.1", "Private - Router"),
                ("10.0.0.1", "Private - Network"),
                ("8.8.8.8", "Public - Google DNS"),
                ("1.1.1.1", "Public - Cloudflare DNS"),
                ("255.255.255.255", "Broadcast"),
                ("0.0.0.0", "Any/Unknown"),
            };

            Console.WriteLine("{0,-20} {1,-20}", "IP Address", "Description");
            Console.WriteLine(new string('-', 40));

            foreach (var (ip, desc) in commonIPs)
            {
                Console.WriteLine("{0,-20} {1,-20}", ip, desc);
            }
        }
    }
}
