using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Problem 1: Insurance Policy Management System
/// Store Unique Policies using HashSet, LinkedHashSet, and SortedSet.
/// Retrieve policies by expiry date, coverage type, etc.
/// </summary>
class InsurancePolicyManagementProgram
{
    class Policy
    {
        public string PolicyNumber { get; set; }
        public string HolderName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CoverageType { get; set; }
        public decimal Premium { get; set; }

        public Policy(string policyNumber, string holderName, DateTime expiryDate, string coverageType, decimal premium)
        {
            PolicyNumber = policyNumber;
            HolderName = holderName;
            ExpiryDate = expiryDate;
            CoverageType = coverageType;
            Premium = premium;
        }

        public override string ToString()
        {
            return $"{PolicyNumber} | {HolderName} | {CoverageType} | Exp: {ExpiryDate:yyyy-MM-dd} | Premium: Rs.{Premium}";
        }

        public override bool Equals(object obj)
        {
            return obj is Policy p && p.PolicyNumber == PolicyNumber;
        }

        public override int GetHashCode()
        {
            return PolicyNumber.GetHashCode();
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║  Insurance Policy Management System               ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Create policies
            List<Policy> policies = new List<Policy>
            {
                new Policy("POL001", "Rajesh Kumar", DateTime.Now.AddDays(15), "Health", 5000),
                new Policy("POL002", "Priya Singh", DateTime.Now.AddDays(45), "Auto", 3000),
                new Policy("POL003", "Amit Patel", DateTime.Now.AddDays(200), "Home", 8000),
                new Policy("POL004", "Neha Sharma", DateTime.Now.AddDays(5), "Health", 4500),
                new Policy("POL005", "Vikram Gupta", DateTime.Now.AddDays(100), "Auto", 3500),
                new Policy("POL001", "Rajesh Kumar", DateTime.Now.AddDays(15), "Health", 5000) // Duplicate
            };

            Console.WriteLine("=== 1. UNIQUE POLICIES (HashSet) ===");
            HashSet<Policy> uniquePolicies = new HashSet<Policy>(policies);
            Console.WriteLine($"Total unique policies: {uniquePolicies.Count}");
            Console.WriteLine("(Duplicates removed)\n");

            Console.WriteLine("=== 2. ALL POLICIES STORED ===");
            foreach (var policy in uniquePolicies)
            {
                Console.WriteLine(policy);
            }
            Console.WriteLine();

            Console.WriteLine("=== 3. POLICIES EXPIRING WITHIN 30 DAYS ===");
            DateTime thirtyDaysLater = DateTime.Now.AddDays(30);
            var expiringPolicies = uniquePolicies
                .Where(p => p.ExpiryDate <= thirtyDaysLater && p.ExpiryDate >= DateTime.Now)
                .OrderBy(p => p.ExpiryDate)
                .ToList();

            if (expiringPolicies.Count == 0)
            {
                Console.WriteLine("No policies expiring within 30 days");
            }
            else
            {
                foreach (var policy in expiringPolicies)
                {
                    int daysLeft = (int)(policy.ExpiryDate - DateTime.Now).TotalDays;
                    Console.WriteLine($"  {policy} (Expires in {daysLeft} days)");
                }
            }
            Console.WriteLine();

            Console.WriteLine("=== 4. POLICIES BY COVERAGE TYPE ===");
            var groupedByType = uniquePolicies.GroupBy(p => p.CoverageType);
            foreach (var group in groupedByType)
            {
                Console.WriteLine($"{group.Key}: {group.Count()} policies");
                foreach (var policy in group)
                {
                    Console.WriteLine($"  {policy}");
                }
            }
            Console.WriteLine();

            Console.WriteLine("=== 5. SORTED BY EXPIRY DATE ===");
            var sortedByExpiry = uniquePolicies.OrderBy(p => p.ExpiryDate).ToList();
            foreach (var policy in sortedByExpiry)
            {
                Console.WriteLine(policy);
            }
            Console.WriteLine();

            Console.WriteLine("=== 6. POLICY STATISTICS ===");
            Console.WriteLine($"Total Policies: {uniquePolicies.Count}");
            Console.WriteLine($"Total Premium Revenue: Rs.{uniquePolicies.Sum(p => p.Premium)}");
            Console.WriteLine($"Average Premium: Rs.{uniquePolicies.Average(p => p.Premium):F2}");
            Console.WriteLine($"Coverage Types: {string.Join(", ", uniquePolicies.Select(p => p.CoverageType).Distinct())}");
            Console.WriteLine();

            Console.WriteLine("✓ Insurance Policy Management System simulation completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }
}
