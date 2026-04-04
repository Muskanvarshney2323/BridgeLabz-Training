using NUnit.Framework;
using System;

namespace CollectionNUnit.Basic
{
    /// <summary>
    /// DatabaseConnection Class: Simulates database connection lifecycle
    /// </summary>
    public class DatabaseConnection
    {
        private bool isConnected = false;

        public void Connect()
        {
            // Simulate connecting to database
            System.Threading.Thread.Sleep(100);
            isConnected = true;
            Console.WriteLine("✓ Database connected");
        }

        public void Disconnect()
        {
            // Simulate disconnecting from database
            System.Threading.Thread.Sleep(50);
            isConnected = false;
            Console.WriteLine("✓ Database disconnected");
        }

        public bool IsConnected()
        {
            return isConnected;
        }

        public string ExecuteQuery(string query)
        {
            if (!isConnected)
            {
                throw new InvalidOperationException("Database is not connected");
            }

            // Simulate query execution
            return $"Query executed: {query}";
        }

        public void PerformTransaction(Action transaction)
        {
            if (!isConnected)
            {
                throw new InvalidOperationException("Database is not connected");
            }

            transaction?.Invoke();
        }
    }

    /// <summary>
    /// SetupAndTeardownTesting: NUnit test cases demonstrating SetUp and TearDown patterns
    /// Tests database connection lifecycle with proper setup and cleanup
    /// </summary>
    [TestFixture]
    public class SetupAndTeardownTesting
    {
        private DatabaseConnection connection;

        // [SetUp] runs before EACH test method
        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("\n--- [SetUp] Called ---");
            connection = new DatabaseConnection();
            connection.Connect();
            Assert.IsTrue(connection.IsConnected(), "Connection should be established");
        }

        // [TearDown] runs after EACH test method
        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("--- [TearDown] Called ---");
            if (connection != null && connection.IsConnected())
            {
                connection.Disconnect();
            }
            connection = null;
        }

        #region Basic Setup/Teardown Tests

        [Test]
        public void Test_One_ConnectionIsActive()
        {
            Console.WriteLine("Test_One: Executing");
            
            // Arrange, Act & Assert
            Assert.IsTrue(connection.IsConnected());
            Assert.IsNotNull(connection);
        }

        [Test]
        public void Test_Two_ConnectionIsActive()
        {
            Console.WriteLine("Test_Two: Executing");
            
            // Arrange, Act & Assert
            Assert.IsTrue(connection.IsConnected());
            Assert.IsNotNull(connection);
        }

        [Test]
        public void Test_Three_ConnectionIsActive()
        {
            Console.WriteLine("Test_Three: Executing");
            
            // Arrange, Act & Assert
            Assert.IsTrue(connection.IsConnected());
            Assert.IsNotNull(connection);
        }

        #endregion

        #region Query Execution Tests

        [Test]
        public void ExecuteQuery_ValidQuery_ReturnsResult()
        {
            // Act
            string result = connection.ExecuteQuery("SELECT * FROM Users");

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Does.Contain("Query executed"));
        }

        [Test]
        public void ExecuteQuery_WithParameters_ReturnsResult()
        {
            // Act
            string result = connection.ExecuteQuery("INSERT INTO Users VALUES ('John', 'john@example.com')");

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Does.Contain("Query executed"));
        }

        [Test]
        public void ExecuteQuery_SelectStatement_ReturnsResult()
        {
            // Act
            string result = connection.ExecuteQuery("SELECT Name FROM Users WHERE ID = 1");

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ExecuteQuery_MultipleQueries_AllExecuteSuccessfully()
        {
            // Act
            string result1 = connection.ExecuteQuery("SELECT * FROM Users");
            string result2 = connection.ExecuteQuery("SELECT * FROM Orders");
            string result3 = connection.ExecuteQuery("SELECT * FROM Products");

            // Assert
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);
        }

        #endregion

        #region Connection State Tests

        [Test]
        public void Connection_AfterSetup_IsConnected()
        {
            // Assert
            Assert.IsTrue(connection.IsConnected());
        }

        [Test]
        public void Connection_ObjectNotNull_AfterSetup()
        {
            // Assert
            Assert.IsNotNull(connection);
        }

        [Test]
        public void Connection_MultipleChecks_StaysConnected()
        {
            // Act & Assert
            for (int i = 0; i < 5; i++)
            {
                Assert.IsTrue(connection.IsConnected());
            }
        }

        #endregion

        #region Exception Tests

        [Test]
        public void ExecuteQuery_WithoutConnection_ThrowsException()
        {
            // Arrange
            connection.Disconnect();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => connection.ExecuteQuery("SELECT * FROM Users"));
        }

        [Test]
        public void ExecuteQuery_AfterDisconnect_ThrowsInvalidOperationException()
        {
            // Arrange
            connection.Disconnect();
            Assert.IsFalse(connection.IsConnected());

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => connection.ExecuteQuery("SELECT *"));
            Assert.AreEqual("Database is not connected", ex.Message);
        }

        #endregion

        #region Transaction Tests

        [Test]
        public void PerformTransaction_ValidAction_Executes()
        {
            // Arrange
            bool transactionExecuted = false;

            // Act
            connection.PerformTransaction(() => { transactionExecuted = true; });

            // Assert
            Assert.IsTrue(transactionExecuted);
        }

        [Test]
        public void PerformTransaction_WithoutConnection_ThrowsException()
        {
            // Arrange
            connection.Disconnect();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => 
                connection.PerformTransaction(() => { })
            );
        }

        [Test]
        public void PerformTransaction_ComplexOperation_Executes()
        {
            // Arrange
            int counter = 0;

            // Act
            connection.PerformTransaction(() =>
            {
                counter++;
                var result1 = connection.ExecuteQuery("SELECT * FROM Users");
                counter++;
                var result2 = connection.ExecuteQuery("SELECT * FROM Orders");
                counter++;
            });

            // Assert
            Assert.AreEqual(3, counter);
        }

        #endregion

        #region Setup/Teardown Verification Tests

        [Test]
        [Order(1)]
        public void First_Test_VerifySetupRuns()
        {
            // Verify that SetUp was called by checking connection state
            Assert.IsNotNull(connection, "SetUp should have created connection");
            Assert.IsTrue(connection.IsConnected(), "SetUp should have connected");
        }

        [Test]
        [Order(2)]
        public void Second_Test_NewInstanceCreated()
        {
            // Each test should have its own fresh connection instance
            Assert.IsNotNull(connection);
            Assert.IsTrue(connection.IsConnected());
        }

        [Test]
        [Order(3)]
        public void Third_Test_FreshConnection()
        {
            // This should also have a fresh connection instance from SetUp
            Assert.IsNotNull(connection);
            Assert.IsTrue(connection.IsConnected());
        }

        #endregion

        #region Resource Management Tests

        [Test]
        public void Setup_CreatesNewConnection()
        {
            // Verify that SetUp creates a new instance each time
            Assert.IsNotNull(connection);
            Assert.IsInstanceOf<DatabaseConnection>(connection);
        }

        [Test]
        public void Teardown_Releases_Connection()
        {
            // This test verifies that after execution, TearDown will clean up
            Assert.IsNotNull(connection);
            // After this test, TearDown will disconnect and set to null
        }

        #endregion
    }

    /// <summary>
    /// Additional test class demonstrating OneTimeSetUp and OneTimeTearDown
    /// These run once for the entire fixture, not for each test
    /// </summary>
    [TestFixture]
    public class OneTimeSetupTeardownTesting
    {
        private static DatabaseConnection sharedConnection;
        private int testCount;

        // [OneTimeSetUp] runs ONCE before ALL tests in the fixture
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Console.WriteLine("\n=== [OneTimeSetUp] - Called ONCE at start ===");
            sharedConnection = new DatabaseConnection();
            sharedConnection.Connect();
            testCount = 0;
        }

        // [OneTimeTearDown] runs ONCE after ALL tests in the fixture
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Console.WriteLine("\n=== [OneTimeTearDown] - Called ONCE at end ===");
            if (sharedConnection != null && sharedConnection.IsConnected())
            {
                sharedConnection.Disconnect();
            }
        }

        [SetUp]
        public void SetUp()
        {
            testCount++;
            Console.WriteLine($"[SetUp] Test #{testCount}");
        }

        [Test]
        public void OneTimeTest_One()
        {
            Console.WriteLine("OneTimeTest_One: Executing");
            Assert.IsTrue(sharedConnection.IsConnected());
        }

        [Test]
        public void OneTimeTest_Two()
        {
            Console.WriteLine("OneTimeTest_Two: Executing");
            Assert.IsTrue(sharedConnection.IsConnected());
        }

        [Test]
        public void OneTimeTest_Three()
        {
            Console.WriteLine("OneTimeTest_Three: Executing");
            Assert.IsTrue(sharedConnection.IsConnected());
        }
    }
}
