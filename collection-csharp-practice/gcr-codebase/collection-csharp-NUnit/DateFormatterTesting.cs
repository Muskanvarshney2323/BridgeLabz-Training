using NUnit.Framework;
using System;
using System.Globalization;

namespace CollectionNUnit.Advanced
{
    /// <summary>
    /// DateFormatter Class: Formats dates between different formats
    /// Converts from yyyy-MM-dd to dd-MM-yyyy format
    /// </summary>
    public class DateFormatter
    {
        public string FormatDate(string inputDate)
        {
            if (string.IsNullOrEmpty(inputDate))
            {
                throw new ArgumentException("Input date cannot be null or empty");
            }

            // Try to parse yyyy-MM-dd format
            if (DateTime.TryParseExact(inputDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, 
                DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate.ToString("dd-MM-yyyy");
            }

            throw new FormatException($"Invalid date format: {inputDate}. Expected format: yyyy-MM-dd");
        }

        public bool IsValidDate(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return false;
            }

            return DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out _);
        }

        public DateTime ParseDate(string inputDate)
        {
            if (string.IsNullOrEmpty(inputDate))
            {
                throw new ArgumentException("Input date cannot be null or empty");
            }

            if (!DateTime.TryParseExact(inputDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime parsedDate))
            {
                throw new FormatException($"Invalid date format: {inputDate}. Expected format: yyyy-MM-dd");
            }

            return parsedDate;
        }

        public int GetDayOfWeek(string inputDate)
        {
            DateTime parsedDate = ParseDate(inputDate);
            return (int)parsedDate.DayOfWeek;
        }

        public string GetDayName(string inputDate)
        {
            DateTime parsedDate = ParseDate(inputDate);
            return parsedDate.ToString("dddd");
        }

        public bool IsLeapYear(string inputDate)
        {
            DateTime parsedDate = ParseDate(inputDate);
            return DateTime.IsLeapYear(parsedDate.Year);
        }
    }

    /// <summary>
    /// DateFormatterTesting: NUnit test cases for DateFormatter class
    /// Tests date formatting, validation, and date-related operations
    /// </summary>
    [TestFixture]
    public class DateFormatterTesting
    {
        private DateFormatter formatter;

        [SetUp]
        public void SetUp()
        {
            formatter = new DateFormatter();
        }

        #region Format Date Tests

        [Test]
        public void FormatDate_ValidDate_ReturnsCorrectFormat()
        {
            // Act
            string result = formatter.FormatDate("2024-01-15");

            // Assert
            Assert.AreEqual("15-01-2024", result);
        }

        [Test]
        public void FormatDate_JanuaryDate_ReturnsCorrectFormat()
        {
            // Act
            string result = formatter.FormatDate("2023-01-01");

            // Assert
            Assert.AreEqual("01-01-2023", result);
        }

        [Test]
        public void FormatDate_DecemberDate_ReturnsCorrectFormat()
        {
            // Act
            string result = formatter.FormatDate("2023-12-31");

            // Assert
            Assert.AreEqual("31-12-2023", result);
        }

        [Test]
        public void FormatDate_LeapYearDate_ReturnsCorrectFormat()
        {
            // Act
            string result = formatter.FormatDate("2024-02-29");

            // Assert
            Assert.AreEqual("29-02-2024", result);
        }

        [TestCase("2023-01-15", "15-01-2023")]
        [TestCase("2023-06-30", "30-06-2023")]
        [TestCase("2023-12-25", "25-12-2023")]
        [TestCase("2024-02-14", "14-02-2024")]
        public void FormatDate_VariousDates_ReturnsCorrectFormat(string input, string expected)
        {
            // Act
            string result = formatter.FormatDate(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region Invalid Format Tests

        [Test]
        public void FormatDate_InvalidFormat_ThrowsFormatException()
        {
            // Act & Assert
            Assert.Throws<FormatException>(() => formatter.FormatDate("15-01-2023"));
        }

        [Test]
        public void FormatDate_WrongSeparator_ThrowsFormatException()
        {
            // Act & Assert
            Assert.Throws<FormatException>(() => formatter.FormatDate("2023/01/15"));
        }

        [Test]
        public void FormatDate_InvalidDate_ThrowsFormatException()
        {
            // Act & Assert
            Assert.Throws<FormatException>(() => formatter.FormatDate("2023-13-01"));
        }

        [Test]
        public void FormatDate_InvalidDay_ThrowsFormatException()
        {
            // Act & Assert
            Assert.Throws<FormatException>(() => formatter.FormatDate("2023-02-30"));
        }

        [TestCase("")]
        [TestCase(null)]
        public void FormatDate_NullOrEmpty_ThrowsArgumentException(string input)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => formatter.FormatDate(input));
        }

        [TestCase("2023-13-15")]
        [TestCase("2023-01-32")]
        [TestCase("2023-02-30")]
        public void FormatDate_InvalidDates_ThrowsFormatException(string input)
        {
            // Act & Assert
            Assert.Throws<FormatException>(() => formatter.FormatDate(input));
        }

        #endregion

        #region Date Validation Tests

        [Test]
        public void IsValidDate_ValidDate_ReturnsTrue()
        {
            // Act
            bool result = formatter.IsValidDate("2023-01-15");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidDate_InvalidDate_ReturnsFalse()
        {
            // Act
            bool result = formatter.IsValidDate("2023-02-30");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidDate_InvalidMonth_ReturnsFalse()
        {
            // Act
            bool result = formatter.IsValidDate("2023-13-15");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidDate_InvalidFormat_ReturnsFalse()
        {
            // Act
            bool result = formatter.IsValidDate("15-01-2023");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidDate_NullString_ReturnsFalse()
        {
            // Act
            bool result = formatter.IsValidDate(null);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidDate_EmptyString_ReturnsFalse()
        {
            // Act
            bool result = formatter.IsValidDate("");

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region Parse Date Tests

        [Test]
        public void ParseDate_ValidDate_ReturnsDateTime()
        {
            // Act
            DateTime result = formatter.ParseDate("2023-01-15");

            // Assert
            Assert.AreEqual(2023, result.Year);
            Assert.AreEqual(1, result.Month);
            Assert.AreEqual(15, result.Day);
        }

        [Test]
        public void ParseDate_InvalidFormat_ThrowsFormatException()
        {
            // Act & Assert
            Assert.Throws<FormatException>(() => formatter.ParseDate("15-01-2023"));
        }

        [Test]
        public void ParseDate_NullString_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => formatter.ParseDate(null));
        }

        #endregion

        #region Leap Year Tests

        [Test]
        public void IsLeapYear_LeapYear_ReturnsTrue()
        {
            // Act
            bool result = formatter.IsLeapYear("2024-01-01");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsLeapYear_NonLeapYear_ReturnsFalse()
        {
            // Act
            bool result = formatter.IsLeapYear("2023-01-01");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsLeapYear_DivisibleBy100NotBy400_ReturnsFalse()
        {
            // Act
            bool result = formatter.IsLeapYear("1900-01-01");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsLeapYear_DivisibleBy400_ReturnsTrue()
        {
            // Act
            bool result = formatter.IsLeapYear("2000-01-01");

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase("2020-01-01", true)]
        [TestCase("2021-01-01", false)]
        [TestCase("2022-01-01", false)]
        [TestCase("2023-01-01", false)]
        [TestCase("2024-01-01", true)]
        public void IsLeapYear_VariousYears_CorrectValidation(string date, bool expected)
        {
            // Act
            bool result = formatter.IsLeapYear(date);

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region Day of Week Tests

        [Test]
        public void GetDayName_Monday_ReturnsMonday()
        {
            // Act - 2024-01-01 is a Monday
            string result = formatter.GetDayName("2024-01-01");

            // Assert
            Assert.AreEqual("Monday", result);
        }

        [Test]
        public void GetDayName_Friday_ReturnsFriday()
        {
            // Act - 2024-01-05 is a Friday
            string result = formatter.GetDayName("2024-01-05");

            // Assert
            Assert.AreEqual("Friday", result);
        }

        [Test]
        public void GetDayOfWeek_ValidDate_ReturnsValidDayNumber()
        {
            // Act
            int dayOfWeek = formatter.GetDayOfWeek("2024-01-01");

            // Assert
            Assert.GreaterOrEqual(dayOfWeek, 0);
            Assert.LessOrEqual(dayOfWeek, 6);
        }

        #endregion

        #region Edge Cases

        [Test]
        public void FormatDate_EndOfYear_ReturnsCorrectFormat()
        {
            // Act
            string result = formatter.FormatDate("2023-12-31");

            // Assert
            Assert.AreEqual("31-12-2023", result);
        }

        [Test]
        public void FormatDate_StartOfYear_ReturnsCorrectFormat()
        {
            // Act
            string result = formatter.FormatDate("2023-01-01");

            // Assert
            Assert.AreEqual("01-01-2023", result);
        }

        [Test]
        public void FormatDate_FirstDayOfMonth_ReturnsCorrectFormat()
        {
            // Act
            string result = formatter.FormatDate("2023-06-01");

            // Assert
            Assert.AreEqual("01-06-2023", result);
        }

        [Test]
        public void FormatDate_LastDayOfMonth_ReturnsCorrectFormat()
        {
            // Act
            string result = formatter.FormatDate("2023-06-30");

            // Assert
            Assert.AreEqual("30-06-2023", result);
        }

        #endregion

        #region Integration Tests

        [Test]
        public void Integration_ParseAndFormat_ConsistentResults()
        {
            // Arrange
            string inputDate = "2023-06-15";

            // Act
            string formatted = formatter.FormatDate(inputDate);
            DateTime parsed = formatter.ParseDate(inputDate);

            // Assert
            Assert.AreEqual("15-06-2023", formatted);
            Assert.AreEqual(15, parsed.Day);
            Assert.AreEqual(6, parsed.Month);
            Assert.AreEqual(2023, parsed.Year);
        }

        [Test]
        public void Integration_ValidateAndFormat_BothSucceed()
        {
            // Arrange
            string date = "2024-02-29";

            // Act
            bool isValid = formatter.IsValidDate(date);
            string formatted = formatter.FormatDate(date);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual("29-02-2024", formatted);
        }

        #endregion

        [TearDown]
        public void TearDown()
        {
            formatter = null;
        }
    }
}
