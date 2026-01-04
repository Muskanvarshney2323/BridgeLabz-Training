using System;

namespace csharp_inheritance
{
    class BankAccount
    {
        public string AccountNumber { get; set; }
        public double Balance { get; set; }

        public BankAccount(string accNo, double balance)
        {
            AccountNumber = accNo;
            Balance = balance;
        }

        public virtual void DisplayAccountType()
        {
            Console.WriteLine($"Account {AccountNumber} Balance {Balance}");
        }
    }

    class SavingsAccount : BankAccount
    {
        public double InterestRate { get; set; }

        public SavingsAccount(string accNo, double bal, double rate) : base(accNo, bal)
        {
            InterestRate = rate;
        }

        public override void DisplayAccountType()
        {
            Console.WriteLine($"Savings Account {AccountNumber}, Interest: {InterestRate}%");
        }
    }

    class CheckingAccount : BankAccount
    {
        public double WithdrawalLimit { get; set; }

        public CheckingAccount(string accNo, double bal, double limit) : base(accNo, bal)
        {
            WithdrawalLimit = limit;
        }

        public override void DisplayAccountType()
        {
            Console.WriteLine($"Checking Account {AccountNumber}, WithdrawalLimit: {WithdrawalLimit}");
        }
    }

    class FixedDepositAccount : BankAccount
    {
        public int MaturityMonths { get; set; }

        public FixedDepositAccount(string accNo, double bal, int months) : base(accNo, bal)
        {
            MaturityMonths = months;
        }

        public override void DisplayAccountType()
        {
            Console.WriteLine($"Fixed Deposit {AccountNumber}, Matures in: {MaturityMonths} months");
        }
    }

    class Program
    {
        static void Main()
        {
            BankAccount[] accounts = new BankAccount[]
            {
                new SavingsAccount("SA001", 5000, 4.5),
                new CheckingAccount("CA001", 2000, 1000),
                new FixedDepositAccount("FD001", 10000, 12)
            };

            foreach (var a in accounts)
            {
                a.DisplayAccountType();
            }
        }
    }
}