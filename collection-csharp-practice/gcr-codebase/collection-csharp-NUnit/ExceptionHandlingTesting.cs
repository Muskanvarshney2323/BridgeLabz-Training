using NUnit.Framework;
using System;

namespace CollectionNUnit.Basic
{
    /// <summary>
    /// DivideOperation Class: Implements division with exception handling
    /// </summary>
    public class DivideOperation
    {
        public double Divide(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new ArithmeticException("Division by zero is not allowed");
            }

            return (double)numerator / denominator;
        }

        public int IntegerDivide(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be zero", nameof(denominator));
            }

            return numerator / denominator;
        }

        public double SafeDivide(int numerator, int denominator, double defaultValue)
        {
            try
            {
                return Divide(numerator, denominator);
            }
            catch (ArithmeticException)
            {
                return defaultValue;
            }
        }
    }

    /// <summary>
    /// ExceptionHandlingTesting: NUnit test cases for exception handling
    /// Tests that exceptions are thrown properly for invalid operations
    /// </summary>
    [TestFixture]
    public class ExceptionHandlingTesting
    {
        private DivideOperation divideOperation;

        [SetUp]
        public void SetUp()
        {
            divideOperation = new DivideOperation();
        }

        #region Basic Exception Tests

        [Test]
        public void Divide_ByZero_ThrowsArithmeticException()
        {
            // Act & Assert
            Assert.Throws<ArithmeticException>(() => divideOperation.Divide(10, 0));
        }

        [Test]
        public void Divide_ByZero_ExceptionHasCorrectMessage()
        {
            // Act & Assert
            var ex = Assert.Throws<ArithmeticException>(() => divideOperation.Divide(10, 0));
            Assert.AreEqual("Division by zero is not allowed", ex.Message);
        }

        [Test]
        public void Divide_ByZero_ExceptionIsNotNull()
        {
            // Act & Assert
            var ex = Assert.Throws<ArithmeticException>(() => divideOperation.Divide(10, 0));
            Assert.IsNotNull(ex);
        }

        [Test]
        public void Divide_ValidNumbers_DoesNotThrowException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => divideOperation.Divide(10, 2));
        }

        #endregion

        #region Exception Message Tests

        [Test]
        public void Divide_ByZero_MessageContainsKeyword()
        {
            // Act & Assert
            var ex = Assert.Throws<ArithmeticException>(() => divideOperation.Divide(5, 0));
            Assert.That(ex.Message, Does.Contain("zero"));
        }

        [Test]
        public void IntegerDivide_ByZero_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => divideOperation.IntegerDivide(10, 0));
        }

        [Test]
        public void IntegerDivide_ByZero_ArgumentExceptionHasParameterName()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => divideOperation.IntegerDivide(10, 0));
            Assert.AreEqual("denominator", ex.ParamName);
        }

        #endregion

        #region Multiple Exception Tests

        [Test]
        public void Divide_ZeroNumerator_NoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => divideOperation.Divide(0, 5));
        }

        [Test]
        public void Divide_ZeroNumerator_ReturnsZero()
        {
            // Act
            double result = divideOperation.Divide(0, 5);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Divide_NegativeDenominator_NoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => divideOperation.Divide(10, -5));
        }

        [Test]
        public void Divide_NegativeDenominator_ReturnsNegativeResult()
        {
            // Act
            double result = divideOperation.Divide(10, -5);

            // Assert
            Assert.AreEqual(-2, result);
        }

        #endregion

        #region Exception Type Tests

        [Test]
        public void Divide_ByZero_IsArithmeticExceptionType()
        {
            // Act & Assert
            var ex = Assert.Throws<ArithmeticException>(() => divideOperation.Divide(10, 0));
            Assert.IsInstanceOf<ArithmeticException>(ex);
        }

        [Test]
        public void Divide_ByZero_IsExceptionType()
        {
            // Act & Assert
            var ex = Assert.Throws<ArithmeticException>(() => divideOperation.Divide(10, 0));
            Assert.IsInstanceOf<Exception>(ex);
        }

        [Test]
        public void IntegerDivide_ByZero_IsArgumentExceptionType()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => divideOperation.IntegerDivide(10, 0));
            Assert.IsInstanceOf<ArgumentException>(ex);
        }

        #endregion

        #region Safe Division Tests

        [Test]
        public void SafeDivide_ByZero_ReturnsDefaultValue()
        {
            // Act
            double result = divideOperation.SafeDivide(10, 0, -1.0);

            // Assert
            Assert.AreEqual(-1.0, result);
        }

        [Test]
        public void SafeDivide_ValidDivision_ReturnsCorrectResult()
        {
            // Act
            double result = divideOperation.SafeDivide(10, 2, -1.0);

            // Assert
            Assert.AreEqual(5.0, result);
        }

        [Test]
        public void SafeDivide_ByZero_WithDifferentDefaults_ReturnsCorrectDefault()
        {
            // Act
            double result1 = divideOperation.SafeDivide(10, 0, 0.0);
            double result2 = divideOperation.SafeDivide(10, 0, 999.0);

            // Assert
            Assert.AreEqual(0.0, result1);
            Assert.AreEqual(999.0, result2);
        }

        #endregion

        #region Exception Properties Tests

        [Test]
        public void Divide_ByZero_ExceptionHasStackTrace()
        {
            // Act & Assert
            var ex = Assert.Throws<ArithmeticException>(() => divideOperation.Divide(10, 0));
            Assert.IsNotNull(ex.StackTrace);
        }

        [Test]
        public void Divide_ByZero_ExceptionHasSource()
        {
            // Act & Assert
            var ex = Assert.Throws<ArithmeticException>(() => divideOperation.Divide(10, 0));
            Assert.IsNotNull(ex.Source);
        }

        #endregion

        #region Sequential Exception Tests

        [Test]
        public void Divide_MultipleZeroDivisions_AllThrowExceptions()
        {
            // Act & Assert
            for (int i = 0; i < 3; i++)
            {
                Assert.Throws<ArithmeticException>(() => divideOperation.Divide(i, 0));
            }
        }

        [Test]
        public void Divide_ValidThenInvalid_SecondThrowsException()
        {
            // Act
            double validResult = divideOperation.Divide(10, 2);

            // Assert
            Assert.AreEqual(5.0, validResult);
            Assert.Throws<ArithmeticException>(() => divideOperation.Divide(10, 0));
        }

        #endregion

        #region Integration Tests

        [Test]
        public void ExceptionHandling_CatchAndHandle_ExceptionCaught()
        {
            // Act & Assert
            try
            {
                divideOperation.Divide(10, 0);
                Assert.Fail("Exception was not thrown");
            }
            catch (ArithmeticException ex)
            {
                Assert.IsTrue(ex.Message.Contains("zero"));
            }
        }

        [Test]
        public void ExceptionHandling_TryMultipleOperations_CorrectExceptionThrown()
        {
            // Act & Assert
            var ex = Assert.Throws<ArithmeticException>(() =>
            {
                divideOperation.Divide(5, 2);
                divideOperation.Divide(10, 0);
                divideOperation.Divide(8, 4);
            });

            Assert.That(ex.Message, Does.Contain("zero"));
        }

        #endregion

        [TearDown]
        public void TearDown()
        {
            divideOperation = null;
        }
    }
}
