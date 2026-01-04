using System;
using System.Collections.Generic;

namespace csharp_modelling
{
    // Association: Bank and Customers are associated; Customer can have accounts linked to a Bank.
    class BankAccount
    {
        public string AccountNumber { get; set; }
        public double Balance { get; private set; }

        public BankAccount(string accountNumber, double initialBalance = 0)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public void Deposit(double amount)
        {
            Balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (amount <= Balance) Balance -= amount;
            else Console.WriteLine("Insufficient funds.");
        }
    }

    class Customer
    {
        public string Name { get; set; }
        public Bank Bank { get; set; } // association
        public List<BankAccount> Accounts { get; private set; }

        public Customer(string name, Bank bank)
        {
            Name = name;
            Bank = bank;
            Accounts = new List<BankAccount>();
        }

        public void OpenAccount(string accNo, double initialDeposit = 0)
        {
            var acc = Bank.OpenAccount(accNo, initialDeposit);
            Accounts.Add(acc);
        }

        public void ViewBalances()
        {
            Console.WriteLine($"Balances for {Name} at {Bank.Name}:");
            foreach (var a in Accounts)
            {
                Console.WriteLine($" - {a.AccountNumber}: {a.Balance}");
            }
        }
    }

    class Bank
    {
        public string Name { get; set; }
        private List<BankAccount> _accounts = new List<BankAccount>();

        public Bank(string name)
        {
            Name = name;
        }

        public BankAccount OpenAccount(string accNo, double initialDeposit)
        {
            var acc = new BankAccount(accNo, initialDeposit);
            _accounts.Add(acc);
            return acc; // return account to associate with Customer
        }
    }

    class Program
    {
        static void Main()
        {
            var bank = new Bank("Community Bank");
            var customer = new Customer("Rita", bank);

            customer.OpenAccount("CUST1001", 500);
            customer.OpenAccount("CUST1002", 1500);

            customer.ViewBalances();
        }
    }
}