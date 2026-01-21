using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Problem 4: Implement a Banking System
/// Use Dictionary for account balances, SortedDictionary to sort by balance,
/// Queue for processing withdrawal requests.
/// </summary>
class BankingSystemProgram
{
    class Account
    {
        public int AccountNumber { get; set; }
        public string HolderName { get; set; }
        public double Balance { get; set; }

        public Account(int accountNumber, string holderName, double balance)
        {
            AccountNumber = accountNumber;
            HolderName = holderName;
            Balance = balance;
        }

        public override string ToString()
        {
            return $"Acc#{AccountNumber} | {HolderName} | Balance: Rs.{Balance:F2}";
        }
    }

    class WithdrawalRequest
    {
        public int AccountNumber { get; set; }
        public double Amount { get; set; }
        public DateTime RequestTime { get; set; }

        public WithdrawalRequest(int accountNumber, double amount)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            RequestTime = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Withdrawal Request: Acc#{AccountNumber}, Amount: Rs.{Amount:F2}";
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║         Implement a Banking System                ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Accounts dictionary
            Dictionary<int, Account> accounts = new Dictionary<int, Account>
            {
                { 1001, new Account(1001, "Rajesh Kumar", 50000) },
                { 1002, new Account(1002, "Priya Singh", 75000) },
                { 1003, new Account(1003, "Amit Patel", 30000) },
                { 1004, new Account(1004, "Neha Sharma", 100000) },
                { 1005, new Account(1005, "Vikram Gupta", 45000) }
            };

            // Withdrawal queue
            Queue<WithdrawalRequest> withdrawalQueue = new Queue<WithdrawalRequest>();

            Console.WriteLine("=== BANKING SYSTEM ===\n");

            // Display all accounts
            Console.WriteLine("=== ALL ACCOUNTS ===");
            foreach (var account in accounts.Values)
            {
                Console.WriteLine($"  {account}");
            }
            Console.WriteLine();

            // Display sorted by balance
            Console.WriteLine("=== ACCOUNTS SORTED BY BALANCE (Descending) ===");
            var sortedByBalance = accounts.Values.OrderByDescending(x => x.Balance);
            foreach (var account in sortedByBalance)
            {
                Console.WriteLine($"  {account}");
            }
            Console.WriteLine();

            // Process transactions
            Console.WriteLine("=== PROCESSING TRANSACTIONS ===\n");

            // Deposit
            Console.WriteLine("Deposit Rs.10,000 to Account 1001");
            Deposit(accounts, 1001, 10000);
            Console.WriteLine($"  ✓ New Balance: Rs.{accounts[1001].Balance:F2}\n");

            // Withdrawal (immediate)
            Console.WriteLine("Withdrawal Rs.15,000 from Account 1002");
            Withdraw(accounts, 1002, 15000);
            Console.WriteLine($"  ✓ New Balance: Rs.{accounts[1002].Balance:F2}\n");

            // Failed withdrawal attempt
            Console.WriteLine("Attempt to withdraw Rs.50,000 from Account 1003");
            Withdraw(accounts, 1003, 50000);
            Console.WriteLine();

            // Queue withdrawal requests
            Console.WriteLine("=== QUEUING WITHDRAWAL REQUESTS ===");
            withdrawalQueue.Enqueue(new WithdrawalRequest(1004, 20000));
            Console.WriteLine("✓ Request 1 queued: Account 1004, Rs.20,000");
            
            withdrawalQueue.Enqueue(new WithdrawalRequest(1005, 5000));
            Console.WriteLine("✓ Request 2 queued: Account 1005, Rs.5,000");
            
            withdrawalQueue.Enqueue(new WithdrawalRequest(1001, 25000));
            Console.WriteLine("✓ Request 3 queued: Account 1001, Rs.25,000");
            Console.WriteLine();

            // Process withdrawal queue
            Console.WriteLine("=== PROCESSING WITHDRAWAL QUEUE ===");
            int requestNum = 1;
            while (withdrawalQueue.Count > 0)
            {
                WithdrawalRequest request = withdrawalQueue.Dequeue();
                Console.WriteLine($"\nProcessing Request #{requestNum}:");
                Console.WriteLine($"  {request}");
                
                if (accounts.ContainsKey(request.AccountNumber))
                {
                    Withdraw(accounts, request.AccountNumber, request.Amount);
                    Console.WriteLine($"  New Balance: Rs.{accounts[request.AccountNumber].Balance:F2}");
                }
                else
                {
                    Console.WriteLine($"  ✗ Account not found");
                }
                
                requestNum++;
            }
            Console.WriteLine();

            // Bank statistics
            Console.WriteLine("=== BANK STATISTICS ===");
            double totalBalance = accounts.Values.Sum(x => x.Balance);
            double avgBalance = accounts.Values.Average(x => x.Balance);
            double maxBalance = accounts.Values.Max(x => x.Balance);
            double minBalance = accounts.Values.Min(x => x.Balance);

            Console.WriteLine($"Total Accounts: {accounts.Count}");
            Console.WriteLine($"Total Deposits: Rs.{totalBalance:F2}");
            Console.WriteLine($"Average Balance: Rs.{avgBalance:F2}");
            Console.WriteLine($"Highest Balance: Rs.{maxBalance:F2}");
            Console.WriteLine($"Lowest Balance: Rs.{minBalance:F2}");
            Console.WriteLine();

            // Final account status
            Console.WriteLine("=== FINAL ACCOUNT STATUS ===");
            var finalSorted = new SortedDictionary<int, Account>(accounts);
            foreach (var kvp in finalSorted)
            {
                Console.WriteLine($"  {kvp.Value}");
            }
            Console.WriteLine();

            Console.WriteLine("✓ Banking System simulation completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static void Deposit(Dictionary<int, Account> accounts, int accountNumber, double amount)
    {
        if (accounts.ContainsKey(accountNumber))
        {
            if (amount > 0)
            {
                accounts[accountNumber].Balance += amount;
                Console.WriteLine($"  ✓ Deposit successful");
            }
            else
            {
                Console.WriteLine($"  ✗ Invalid amount");
            }
        }
        else
        {
            Console.WriteLine($"  ✗ Account not found");
        }
    }

    static void Withdraw(Dictionary<int, Account> accounts, int accountNumber, double amount)
    {
        if (!accounts.ContainsKey(accountNumber))
        {
            Console.WriteLine($"  ✗ Account not found");
            return;
        }

        if (amount <= 0)
        {
            Console.WriteLine($"  ✗ Invalid amount");
            return;
        }

        if (accounts[accountNumber].Balance >= amount)
        {
            accounts[accountNumber].Balance -= amount;
            Console.WriteLine($"  ✓ Withdrawal successful");
        }
        else
        {
            Console.WriteLine($"  ✗ Insufficient balance (Available: Rs.{accounts[accountNumber].Balance:F2})");
        }
    }
}
