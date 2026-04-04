using System;
using System.Collections.Generic;

namespace OopsPractice.EncapsulationAbstractionPolymorphism
{
    interface ILoanable
    {
        bool ApplyForLoan(double amount);
        double CalculateLoanEligibility();
    }

    abstract class BankAccount
    {
        private string _accountNumber;
        private string _holderName;
        protected double Balance;

        protected BankAccount(string accNo, string holder, double initial)
        {
            _accountNumber = accNo; _holderName = holder; Balance = initial;
        }

        public string AccountNumber => _accountNumber;
        public string HolderName => _holderName;

        public void Deposit(double amt) => Balance += amt;
        public bool Withdraw(double amt)
        {
            if (amt <= Balance) { Balance -= amt; return true; }
            return false;
        }

        public abstract double CalculateInterest();
    }

    class SavingsAccount : BankAccount, ILoanable
    {
        public SavingsAccount(string acc, string h, double bal) : base(acc, h, bal) { }
        public override double CalculateInterest() => Balance * 0.04;
        public bool ApplyForLoan(double amount) => CalculateLoanEligibility() >= amount;
        public double CalculateLoanEligibility() => Balance * 2; // simple rule
    }

    class CurrentAccount : BankAccount
    {
        public CurrentAccount(string acc, string h, double bal) : base(acc, h, bal) { }
        public override double CalculateInterest() => 0; // no interest
    }

    class Program
    {
        static void Main()
        {
            var accounts = new List<BankAccount>
            {
                new SavingsAccount("S001","John",10000), new CurrentAccount("C001","Corp",50000)
            };

            foreach (var a in accounts)
            {
                Console.WriteLine($"{a.HolderName} ({a.AccountNumber}) Balance: {a.CalculateInterest():C} interest");
            }
        }
    }
}