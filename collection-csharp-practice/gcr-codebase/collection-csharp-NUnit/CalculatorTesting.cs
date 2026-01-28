using NUnit.Framework;
using System;

namespace CollectionNUnit.Basic
{
    /// <summary>
    /// Calculator Class: Implements basic arithmetic operations
    /// </summary>
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }

        public int Multiply(int a, int b)
        {
            return a * b;
        }

        public double Divide(int a, int b)
        {
            if (b == 0)
            {
                throw new ArithmeticException("Cannot divide by zero");
            }
            return (double)a / b;
        }
    }

    /// <summary>
    /// CalculatorTesting: NUnit test cases for Calculator class
    /// Tests basic arithmetic operations and exception handling
    /// </summary>
    [TestFixture]
    public class CalculatorTesting
    {
        private Calculator calculator;

        [SetUp]
        public void SetUp()
        {
            calculator = new Calculator();
        }

        #region Addition Tests

        [Test]
        public void Add_PositiveNumbers_ReturnsSum()
        {
            // Arrange & Act
            int result = calculator.Add(5, 3);

            // Assert
            Assert.AreEqual(8, result);
        }

        [Test]
        public void Add_NegativeNumbers_ReturnsCorrectSum()
        {
            // Arrange & Act
            int result = calculator.Add(-5, -3);

            // Assert
            Assert.AreEqual(-8, result);
        }

        [Test]
        public void Add_PositiveAndNegative_ReturnsCorrectSum()
        {
            // Arrange & Act
            int result = calculator.Add(10, -5);

            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Add_Zero_ReturnsOtherNumber()
        {
            // Arrange & Act
            int result = calculator.Add(0, 7);

            // Assert
            Assert.AreEqual(7, result);
        }

        #endregion

        #region Subtraction Tests

        [Test]
        public void Subtract_PositiveNumbers_ReturnsDifference()
        {
            // Arrange & Act
            int result = calculator.Subtract(10, 3);

            // Assert
            Assert.AreEqual(7, result);
        }

        [Test]
        public void Subtract_LargerFromSmaller_ReturnsNegative()
        {
            // Arrange & Act
            int result = calculator.Subtract(3, 10);

            // Assert
            Assert.AreEqual(-7, result);
        }

        [Test]
        public void Subtract_NegativeNumbers_ReturnsCorrectDifference()
        {
            // Arrange & Act
            int result = calculator.Subtract(-5, -3);

            // Assert
            Assert.AreEqual(-2, result);
        }

        [Test]
        public void Subtract_SameNumbers_ReturnsZero()
        {
            // Arrange & Act
            int result = calculator.Subtract(5, 5);

            // Assert
            Assert.AreEqual(0, result);
        }

        #endregion

        #region Multiplication Tests

        [Test]
        public void Multiply_PositiveNumbers_ReturnsProduct()
        {
            // Arrange & Act
            int result = calculator.Multiply(4, 5);

            // Assert
            Assert.AreEqual(20, result);
        }

        [Test]
        public void Multiply_NegativeNumbers_ReturnsPositiveProduct()
        {
            // Arrange & Act
            int result = calculator.Multiply(-4, -5);

            // Assert
            Assert.AreEqual(20, result);
        }

        [Test]
        public void Multiply_PositiveAndNegative_ReturnsNegativeProduct()
        {
            // Arrange & Act
            int result = calculator.Multiply(4, -5);

            // Assert
            Assert.AreEqual(-20, result);
        }

        [Test]
        public void Multiply_ByZero_ReturnsZero()
        {
            // Arrange & Act
            int result = calculator.Multiply(5, 0);

            // Assert
            Assert.AreEqual(0, result);
        }

        #endregion

        #region Division Tests

        [Test]
        public void Divide_PositiveNumbers_ReturnsQuotient()
        {
            // Arrange & Act
            double result = calculator.Divide(10, 2);

            // Assert
            Assert.AreEqual(5.0, result);
        }

        [Test]
        public void Divide_WithRemainder_ReturnsDecimal()
        {
            // Arrange & Act
            double result = calculator.Divide(10, 3);

            // Assert
            Assert.AreEqual(3.333, result, 0.01);
        }

        [Test]
        public void Divide_NegativeNumbers_ReturnsCorrectQuotient()
        {
            // Arrange & Act
            double result = calculator.Divide(-10, -2);

            // Assert
            Assert.AreEqual(5.0, result);
        }

        [Test]
        public void Divide_ByZero_ThrowsArithmeticException()
        {
            // Arrange & Act & Assert
            var ex = Assert.Throws<ArithmeticException>(() => calculator.Divide(10, 0));
            Assert.AreEqual("Cannot divide by zero", ex.Message);
        }

        [Test]
        public void Divide_ZeroDividedByNumber_ReturnsZero()
        {
            // Arrange & Act
            double result = calculator.Divide(0, 5);

            // Assert
            Assert.AreEqual(0.0, result);
        }

        #endregion

        #region Edge Cases

        [Test]
        public void Divide_LargeNumbers_ReturnsCorrectResult()
        {
            // Arrange & Act
            double result = calculator.Divide(1000000, 1000);

            // Assert
            Assert.AreEqual(1000.0, result);
        }

        [Test]
        public void Operations_WithOne_ReturnsExpectedResults()
        {
            // Arrange & Act & Assert
            Assert.AreEqual(6, calculator.Add(5, 1));
            Assert.AreEqual(4, calculator.Subtract(5, 1));
            Assert.AreEqual(5, calculator.Multiply(5, 1));
            Assert.AreEqual(5.0, calculator.Divide(5, 1));
        }

        #endregion

        [TearDown]
        public void TearDown()
        {
            calculator = null;
        }
    }
}
