using System;

class BankAccount
{
    // Access modifiers
    public int accountNumber;          // Public
    protected string accountHolder;    // Protected
    private double balance;            // Private

    // Constructor
    public BankAccount(int accNo, string holder, double bal)
    {
        accountNumber = accNo;
        accountHolder = holder;
        balance = bal;
    }

    // Public method to get balance
    public double GetBalance()
    {
        return balance;
    }

    // Public method to set balance
    public void SetBalance(double amount)
    {
        if (amount >= 0)
        {
            balance = amount;
        }
    }
}

// Subclass
class SavingsAccount : BankAccount
{
    double interestRate;

    public SavingsAccount(int accNo, string holder, double bal, double rate)
        : base(accNo, holder, bal)
    {
        interestRate = rate;
    }

    public void DisplaySavingsAccount()
    {
        Console.WriteLine("Account Number: " + accountNumber); // public
        Console.WriteLine("Account Holder: " + accountHolder); // protected
        Console.WriteLine("Balance: " + GetBalance());         // private via method
        Console.WriteLine("Interest Rate: " + interestRate);
    }
}

class Program
{
    static void Main()
    {
        SavingsAccount sa =
            new SavingsAccount(12345, "Rohit", 5000, 4.5);

        sa.DisplaySavingsAccount();

        Console.WriteLine();

        // Modify balance using public method
        sa.SetBalance(7000);
        Console.WriteLine("Updated Balance: " + sa.GetBalance());
    }
}
