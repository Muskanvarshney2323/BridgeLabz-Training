using System;

namespace csharp_keywords
{
    class BankAccount
    {
        public static string BankName = "Global Bank";
        private static int totalAccounts = 0;

        public readonly string AccountNumber;
        public string AccountHolderName;
        public double Balance;

        public BankAccount(string AccountHolderName, string AccountNumber, double initialBalance)
        {
            this.AccountHolderName = AccountHolderName; // using this to disambiguate
            this.AccountNumber = AccountNumber; // readonly assigned in constructor
            this.Balance = initialBalance;
            totalAccounts++;
        }

        public static void GetTotalAccounts()
        {
            Console.WriteLine($"Total accounts: {totalAccounts}");
        }

        public void DisplayDetails(object obj)
        {
            if (obj is BankAccount acc)
            {
                Console.WriteLine("--- Bank Account ---");
                Console.WriteLine($"Bank: {BankName}");
                Console.WriteLine($"Holder: {acc.AccountHolderName}");
                Console.WriteLine($"Account No: {acc.AccountNumber}");
                Console.WriteLine($"Balance: {acc.Balance}");
            }
            else
            {
                Console.WriteLine("Object is not a BankAccount instance.");
            }
        }

        // small demo
        static void Main()
        {
            var a1 = new BankAccount("Alice", "ACC1001", 5000);
            var a2 = new BankAccount("Bob", "ACC1002", 3000);

            a1.DisplayDetails(a1);
            a2.DisplayDetails(a2);

            BankAccount.GetTotalAccounts();
        }
    }
}