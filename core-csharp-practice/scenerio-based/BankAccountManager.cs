using System;

class BankAccount
{
    public int accountNumber;
    public string name;
    public int pin;
    public double balance;

    // Constructor
    public BankAccount(int accNo, string accName, int accPin, double initialBalance)
    {
        accountNumber = accNo;
        name = accName;
        pin = accPin;
        balance = initialBalance;
    }

    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            balance += amount;
            Console.WriteLine("Deposit Successfull");
        }
        else
        {
            Console.WriteLine("Invalid Amount");
        }
    }

    public void Withdraw(double amount)
    {
        if (amount > 0 && amount<= balance)
        {
            balance = balance - amount;
            Console.WriteLine("Withdrawal Successful");
        }
        else
        {
            Console.WriteLine("Invalid Amount or Insufficient Balance");
        }
    }

    public void CheckBalance()
    {
        Console.WriteLine("Current Balance: " + balance);
    }
}

class Program
{
    static void Main()
    {
        BankAccount[] accounts = new BankAccount[5];
        int count = 0;
        int accNoGenerator = 1001;

        while (true)
        {
            Console.WriteLine("\n--- Banking App ---");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Choose option: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Console.Write("Enter Name: ");
                string name = Console.ReadLine();

                Console.Write("Set PIN: ");
                int pin = int.Parse(Console.ReadLine());

                Console.Write("Initial Balance: ");
                double balance = double.Parse(Console.ReadLine());

                accounts[count] = new BankAccount(accNoGenerator, name, pin, balance);
                Console.WriteLine("Account Created Successfully");
                Console.WriteLine("Your Account Number: " + accNoGenerator);

                accNoGenerator++;
                count++;
            }
            else if (choice == 2)
            {
                Console.Write("Enter Account Number: ");
                int accNo = int.Parse(Console.ReadLine());

                Console.Write("Enter PIN: ");
                int pin = int.Parse(Console.ReadLine());

                BankAccount currentAccount = null;

                for (int i = 0; i < count; i++)
                {
                    if (accounts[i].accountNumber == accNo && accounts[i].pin == pin)
                    {
                        currentAccount = accounts[i];
                        break;
                    }
                }

                if (currentAccount == null)
                {
                    Console.WriteLine("Invalid Login");
                }
                else
                {
                    Console.WriteLine("Login Successful");

                    while (true)
                    {
                        Console.WriteLine("\n1. Deposit");
                        Console.WriteLine("2. Withdraw");
                        Console.WriteLine("3. Check Balance");
                        Console.WriteLine("4. Logout");
                        Console.Write("Choose option: ");
                        int opt = int.Parse(Console.ReadLine());

                        if (opt == 1)
                        {
                            Console.Write("Enter Amount: ");
                            currentAccount.Deposit(double.Parse(Console.ReadLine()));
                        }
                        else if (opt == 2)
                        {
                            Console.Write("Enter Amount: ");
                            currentAccount.Withdraw(double.Parse(Console.ReadLine()));
                        }
                        else if (opt == 3)
                        {
                            currentAccount.CheckBalance();
                        }
                        else if (opt == 4)
                        {
                            Console.WriteLine("Logged Out");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Option");
                        }
                    }
                }
            }
            else if (choice == 3)
            {
                Console.WriteLine("Thank You!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid Choice");
            }
        }
    }
}
