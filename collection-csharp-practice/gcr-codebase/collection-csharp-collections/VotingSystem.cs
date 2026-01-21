using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Problem 2: Design a Voting System
/// Use Dictionary to store votes, SortedDictionary to display results in order.
/// </summary>
class VotingSystemProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║           Design a Voting System                  ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Candidates and votes
            Dictionary<string, int> votes = new Dictionary<string, int>();

            // Simulate voting
            Console.WriteLine("=== Voting in Progress ===");
            List<string> ballots = new List<string>
            {
                "Alice", "Bob", "Alice", "Charlie", "Bob", "Alice",
                "David", "Charlie", "Bob", "Alice", "Charlie", "David",
                "Alice", "Bob"
            };

            Console.WriteLine($"Total votes cast: {ballots.Count}\n");

            // Count votes using Dictionary
            foreach (var candidate in ballots)
            {
                if (votes.ContainsKey(candidate))
                {
                    votes[candidate]++;
                }
                else
                {
                    votes[candidate] = 1;
                }
            }

            Console.WriteLine("=== VOTING RESULTS (Alphabetical Order) ===");
            SortedDictionary<string, int> sortedVotes = new SortedDictionary<string, int>(votes);
            foreach (var kvp in sortedVotes)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value} votes");
            }
            Console.WriteLine();

            Console.WriteLine("=== VOTING RESULTS (By Vote Count - Descending) ===");
            var votesByCount = votes.OrderByDescending(x => x.Value);
            int rank = 1;
            foreach (var kvp in votesByCount)
            {
                double percentage = (kvp.Value * 100.0) / ballots.Count;
                Console.WriteLine($"{rank}. {kvp.Key}: {kvp.Value} votes ({percentage:F1}%)");
                rank++;
            }
            Console.WriteLine();

            Console.WriteLine("=== WINNER ===");
            var winner = votes.OrderByDescending(x => x.Value).First();
            Console.WriteLine($"🏆 Candidate: {winner.Key}");
            Console.WriteLine($"Votes: {winner.Value}");
            Console.WriteLine($"Vote Share: {(winner.Value * 100.0 / ballots.Count):F1}%");
            Console.WriteLine();

            Console.WriteLine("=== STATISTICS ===");
            Console.WriteLine($"Total Candidates: {votes.Count}");
            Console.WriteLine($"Total Votes: {ballots.Count}");
            Console.WriteLine($"Average Votes per Candidate: {votes.Average(x => x.Value):F2}");
            Console.WriteLine($"Highest Votes: {votes.Values.Max()}");
            Console.WriteLine($"Lowest Votes: {votes.Values.Min()}");
            Console.WriteLine();

            // Maintain order of voting (using LinkedHashMap equivalent)
            Console.WriteLine("=== VOTE TIMELINE (Order of Arrival) ===");
            LinkedHashSet<string> voteOrder = new LinkedHashSet<string>(ballots);
            int voteNum = 1;
            foreach (var candidate in ballots)
            {
                Console.WriteLine($"Vote #{voteNum}: {candidate}");
                voteNum++;
            }
            Console.WriteLine();

            Console.WriteLine("✓ Voting System simulation completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }
}
