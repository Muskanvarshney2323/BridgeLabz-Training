using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace CollectionNUnit.Advanced
{
    /// <summary>
    /// PasswordValidator Class: Validates password strength
    /// Requirements: Minimum 8 characters, at least 1 uppercase, at least 1 digit
    /// </summary>
    public class PasswordValidator
    {
        private const int MinLength = 8;

        public bool IsValid(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            // Check minimum length
            if (password.Length < MinLength)
            {
                return false;
            }

            // Check for at least one uppercase letter
            bool hasUpperCase = Regex.IsMatch(password, @"[A-Z]");
            if (!hasUpperCase)
            {
                return false;
            }

            // Check for at least one digit
            bool hasDigit = Regex.IsMatch(password, @"[0-9]");
            if (!hasDigit)
            {
                return false;
            }

            return true;
        }

        public string GetValidationMessage(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "Password cannot be empty";
            }

            if (password.Length < MinLength)
            {
                return $"Password must be at least {MinLength} characters long";
            }

            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                return "Password must contain at least one uppercase letter";
            }

            if (!Regex.IsMatch(password, @"[0-9]"))
            {
                return "Password must contain at least one digit";
            }

            return "Password is valid";
        }

        public int GetPasswordStrength(string password)
        {
            int strength = 0;

            if (string.IsNullOrEmpty(password))
            {
                return 0;
            }

            // Length check
            if (password.Length >= MinLength) strength += 1;
            if (password.Length >= 12) strength += 1;
            if (password.Length >= 16) strength += 1;

            // Uppercase check
            if (Regex.IsMatch(password, @"[A-Z]")) strength += 1;

            // Lowercase check
            if (Regex.IsMatch(password, @"[a-z]")) strength += 1;

            // Digit check
            if (Regex.IsMatch(password, @"[0-9]")) strength += 1;

            // Special character check
            if (Regex.IsMatch(password, @"[!@#$%^&*()_\-+=\[\]{};:'""<>,.?/\\|`~]")) strength += 1;

            return strength;
        }
    }

    /// <summary>
    /// PasswordValidatorTesting: NUnit test cases for PasswordValidator class
    /// Tests valid/invalid passwords and password strength validation
    /// </summary>
    [TestFixture]
    public class PasswordValidatorTesting
    {
        private PasswordValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new PasswordValidator();
        }

        #region Valid Password Tests

        [Test]
        public void IsValid_ValidPassword_ReturnsTrue()
        {
            // Act
            bool result = validator.IsValid("Password1");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid_ValidPasswordWithNumbers_ReturnsTrue()
        {
            // Act
            bool result = validator.IsValid("MyPass123");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid_ValidLongPassword_ReturnsTrue()
        {
            // Act
            bool result = validator.IsValid("MyVeryLongPassword1234");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid_ValidPasswordWithSpecialCharacters_ReturnsTrue()
        {
            // Act
            bool result = validator.IsValid("Pass@word1");

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase("MyPassword1")]
        [TestCase("SecurePass99")]
        [TestCase("StrongP@ss1")]
        [TestCase("Admin12345")]
        public void IsValid_VariousValidPasswords_ReturnsTrue(string password)
        {
            // Act
            bool result = validator.IsValid(password);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region Invalid Password - Length Tests

        [Test]
        public void IsValid_TooShort_ReturnsFalse()
        {
            // Act
            bool result = validator.IsValid("Pass1");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_ExactlySevenCharacters_ReturnsFalse()
        {
            // Act
            bool result = validator.IsValid("Pass12");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_ExactlyEightCharactersWithAllRequirements_ReturnsTrue()
        {
            // Act
            bool result = validator.IsValid("Pass1234");

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase("Pass1")]
        [TestCase("Pwd1")]
        [TestCase("A1")]
        [TestCase("P1")]
        public void IsValid_TooShortPasswords_ReturnsFalse(string password)
        {
            // Act
            bool result = validator.IsValid(password);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region Invalid Password - Uppercase Tests

        [Test]
        public void IsValid_NoUpperCase_ReturnsFalse()
        {
            // Act
            bool result = validator.IsValid("password1");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_LowercaseAndNumbers_ReturnsFalse()
        {
            // Act
            bool result = validator.IsValid("passwrd1");

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase("password1234")]
        [TestCase("mypassword99")]
        [TestCase("testpass12")]
        public void IsValid_NoUpperCasePasswords_ReturnsFalse(string password)
        {
            // Act
            bool result = validator.IsValid(password);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region Invalid Password - Digit Tests

        [Test]
        public void IsValid_NoDigits_ReturnsFalse()
        {
            // Act
            bool result = validator.IsValid("Password");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_UppercaseButNoDigits_ReturnsFalse()
        {
            // Act
            bool result = validator.IsValid("MyPassword");

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase("MyPassword")]
        [TestCase("SecurePass")]
        [TestCase("AdminPanel")]
        public void IsValid_NoDigitsPasswords_ReturnsFalse(string password)
        {
            // Act
            bool result = validator.IsValid(password);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region Invalid Password - Empty/Null Tests

        [Test]
        public void IsValid_NullPassword_ReturnsFalse()
        {
            // Act
            bool result = validator.IsValid(null);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_EmptyPassword_ReturnsFalse()
        {
            // Act
            bool result = validator.IsValid("");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_WhitespacePassword_ReturnsFalse()
        {
            // Act
            bool result = validator.IsValid("        ");

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region Password Strength Tests

        [Test]
        public void GetPasswordStrength_ValidPassword_ReturnPositiveValue()
        {
            // Act
            int strength = validator.GetPasswordStrength("Password1");

            // Assert
            Assert.Greater(strength, 0);
        }

        [Test]
        public void GetPasswordStrength_WeakPassword_ReturnLowValue()
        {
            // Act
            int strength = validator.GetPasswordStrength("Pass1");

            // Assert
            Assert.Less(strength, 3);
        }

        [Test]
        public void GetPasswordStrength_StrongPassword_ReturnHighValue()
        {
            // Act
            int strength = validator.GetPasswordStrength("MyStr0ng!P@ssw0rd");

            // Assert
            Assert.Greater(strength, 4);
        }

        [Test]
        public void GetPasswordStrength_EmptyPassword_ReturnZero()
        {
            // Act
            int strength = validator.GetPasswordStrength("");

            // Assert
            Assert.AreEqual(0, strength);
        }

        #endregion

        #region Validation Message Tests

        [Test]
        public void GetValidationMessage_ValidPassword_ReturnsValidMessage()
        {
            // Act
            string message = validator.GetValidationMessage("Password1");

            // Assert
            Assert.AreEqual("Password is valid", message);
        }

        [Test]
        public void GetValidationMessage_EmptyPassword_ReturnsEmptyMessage()
        {
            // Act
            string message = validator.GetValidationMessage("");

            // Assert
            Assert.That(message, Does.Contain("empty"));
        }

        [Test]
        public void GetValidationMessage_TooShort_ReturnsMissingLengthMessage()
        {
            // Act
            string message = validator.GetValidationMessage("Pass1");

            // Assert
            Assert.That(message, Does.Contain("characters"));
        }

        [Test]
        public void GetValidationMessage_NoUpperCase_ReturnsMissingUppercaseMessage()
        {
            // Act
            string message = validator.GetValidationMessage("password1");

            // Assert
            Assert.That(message, Does.Contain("uppercase"));
        }

        [Test]
        public void GetValidationMessage_NoDigit_ReturnsMissingDigitMessage()
        {
            // Act
            string message = validator.GetValidationMessage("Password");

            // Assert
            Assert.That(message, Does.Contain("digit"));
        }

        #endregion

        #region Edge Cases

        [Test]
        public void IsValid_PasswordWithSpaces_ReturnsTrue()
        {
            // Act
            bool result = validator.IsValid("Pass Word1");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid_VeryLongPassword_ReturnsTrue()
        {
            // Act
            bool result = validator.IsValid("MyVeryLongAndComplexPassword123456789");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid_PasswordWithSpecialCharacters_ReturnsTrue()
        {
            // Act
            bool result = validator.IsValid("P@ssw0rd!");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid_MultipleUppercase_ReturnsTrue()
        {
            // Act
            bool result = validator.IsValid("PASSword1");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid_MultipleDigits_ReturnsTrue()
        {
            // Act
            bool result = validator.IsValid("Password123456");

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        [TearDown]
        public void TearDown()
        {
            validator = null;
        }
    }
}
