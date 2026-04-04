using NUnit.Framework;
using System;

namespace CollectionNUnit.Advanced
{
    /// <summary>
    /// TemperatureConverter Class: Converts between Celsius and Fahrenheit
    /// Formula: F = (C × 9/5) + 32
    /// Formula: C = (F - 32) × 5/9
    /// </summary>
    public class TemperatureConverter
    {
        public double CelsiusToFahrenheit(double celsius)
        {
            return (celsius * 9.0 / 5.0) + 32.0;
        }

        public double FahrenheitToCelsius(double fahrenheit)
        {
            return (fahrenheit - 32.0) * 5.0 / 9.0;
        }

        public bool IsValidTemperature(double celsius)
        {
            // Absolute zero in Celsius is -273.15°C
            return celsius >= -273.15;
        }

        public string GetTemperatureCategory(double celsius)
        {
            if (celsius < -50)
                return "Extremely Cold";
            else if (celsius < 0)
                return "Freezing";
            else if (celsius < 15)
                return "Cold";
            else if (celsius < 25)
                return "Moderate";
            else if (celsius < 35)
                return "Warm";
            else
                return "Hot";
        }
    }

    /// <summary>
    /// TemperatureConverterTesting: NUnit test cases for TemperatureConverter class
    /// Tests temperature conversions and validations
    /// </summary>
    [TestFixture]
    public class TemperatureConverterTesting
    {
        private TemperatureConverter converter;

        [SetUp]
        public void SetUp()
        {
            converter = new TemperatureConverter();
        }

        #region Celsius to Fahrenheit Tests

        [Test]
        public void CelsiusToFahrenheit_Freezing_Returns32()
        {
            // Act
            double result = converter.CelsiusToFahrenheit(0);

            // Assert
            Assert.AreEqual(32.0, result, 0.01);
        }

        [Test]
        public void CelsiusToFahrenheit_BoilingPoint_Returns212()
        {
            // Act
            double result = converter.CelsiusToFahrenheit(100);

            // Assert
            Assert.AreEqual(212.0, result, 0.01);
        }

        [Test]
        public void CelsiusToFahrenheit_RoomTemperature_CorrectValue()
        {
            // Act
            double result = converter.CelsiusToFahrenheit(20);

            // Assert
            Assert.AreEqual(68.0, result, 0.01);
        }

        [Test]
        public void CelsiusToFahrenheit_NegativeTemperature_CorrectValue()
        {
            // Act
            double result = converter.CelsiusToFahrenheit(-10);

            // Assert
            Assert.AreEqual(14.0, result, 0.01);
        }

        [TestCase(0, 32)]
        [TestCase(10, 50)]
        [TestCase(20, 68)]
        [TestCase(30, 86)]
        [TestCase(40, 104)]
        [TestCase(50, 122)]
        public void CelsiusToFahrenheit_VariousTemperatures_CorrectConversion(double celsius, double expectedFahrenheit)
        {
            // Act
            double result = converter.CelsiusToFahrenheit(celsius);

            // Assert
            Assert.AreEqual(expectedFahrenheit, result, 0.1);
        }

        #endregion

        #region Fahrenheit to Celsius Tests

        [Test]
        public void FahrenheitToCelsius_Freezing_ReturnsZero()
        {
            // Act
            double result = converter.FahrenheitToCelsius(32);

            // Assert
            Assert.AreEqual(0.0, result, 0.01);
        }

        [Test]
        public void FahrenheitToCelsius_BoilingPoint_Returns100()
        {
            // Act
            double result = converter.FahrenheitToCelsius(212);

            // Assert
            Assert.AreEqual(100.0, result, 0.01);
        }

        [Test]
        public void FahrenheitToCelsius_RoomTemperature_CorrectValue()
        {
            // Act
            double result = converter.FahrenheitToCelsius(68);

            // Assert
            Assert.AreEqual(20.0, result, 0.01);
        }

        [Test]
        public void FahrenheitToCelsius_NegativeTemperature_CorrectValue()
        {
            // Act
            double result = converter.FahrenheitToCelsius(14);

            // Assert
            Assert.AreEqual(-10.0, result, 0.01);
        }

        [TestCase(32, 0)]
        [TestCase(50, 10)]
        [TestCase(68, 20)]
        [TestCase(86, 30)]
        [TestCase(104, 40)]
        [TestCase(122, 50)]
        public void FahrenheitToCelsius_VariousTemperatures_CorrectConversion(double fahrenheit, double expectedCelsius)
        {
            // Act
            double result = converter.FahrenheitToCelsius(fahrenheit);

            // Assert
            Assert.AreEqual(expectedCelsius, result, 0.1);
        }

        #endregion

        #region Reciprocal Conversion Tests

        [Test]
        public void ConversionsAreReciprocal_CelsiusToFahrenheitAndBack()
        {
            // Arrange
            double originalCelsius = 25;

            // Act
            double fahrenheit = converter.CelsiusToFahrenheit(originalCelsius);
            double backToCelsius = converter.FahrenheitToCelsius(fahrenheit);

            // Assert
            Assert.AreEqual(originalCelsius, backToCelsius, 0.01);
        }

        [Test]
        public void ConversionsAreReciprocal_FahrenheitToCelsiusAndBack()
        {
            // Arrange
            double originalFahrenheit = 77;

            // Act
            double celsius = converter.FahrenheitToCelsius(originalFahrenheit);
            double backToFahrenheit = converter.CelsiusToFahrenheit(celsius);

            // Assert
            Assert.AreEqual(originalFahrenheit, backToFahrenheit, 0.01);
        }

        #endregion

        #region Extreme Temperature Tests

        [Test]
        public void CelsiusToFahrenheit_AbsoluteZero_CorrectValue()
        {
            // Act
            double result = converter.CelsiusToFahrenheit(-273.15);

            // Assert
            Assert.AreEqual(-459.67, result, 0.01);
        }

        [Test]
        public void CelsiusToFahrenheit_VeryHighTemperature_CorrectValue()
        {
            // Act
            double result = converter.CelsiusToFahrenheit(1000);

            // Assert
            Assert.Greater(result, 1800);
        }

        #endregion

        #region Decimal Precision Tests

        [Test]
        public void CelsiusToFahrenheit_DecimalValue_MaintainsPrecision()
        {
            // Act
            double result = converter.CelsiusToFahrenheit(36.6);

            // Assert
            Assert.AreEqual(97.88, result, 0.01);
        }

        [Test]
        public void FahrenheitToCelsius_DecimalValue_MaintainsPrecision()
        {
            // Act
            double result = converter.FahrenheitToCelsius(98.6);

            // Assert
            Assert.AreEqual(37.0, result, 0.1);
        }

        #endregion

        #region Temperature Validation Tests

        [Test]
        public void IsValidTemperature_AboveAbsoluteZero_ReturnsTrue()
        {
            // Act
            bool result = converter.IsValidTemperature(0);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidTemperature_RoomTemperature_ReturnsTrue()
        {
            // Act
            bool result = converter.IsValidTemperature(20);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidTemperature_AbsoluteZero_ReturnsTrue()
        {
            // Act
            bool result = converter.IsValidTemperature(-273.15);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidTemperature_BelowAbsoluteZero_ReturnsFalse()
        {
            // Act
            bool result = converter.IsValidTemperature(-300);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase(-273.15, true)]
        [TestCase(-273.14, true)]
        [TestCase(-273.16, false)]
        [TestCase(-274, false)]
        public void IsValidTemperature_VaryingTemperatures_CorrectValidation(double temperature, bool expected)
        {
            // Act
            bool result = converter.IsValidTemperature(temperature);

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region Temperature Category Tests

        [Test]
        public void GetTemperatureCategory_BelowMinus50_ReturnsExtremelyC()
        {
            // Act
            string category = converter.GetTemperatureCategory(-60);

            // Assert
            Assert.AreEqual("Extremely Cold", category);
        }

        [Test]
        public void GetTemperatureCategory_Freezing_ReturnsFreezing()
        {
            // Act
            string category = converter.GetTemperatureCategory(-10);

            // Assert
            Assert.AreEqual("Freezing", category);
        }

        [Test]
        public void GetTemperatureCategory_Cold_ReturnsCold()
        {
            // Act
            string category = converter.GetTemperatureCategory(5);

            // Assert
            Assert.AreEqual("Cold", category);
        }

        [Test]
        public void GetTemperatureCategory_Moderate_ReturnsModerate()
        {
            // Act
            string category = converter.GetTemperatureCategory(20);

            // Assert
            Assert.AreEqual("Moderate", category);
        }

        [Test]
        public void GetTemperatureCategory_Warm_ReturnsWarm()
        {
            // Act
            string category = converter.GetTemperatureCategory(30);

            // Assert
            Assert.AreEqual("Warm", category);
        }

        [Test]
        public void GetTemperatureCategory_Hot_ReturnsHot()
        {
            // Act
            string category = converter.GetTemperatureCategory(40);

            // Assert
            Assert.AreEqual("Hot", category);
        }

        #endregion

        #region Integration Tests

        [Test]
        public void Integration_ConvertAndCategorize_Correct()
        {
            // Arrange
            double celsius = 25;

            // Act
            double fahrenheit = converter.CelsiusToFahrenheit(celsius);
            string category = converter.GetTemperatureCategory(celsius);
            bool isValid = converter.IsValidTemperature(celsius);

            // Assert
            Assert.AreEqual(77.0, fahrenheit, 0.1);
            Assert.AreEqual("Moderate", category);
            Assert.IsTrue(isValid);
        }

        [Test]
        public void Integration_ConvertBodyTemperature_AllCorrect()
        {
            // Arrange
            double celsiusTemp = 37;

            // Act
            double fahrenheitTemp = converter.CelsiusToFahrenheit(celsiusTemp);
            string category = converter.GetTemperatureCategory(celsiusTemp);

            // Assert
            Assert.AreEqual(98.6, fahrenheitTemp, 0.1);
            Assert.AreEqual("Warm", category);
        }

        #endregion

        [TearDown]
        public void TearDown()
        {
            converter = null;
        }
    }
}
