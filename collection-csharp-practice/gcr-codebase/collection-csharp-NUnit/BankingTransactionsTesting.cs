using NUnit.Framework;
using System;

namespace CollectionNUnit.Advanced
{
    /// <summary>
    /// BankAccount Class: Manages account balance with deposit and withdraw operations
    /// </summary>
    public class BankAccount
    {
        private double balance;
        private string accountNumber;

        public BankAccount(string accountNumber, double initialBalance = 0)
        {
            accountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
            if (initialBalance < 0)
            {
                throw new ArgumentException("Initial balance cannot be negative");
            }

            this.accountNumber = accountNumber;
            this.balance = initialBalance;
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive");
            }

            balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive");
            }

            if (amount > balance)
            {
                throw new InvalidOperationException("Insufficient funds for withdrawal");
            }

            balance -= amount;
        }

        public double GetBalance()
        {
            return balance;
        }

        public string GetAccountNumber()
        {
            return accountNumber;
        }

        public void Transfer(BankAccount recipient, double amount)
        {
            if (recipient == null)
            {
                throw new ArgumentNullException(nameof(recipient));
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Transfer amount must be positive");
            }

            this.Withdraw(amount);
            recipient.Deposit(amount);
        }
    }

    /// <summary>
    /// BankingTransactionsTesting: NUnit test cases for BankAccount class
    /// Tests deposits, withdrawals, balance updates, and transaction validations
    /// </summary>
    [TestFixture]
    public class BankingTransactionsTesting
    {
        private BankAccount account;

        [SetUp]
        public void SetUp()
        {
            account = new BankAccount("ACC001", 1000);
        }

        #region Deposit Tests

        [Test]
        public void Deposit_ValidAmount_IncreasesBalance()
        {
            // Arrange
            double initialBalance = account.GetBalance();
            double depositAmount = 500;

            // Act
            account.Deposit(depositAmount);

            // Assert
            Assert.AreEqual(initialBalance + depositAmount, account.GetBalance());
        }

        [Test]
        public void Deposit_ZeroAmount_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => account.Deposit(0));
        }

        [Test]
        public void Deposit_NegativeAmount_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => account.Deposit(-100));
        }

        [Test]
        public void Deposit_SmallAmount_IncreasesBalance()
        {
            // Arrange
            double initialBalance = account.GetBalance();

            // Act
            account.Deposit(0.01);

            // Assert
            Assert.AreEqual(initialBalance + 0.01, account.GetBalance(), 0.001);
        }

        [Test]
        public void Deposit_LargeAmount_IncreasesBalance()
        {
            // Arrange
            double initialBalance = account.GetBalance();

            // Act
            account.Deposit(1000000);

            // Assert
            Assert.AreEqual(initialBalance + 1000000, account.GetBalance());
        }

        [Test]
        public void Deposit_MultipleDeposits_BalanceAccumulates()
        {
            // Act
            account.Deposit(100);
            account.Deposit(200);
            account.Deposit(300);

            // Assert
            Assert.AreEqual(1600, account.GetBalance());
        }

        #endregion

        #region Withdrawal Tests

        [Test]
        public void Withdraw_ValidAmount_DecreasesBalance()
        {
            // Arrange
            double initialBalance = account.GetBalance();
            double withdrawAmount = 300;

            // Act
            account.Withdraw(withdrawAmount);

            // Assert
            Assert.AreEqual(initialBalance - withdrawAmount, account.GetBalance());
        }

        [Test]
        public void Withdraw_EntireBalance_BalanceBecomesZero()
        {
            // Act
            account.Withdraw(1000);

            // Assert
            Assert.AreEqual(0, account.GetBalance());
        }

        [Test]
        public void Withdraw_MoreThanBalance_ThrowsInvalidOperationException()
        {
            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => account.Withdraw(1500));
            Assert.AreEqual("Insufficient funds for withdrawal", ex.Message);
        }

        [Test]
        public void Withdraw_ZeroAmount_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => account.Withdraw(0));
        }

        [Test]
        public void Withdraw_NegativeAmount_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => account.Withdraw(-100));
        }

        [Test]
        public void Withdraw_SmallAmount_DecreasesBalance()
        {
            // Act
            account.Withdraw(0.01);

            // Assert
            Assert.AreEqual(999.99, account.GetBalance(), 0.001);
        }

        [Test]
        public void Withdraw_MultipleWithdrawals_BalanceCorrect()
        {
            // Act
            account.Withdraw(100);
            account.Withdraw(200);
            account.Withdraw(300);

            // Assert
            Assert.AreEqual(400, account.GetBalance());
        }

        [Test]
        public void Withdraw_FromEmptyAccount_ThrowsException()
        {
            // Arrange
            BankAccount emptyAccount = new BankAccount("ACC002", 0);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => emptyAccount.Withdraw(1));
        }

        #endregion

        #region Balance Tests

        [Test]
        public void GetBalance_InitialBalance_CorrectValue()
        {
            // Assert
            Assert.AreEqual(1000, account.GetBalance());
        }

        [Test]
        public void GetBalance_AfterOperations_CorrectValue()
        {
            // Act
            account.Deposit(500);
            account.Withdraw(200);

            // Assert
            Assert.AreEqual(1300, account.GetBalance());
        }

        [Test]
        public void GetBalance_ZeroBalance_ReturnsZero()
        {
            // Arrange
            BankAccount zeroAccount = new BankAccount("ACC003", 0);

            // Assert
            Assert.AreEqual(0, zeroAccount.GetBalance());
        }

        #endregion

        #region Insufficient Funds Tests

        [Test]
        public void Withdraw_InsufficientFunds_BalanceRemained()
        {
            // Arrange
            double initialBalance = account.GetBalance();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => account.Withdraw(2000));
            Assert.AreEqual(initialBalance, account.GetBalance());
        }

        [Test]
        public void Withdraw_PartialInsufficientFunds_BalanceRemained()
        {
            // Arrange
            account.Withdraw(700);
            double remainingBalance = account.GetBalance();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => account.Withdraw(400));
            Assert.AreEqual(remainingBalance, account.GetBalance());
        }

        #endregion

        #region Account Creation Tests

        [Test]
        public void CreateAccount_WithAccountNumber_Successful()
        {
            // Arrange & Act
            BankAccount newAccount = new BankAccount("ACC100", 500);

            // Assert
            Assert.AreEqual("ACC100", newAccount.GetAccountNumber());
            Assert.AreEqual(500, newAccount.GetBalance());
        }

        [Test]
        public void CreateAccount_WithNullAccountNumber_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BankAccount(null, 1000));
        }

        [Test]
        public void CreateAccount_WithNegativeBalance_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new BankAccount("ACC100", -100));
        }

        [Test]
        public void CreateAccount_WithZeroBalance_Successful()
        {
            // Arrange & Act
            BankAccount emptyAccount = new BankAccount("ACC100", 0);

            // Assert
            Assert.AreEqual(0, emptyAccount.GetBalance());
        }

        #endregion

        #region Transfer Tests

        [Test]
        public void Transfer_ValidAmount_TransfersCorrectly()
        {
            // Arrange
            BankAccount recipient = new BankAccount("ACC200", 500);

            // Act
            account.Transfer(recipient, 300);

            // Assert
            Assert.AreEqual(700, account.GetBalance());
            Assert.AreEqual(800, recipient.GetBalance());
        }

        [Test]
        public void Transfer_ToNullAccount_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => account.Transfer(null, 100));
        }

        [Test]
        public void Transfer_InsufficientFunds_ThrowsException()
        {
            // Arrange
            BankAccount recipient = new BankAccount("ACC200", 500);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => account.Transfer(recipient, 2000));
        }

        [Test]
        public void Transfer_NegativeAmount_ThrowsException()
        {
            // Arrange
            BankAccount recipient = new BankAccount("ACC200", 500);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => account.Transfer(recipient, -100));
        }

        #endregion

        #region Edge Cases

        [Test]
        public void DepositAndWithdraw_SameAmount_BalanceUnchanged()
        {
            // Arrange
            double initialBalance = account.GetBalance();

            // Act
            account.Deposit(500);
            account.Withdraw(500);

            // Assert
            Assert.AreEqual(initialBalance, account.GetBalance());
        }

        [Test]
        public void MultipleTransactions_ComplexSequence_CorrectBalance()
        {
            // Act
            account.Deposit(500);
            account.Withdraw(200);
            account.Deposit(100);
            account.Withdraw(150);

            // Assert
            Assert.AreEqual(1250, account.GetBalance());
        }

        #endregion

        [TearDown]
        public void TearDown()
        {
            account = null;
        }
    }
}
