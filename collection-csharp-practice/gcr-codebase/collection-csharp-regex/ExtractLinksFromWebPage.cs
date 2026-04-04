using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionRegex.Extraction
{
    /// <summary>
    /// Problem 7: Extract Links from a Web Page
    /// Extracts all HTTP and HTTPS URLs from text
    /// </summary>
    class ExtractLinksFromWebPage
    {
        static void Main()
        {
            Console.WriteLine("=== Extract Links from Web Page ===\n");

            // Test cases
            string[] texts = new string[]
            {
                "Visit https://www.google.com and http://example.org for more info.",
                "Check out https://www.github.com, https://stackoverflow.com, and http://wikipedia.org",
                "No links in this text!",
                "Multiple protocols: https://secure.example.com, http://example.com, ftp://files.com (ftp not extracted)",
                "Links with paths: https://github.com/user/repo and https://example.com/page/index.html",
                "Long URL: https://www.example.com/path/to/page?query=value&another=param#section",
                "Subdomain links: https://api.github.com, https://mail.google.com, https://store.example.org",
            };

            foreach (string text in texts)
            {
                Console.WriteLine($"Text: {text}");
                Console.WriteLine("Extracted Links:");
                ExtractAndDisplayLinks(text);
                Console.WriteLine();
            }

            // Detailed analysis
            Console.WriteLine("\n--- Detailed Link Analysis ---\n");
            DetailedLinkAnalysis();
        }

        static void ExtractAndDisplayLinks(string text)
        {
            List<string> links = ExtractLinks(text);

            if (links.Count == 0)
            {
                Console.WriteLine("  (no links found)");
            }
            else
            {
                for (int i = 0; i < links.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {links[i]}");
                }
            }
        }

        static List<string> ExtractLinks(string text)
        {
            List<string> links = new List<string>();

            // Regex pattern for HTTP/HTTPS URLs
            // https?           - http or https
            // ://              - protocol separator
            // [a-zA-Z0-9.-]+   - domain name
            // (?:\.[a-zA-Z]{2,})+  - TLD and subdomains
            // (?:/[^\s]*)?     - optional path and query string

            string pattern = @"https?://(?:www\.)?[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}(?:/[^\s]*)?";

            MatchCollection matches = Regex.Matches(text, pattern);

            foreach (Match match in matches)
            {
                links.Add(match.Value);
            }

            return links;
        }

        static void DetailedLinkAnalysis()
        {
            string text = "Visit https://www.github.com/user/repo for code, https://stackoverflow.com for Q&A, " +
                         "and http://example.org for general info.";

            Console.WriteLine($"Text: {text}\n");

            List<string> links = ExtractLinks(text);

            Console.WriteLine($"Total Links Found: {links.Count}\n");

            for (int i = 0; i < links.Count; i++)
            {
                string link = links[i];
                AnalyzeLink(i + 1, link);
            }
        }

        static void AnalyzeLink(int number, string url)
        {
            Console.WriteLine($"Link {number}: {url}");

            Uri uri = new Uri(url);

            Console.WriteLine($"  Scheme: {uri.Scheme}");
            Console.WriteLine($"  Host: {uri.Host}");
            Console.WriteLine($"  Port: {uri.Port}");
            
            if (!string.IsNullOrEmpty(uri.PathAndQuery))
            {
                Console.WriteLine($"  Path: {uri.PathAndQuery}");
            }

            if (!string.IsNullOrEmpty(uri.Fragment))
            {
                Console.WriteLine($"  Fragment: {uri.Fragment}");
            }

            Console.WriteLine();
        }

        // Extract links with more details
        static List<LinkInfo> ExtractLinksDetailed(string text)
        {
            List<LinkInfo> linkInfos = new List<LinkInfo>();

            string pattern = @"https?://(?:www\.)?[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}(?:/[^\s]*)?";

            MatchCollection matches = Regex.Matches(text, pattern);

            foreach (Match match in matches)
            {
                string url = match.Value;
                Uri uri = new Uri(url);

                linkInfos.Add(new LinkInfo
                {
                    Url = url,
                    Scheme = uri.Scheme,
                    Domain = uri.Host,
                    Path = uri.PathAndQuery,
                    IsSecure = uri.Scheme == "https"
                });
            }

            return linkInfos;
        }

        // Helper class for link information
        class LinkInfo
        {
            public string Url { get; set; }
            public string Scheme { get; set; }
            public string Domain { get; set; }
            public string Path { get; set; }
            public bool IsSecure { get; set; }

            public override string ToString()
            {
                return $"{Url}";
            }
        }

        // Demonstrate different URL patterns
        static void DemonstrateDifferentURLPatterns()
        {
            Console.WriteLine("\n\n--- Different URL Patterns ---\n");

            string[] urls = new string[]
            {
                "https://www.google.com",
                "http://example.org",
                "https://github.com/user/repo",
                "https://example.com/path?query=value",
                "https://example.com:8080/path",
                "https://subdomain.example.com",
                "https://example.co.uk",
                "http://example.io/path/to/page?key=value&another=param#section",
            };

            Console.WriteLine("URL\t\t\t\tProtocol\tDomain");
            Console.WriteLine(new string('-', 70));

            foreach (string url in urls)
            {
                Uri uri = new Uri(url);
                Console.WriteLine($"{url,-40}\t{uri.Scheme,-12}\t{uri.Host}");
            }
        }

        // Extract specific types of links
        static List<string> ExtractSecureLinks(string text)
        {
            List<string> secureLinks = new List<string>();
            string pattern = @"https://(?:www\.)?[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}(?:/[^\s]*)?";

            MatchCollection matches = Regex.Matches(text, pattern);

            foreach (Match match in matches)
            {
                secureLinks.Add(match.Value);
            }

            return secureLinks;
        }

        static List<string> ExtractInsecureLinks(string text)
        {
            List<string> insecureLinks = new List<string>();
            string pattern = @"http://(?:www\.)?[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}(?:/[^\s]*)?";

            MatchCollection matches = Regex.Matches(text, pattern);

            foreach (Match match in matches)
            {
                insecureLinks.Add(match.Value);
            }

            return insecureLinks;
        }
    }
}
