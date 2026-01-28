using NUnit.Framework;
using System;

namespace CollectionNUnit.Basic
{
    /// <summary>
    /// StringUtils Class: Implements string utility methods
    /// </summary>
    public class StringUtils
    {
        public string Reverse(string str)
        {
            if (str == null)
            {
                return null;
            }

            char[] chars = str.ToCharArray();
            System.Array.Reverse(chars);
            return new string(chars);
        }

        public bool IsPalindrome(string str)
        {
            if (str == null)
            {
                return false;
            }

            string cleaned = str.ToLower().Replace(" ", "");
            string reversed = Reverse(cleaned);
            return cleaned == reversed;
        }

        public string ToUpperCase(string str)
        {
            if (str == null)
            {
                return null;
            }

            return str.ToUpper();
        }

        public bool IsEmpty(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public int CountCharacters(string str)
        {
            if (str == null)
            {
                return 0;
            }

            return str.Length;
        }

        public string ReplaceAll(string str, string oldValue, string newValue)
        {
            if (str == null)
            {
                return null;
            }

            return str.Replace(oldValue, newValue);
        }
    }

    /// <summary>
    /// StringUtilityMethodsTesting: NUnit test cases for StringUtils class
    /// Tests string operations like reverse, palindrome, and uppercase conversion
    /// </summary>
    [TestFixture]
    public class StringUtilityMethodsTesting
    {
        private StringUtils stringUtils;

        [SetUp]
        public void SetUp()
        {
            stringUtils = new StringUtils();
        }

        #region Reverse Tests

        [Test]
        public void Reverse_SimpleString_ReturnsReversedString()
        {
            // Arrange & Act
            string result = stringUtils.Reverse("hello");

            // Assert
            Assert.AreEqual("olleh", result);
        }

        [Test]
        public void Reverse_SingleCharacter_ReturnsSameCharacter()
        {
            // Arrange & Act
            string result = stringUtils.Reverse("a");

            // Assert
            Assert.AreEqual("a", result);
        }

        [Test]
        public void Reverse_EmptyString_ReturnsEmptyString()
        {
            // Arrange & Act
            string result = stringUtils.Reverse("");

            // Assert
            Assert.AreEqual("", result);
        }

        [Test]
        public void Reverse_StringWithSpaces_ReturnsReversedString()
        {
            // Arrange & Act
            string result = stringUtils.Reverse("hello world");

            // Assert
            Assert.AreEqual("dlrow olleh", result);
        }

        [Test]
        public void Reverse_StringWithNumbers_ReturnsReversedString()
        {
            // Arrange & Act
            string result = stringUtils.Reverse("12345");

            // Assert
            Assert.AreEqual("54321", result);
        }

        [Test]
        public void Reverse_NullString_ReturnsNull()
        {
            // Arrange & Act
            string result = stringUtils.Reverse(null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Reverse_ReverseTwice_ReturnsOriginalString()
        {
            // Arrange
            string original = "testing";

            // Act
            string reversed1 = stringUtils.Reverse(original);
            string reversed2 = stringUtils.Reverse(reversed1);

            // Assert
            Assert.AreEqual(original, reversed2);
        }

        #endregion

        #region IsPalindrome Tests

        [Test]
        public void IsPalindrome_PalindromeString_ReturnsTrue()
        {
            // Arrange & Act
            bool result = stringUtils.IsPalindrome("racecar");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsPalindrome_NonPalindromeString_ReturnsFalse()
        {
            // Arrange & Act
            bool result = stringUtils.IsPalindrome("hello");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsPalindrome_SingleCharacter_ReturnsTrue()
        {
            // Arrange & Act
            bool result = stringUtils.IsPalindrome("a");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsPalindrome_TwoSameCharacters_ReturnsTrue()
        {
            // Arrange & Act
            bool result = stringUtils.IsPalindrome("aa");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsPalindrome_PalindromeWithSpaces_ReturnsTrue()
        {
            // Arrange & Act
            bool result = stringUtils.IsPalindrome("a man a plan a canal panama");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsPalindrome_CaseInsensitive_ReturnsTrue()
        {
            // Arrange & Act
            bool result = stringUtils.IsPalindrome("RaceCar");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsPalindrome_NullString_ReturnsFalse()
        {
            // Arrange & Act
            bool result = stringUtils.IsPalindrome(null);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsPalindrome_EmptyString_ReturnsTrue()
        {
            // Arrange & Act
            bool result = stringUtils.IsPalindrome("");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsPalindrome_NumberPalindrome_ReturnsTrue()
        {
            // Arrange & Act
            bool result = stringUtils.IsPalindrome("12321");

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region ToUpperCase Tests

        [Test]
        public void ToUpperCase_LowercaseString_ReturnsUppercaseString()
        {
            // Arrange & Act
            string result = stringUtils.ToUpperCase("hello");

            // Assert
            Assert.AreEqual("HELLO", result);
        }

        [Test]
        public void ToUpperCase_UppercaseString_ReturnsSameString()
        {
            // Arrange & Act
            string result = stringUtils.ToUpperCase("HELLO");

            // Assert
            Assert.AreEqual("HELLO", result);
        }

        [Test]
        public void ToUpperCase_MixedCaseString_ReturnsUppercaseString()
        {
            // Arrange & Act
            string result = stringUtils.ToUpperCase("HeLLo WoRLd");

            // Assert
            Assert.AreEqual("HELLO WORLD", result);
        }

        [Test]
        public void ToUpperCase_StringWithNumbers_ReturnsUppercaseString()
        {
            // Arrange & Act
            string result = stringUtils.ToUpperCase("hello123world");

            // Assert
            Assert.AreEqual("HELLO123WORLD", result);
        }

        [Test]
        public void ToUpperCase_NullString_ReturnsNull()
        {
            // Arrange & Act
            string result = stringUtils.ToUpperCase(null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ToUpperCase_EmptyString_ReturnsEmptyString()
        {
            // Arrange & Act
            string result = stringUtils.ToUpperCase("");

            // Assert
            Assert.AreEqual("", result);
        }

        #endregion

        #region Additional Helper Methods Tests

        [Test]
        public void IsEmpty_NullString_ReturnsTrue()
        {
            // Arrange & Act
            bool result = stringUtils.IsEmpty(null);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsEmpty_EmptyString_ReturnsTrue()
        {
            // Arrange & Act
            bool result = stringUtils.IsEmpty("");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsEmpty_NonEmptyString_ReturnsFalse()
        {
            // Arrange & Act
            bool result = stringUtils.IsEmpty("hello");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CountCharacters_RegularString_ReturnsCorrectCount()
        {
            // Arrange & Act
            int result = stringUtils.CountCharacters("hello");

            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void CountCharacters_NullString_ReturnsZero()
        {
            // Arrange & Act
            int result = stringUtils.CountCharacters(null);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void ReplaceAll_ValidReplacement_ReturnsReplacedString()
        {
            // Arrange & Act
            string result = stringUtils.ReplaceAll("hello world", "world", "universe");

            // Assert
            Assert.AreEqual("hello universe", result);
        }

        #endregion

        [TearDown]
        public void TearDown()
        {
            stringUtils = null;
        }
    }
}
