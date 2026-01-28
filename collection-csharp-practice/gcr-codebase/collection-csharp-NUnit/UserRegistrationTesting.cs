using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace CollectionNUnit.Advanced
{
    /// <summary>
    /// UserRegistration Class: Validates and registers users
    /// Validates username, email, and password before registration
    /// </summary>
    public class UserRegistration
    {
        private const int MinPasswordLength = 8;
        private const int MinUsernameLength = 3;
        private const int MaxUsernameLength = 20;

        public void RegisterUser(string username, string email, string password)
        {
            ValidateUsername(username);
            ValidateEmail(email);
            ValidatePassword(password);

            // If all validations pass, user is registered
            // In a real application, this would save to a database
        }

        private void ValidateUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be null or empty");
            }

            if (username.Length < MinUsernameLength)
            {
                throw new ArgumentException($"Username must be at least {MinUsernameLength} characters long");
            }

            if (username.Length > MaxUsernameLength)
            {
                throw new ArgumentException($"Username cannot exceed {MaxUsernameLength} characters");
            }

            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
            {
                throw new ArgumentException("Username can only contain letters, numbers, and underscores");
            }
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty");
            }

            // Simple email validation
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                throw new ArgumentException("Invalid email format");
            }
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty");
            }

            if (password.Length < MinPasswordLength)
            {
                throw new ArgumentException($"Password must be at least {MinPasswordLength} characters long");
            }

            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                throw new ArgumentException("Password must contain at least one uppercase letter");
            }

            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                throw new ArgumentException("Password must contain at least one lowercase letter");
            }

            if (!Regex.IsMatch(password, @"[0-9]"))
            {
                throw new ArgumentException("Password must contain at least one digit");
            }
        }

        public bool IsValidUsername(string username)
        {
            try
            {
                ValidateUsername(username);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                ValidateEmail(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidPassword(string password)
        {
            try
            {
                ValidatePassword(password);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// UserRegistrationTesting: NUnit test cases for UserRegistration class
    /// Tests user registration validation for valid and invalid inputs
    /// </summary>
    [TestFixture]
    public class UserRegistrationTesting
    {
        private UserRegistration registration;

        [SetUp]
        public void SetUp()
        {
            registration = new UserRegistration();
        }

        #region Valid Registration Tests

        [Test]
        public void RegisterUser_ValidInputs_Succeeds()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => registration.RegisterUser("john_doe", "john@example.com", "Password123"));
        }

        [Test]
        public void RegisterUser_ValidInputsMinimumLength_Succeeds()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => registration.RegisterUser("abc", "a@b.co", "Pass1234"));
        }

        [TestCase("john_doe", "john@example.com", "Password123")]
        [TestCase("user123", "user@gmail.com", "SecurePass1")]
        [TestCase("admin_user", "admin@company.com", "Admin@1234")]
        public void RegisterUser_VariousValidInputs_Succeeds(string username, string email, string password)
        {
            // Act & Assert
            Assert.DoesNotThrow(() => registration.RegisterUser(username, email, password));
        }

        #endregion

        #region Username Validation Tests

        [Test]
        public void RegisterUser_NullUsername_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser(null, "user@example.com", "Password123"));
        }

        [Test]
        public void RegisterUser_EmptyUsername_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("", "user@example.com", "Password123"));
        }

        [Test]
        public void RegisterUser_UsernametooShort_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("ab", "user@example.com", "Password123"));
        }

        [Test]
        public void RegisterUser_UsernameTooLong_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("thisusernameistoolongfortherule", "user@example.com", "Password123"));
        }

        [Test]
        public void RegisterUser_UsernameWithSpecialCharacters_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("john@doe", "user@example.com", "Password123"));
        }

        [Test]
        public void RegisterUser_UsernameWithSpaces_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("john doe", "user@example.com", "Password123"));
        }

        [TestCase("user-name")]
        [TestCase("user@name")]
        [TestCase("user name")]
        [TestCase("user!")]
        public void RegisterUser_InvalidUsernameCharacters_ThrowsException(string username)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser(username, "user@example.com", "Password123"));
        }

        #endregion

        #region Email Validation Tests

        [Test]
        public void RegisterUser_NullEmail_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("username", null, "Password123"));
        }

        [Test]
        public void RegisterUser_EmptyEmail_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("username", "", "Password123"));
        }

        [Test]
        public void RegisterUser_InvalidEmailMissingAtSymbol_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("username", "userexample.com", "Password123"));
        }

        [Test]
        public void RegisterUser_InvalidEmailMissingDomain_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("username", "user@.com", "Password123"));
        }

        [Test]
        public void RegisterUser_InvalidEmailMissingExtension_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("username", "user@example", "Password123"));
        }

        [Test]
        public void RegisterUser_ValidEmailFormats_Succeeds()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => registration.RegisterUser("user1", "user.name+tag@example.co.uk", "Password123"));
        }

        [TestCase("invalid.email@")]
        [TestCase("@example.com")]
        [TestCase("user@example..com")]
        public void RegisterUser_InvalidEmailFormats_ThrowsException(string email)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("username", email, "Password123"));
        }

        #endregion

        #region Password Validation Tests

        [Test]
        public void RegisterUser_NullPassword_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("username", "user@example.com", null));
        }

        [Test]
        public void RegisterUser_EmptyPassword_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("username", "user@example.com", ""));
        }

        [Test]
        public void RegisterUser_PasswordTooShort_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("username", "user@example.com", "Pass1"));
        }

        [Test]
        public void RegisterUser_PasswordNoUppercase_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("username", "user@example.com", "password123"));
        }

        [Test]
        public void RegisterUser_PasswordNoLowercase_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("username", "user@example.com", "PASSWORD123"));
        }

        [Test]
        public void RegisterUser_PasswordNoDigit_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("username", "user@example.com", "Password"));
        }

        [TestCase("password123")]
        [TestCase("PASSWORD123")]
        [TestCase("Password")]
        [TestCase("Pass")]
        public void RegisterUser_InsufficientPasswordRequirements_ThrowsException(string password)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => registration.RegisterUser("username", "user@example.com", password));
        }

        #endregion

        #region Individual Validation Tests

        [Test]
        public void IsValidUsername_ValidUsername_ReturnsTrue()
        {
            // Act
            bool result = registration.IsValidUsername("valid_user");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidUsername_InvalidUsername_ReturnsFalse()
        {
            // Act
            bool result = registration.IsValidUsername("ab");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidEmail_ValidEmail_ReturnsTrue()
        {
            // Act
            bool result = registration.IsValidEmail("user@example.com");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidEmail_InvalidEmail_ReturnsFalse()
        {
            // Act
            bool result = registration.IsValidEmail("invalid.email");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidPassword_ValidPassword_ReturnsTrue()
        {
            // Act
            bool result = registration.IsValidPassword("Password123");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidPassword_InvalidPassword_ReturnsFalse()
        {
            // Act
            bool result = registration.IsValidPassword("password");

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region Error Message Tests

        [Test]
        public void RegisterUser_NullUsername_HasDescriptiveError()
        {
            // Act
            var ex = Assert.Throws<ArgumentException>(() => 
                registration.RegisterUser(null, "user@example.com", "Password123"));

            // Assert
            Assert.That(ex.Message, Does.Contain("Username"));
        }

        [Test]
        public void RegisterUser_InvalidEmail_HasDescriptiveError()
        {
            // Act
            var ex = Assert.Throws<ArgumentException>(() => 
                registration.RegisterUser("username", "invalidemail", "Password123"));

            // Assert
            Assert.That(ex.Message, Does.Contain("email"));
        }

        [Test]
        public void RegisterUser_WeakPassword_HasDescriptiveError()
        {
            // Act
            var ex = Assert.Throws<ArgumentException>(() => 
                registration.RegisterUser("username", "user@example.com", "weakpass"));

            // Assert
            Assert.That(ex.Message, Does.Contain("Password"));
        }

        #endregion

        #region Edge Cases

        [Test]
        public void RegisterUser_MinimumValidValues_Succeeds()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => registration.RegisterUser("abc", "a@b.co", "Abc12345"));
        }

        [Test]
        public void RegisterUser_MaximumUsernameLength_Succeeds()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => registration.RegisterUser("12345678901234567890", "user@example.com", "Password123"));
        }

        [Test]
        public void RegisterUser_ComplexValidPassword_Succeeds()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => registration.RegisterUser("username", "user@example.com", "MyS3cur3P@ssw0rd"));
        }

        [Test]
        public void RegisterUser_NumbersInUsername_Succeeds()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => registration.RegisterUser("user123", "user@example.com", "Password123"));
        }

        [Test]
        public void RegisterUser_UnderscoresInUsername_Succeeds()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => registration.RegisterUser("user_name_123", "user@example.com", "Password123"));
        }

        #endregion

        [TearDown]
        public void TearDown()
        {
            registration = null;
        }
    }
}
