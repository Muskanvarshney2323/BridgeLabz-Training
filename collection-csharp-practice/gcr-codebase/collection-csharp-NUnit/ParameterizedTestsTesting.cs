using NUnit.Framework;
using System;

namespace CollectionNUnit.Basic
{
    /// <summary>
    /// EvenNumberChecker: Checks if numbers are even or odd
    /// </summary>
    public class EvenNumberChecker
    {
        public bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        public bool IsOdd(int number)
        {
            return number % 2 != 0;
        }

        public string GetNumberType(int number)
        {
            if (IsEven(number))
            {
                return "Even";
            }
            else
            {
                return "Odd";
            }
        }

        public bool IsDivisibleBy(int number, int divisor)
        {
            if (divisor == 0)
            {
                throw new ArgumentException("Divisor cannot be zero", nameof(divisor));
            }

            return number % divisor == 0;
        }
    }

    /// <summary>
    /// ParameterizedTestsTesting: NUnit test cases using [TestCase] attribute
    /// Tests IsEven method with multiple parameterized values
    /// </summary>
    [TestFixture]
    public class ParameterizedTestsTesting
    {
        private EvenNumberChecker checker;

        [SetUp]
        public void SetUp()
        {
            checker = new EvenNumberChecker();
        }

        #region Basic Parameterized Tests

        [TestCase(2)]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(100)]
        public void IsEven_EvenNumbers_ReturnsTrue(int number)
        {
            // Act
            bool result = checker.IsEven(number);

            // Assert
            Assert.IsTrue(result, $"{number} should be even");
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(99)]
        public void IsEven_OddNumbers_ReturnsFalse(int number)
        {
            // Act
            bool result = checker.IsEven(number);

            // Assert
            Assert.IsFalse(result, $"{number} should be odd");
        }

        #endregion

        #region Multiple Parameter Tests

        [TestCase(2, "Even")]
        [TestCase(3, "Odd")]
        [TestCase(4, "Even")]
        [TestCase(5, "Odd")]
        [TestCase(10, "Even")]
        [TestCase(15, "Odd")]
        public void GetNumberType_VariousNumbers_ReturnsCorrectType(int number, string expectedType)
        {
            // Act
            string result = checker.GetNumberType(number);

            // Assert
            Assert.AreEqual(expectedType, result);
        }

        #endregion

        #region Negative Numbers Tests

        [TestCase(-2)]
        [TestCase(-4)]
        [TestCase(-100)]
        public void IsEven_NegativeEvenNumbers_ReturnsTrue(int number)
        {
            // Act
            bool result = checker.IsEven(number);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-99)]
        public void IsEven_NegativeOddNumbers_ReturnsFalse(int number)
        {
            // Act
            bool result = checker.IsEven(number);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region Edge Cases Tests

        [TestCase(0)]
        public void IsEven_Zero_ReturnsTrue(int number)
        {
            // Act
            bool result = checker.IsEven(number);

            // Assert
            Assert.IsTrue(result, "Zero should be considered even");
        }

        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        public void IsEven_BoundaryValues_WorksCorrectly(int number)
        {
            // Act
            bool result = checker.IsEven(number);

            // Assert - int.MinValue is even (-2147483648)
            // int.MaxValue is odd (2147483647)
            if (number == int.MinValue)
            {
                Assert.IsTrue(result);
            }
            else if (number == int.MaxValue)
            {
                Assert.IsFalse(result);
            }
        }

        #endregion

        #region IsOdd Parameterized Tests

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(99)]
        public void IsOdd_OddNumbers_ReturnsTrue(int number)
        {
            // Act
            bool result = checker.IsOdd(number);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase(2)]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(100)]
        public void IsOdd_EvenNumbers_ReturnsFalse(int number)
        {
            // Act
            bool result = checker.IsOdd(number);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region Divisibility Tests

        [TestCase(10, 2)]
        [TestCase(15, 3)]
        [TestCase(20, 4)]
        [TestCase(25, 5)]
        [TestCase(100, 10)]
        public void IsDivisibleBy_DivisibleNumbers_ReturnsTrue(int number, int divisor)
        {
            // Act
            bool result = checker.IsDivisibleBy(number, divisor);

            // Assert
            Assert.IsTrue(result, $"{number} should be divisible by {divisor}");
        }

        [TestCase(10, 3)]
        [TestCase(15, 4)]
        [TestCase(20, 3)]
        [TestCase(25, 3)]
        [TestCase(100, 3)]
        public void IsDivisibleBy_NotDivisibleNumbers_ReturnsFalse(int number, int divisor)
        {
            // Act
            bool result = checker.IsDivisibleBy(number, divisor);

            // Assert
            Assert.IsFalse(result, $"{number} should not be divisible by {divisor}");
        }

        #endregion

        #region Exception Tests with Parameters

        [TestCase(10, 0)]
        [TestCase(5, 0)]
        [TestCase(100, 0)]
        public void IsDivisibleBy_ByZero_ThrowsArgumentException(int number, int divisor)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => checker.IsDivisibleBy(number, divisor));
        }

        #endregion

        #region Range of Values Tests

        [TestCase(2)]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        [TestCase(14)]
        [TestCase(16)]
        [TestCase(18)]
        [TestCase(20)]
        public void IsEven_SmallPositiveEvenNumbers_ReturnsTrue(int number)
        {
            // Act
            bool result = checker.IsEven(number);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(9)]
        [TestCase(11)]
        [TestCase(13)]
        [TestCase(15)]
        [TestCase(17)]
        [TestCase(19)]
        public void IsEven_SmallPositiveOddNumbers_ReturnsFalse(int number)
        {
            // Act
            bool result = checker.IsEven(number);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region Combination Tests

        [TestCase(2, 1)]
        [TestCase(4, 0)]
        [TestCase(6, 1)]
        [TestCase(8, 0)]
        public void IsEven_And_GetRemainder_CombinedLogic(int number, int expectedRemainder)
        {
            // Act
            bool isEven = checker.IsEven(number);
            int remainder = number % 2;

            // Assert
            Assert.IsTrue(isEven);
            Assert.AreEqual(expectedRemainder, remainder);
        }

        #endregion

        [TearDown]
        public void TearDown()
        {
            checker = null;
        }
    }

    /// <summary>
    /// Extended parameterized tests with values attribute
    /// </summary>
    [TestFixture]
    public class ValuesParameterizedTesting
    {
        private EvenNumberChecker checker;

        [SetUp]
        public void SetUp()
        {
            checker = new EvenNumberChecker();
        }

        [Test]
        public void IsEven_WithValuesAttribute([Values(2, 4, 6, 8, 10)] int evenNumber)
        {
            // Act
            bool result = checker.IsEven(evenNumber);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsOdd_WithValuesAttribute([Values(1, 3, 5, 7, 9)] int oddNumber)
        {
            // Act
            bool result = checker.IsOdd(oddNumber);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsDivisibleBy_WithValuesAttribute(
            [Values(10, 20, 30, 40)] int number,
            [Values(2, 5)] int divisor)
        {
            // Act
            bool result = checker.IsDivisibleBy(number, divisor);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
