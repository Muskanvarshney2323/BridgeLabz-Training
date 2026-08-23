using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FundooNotesApp.BusinessLayer.Helper;
using FundooNotesApp.BusinessLayer.Service;
using FundooNotesApp.ModelLayer.Dtos.Request;
using FundooNotesApp.RepositoryLayer.Context;
using FundooNotesApp.RepositoryLayer.Service;
using Moq;

namespace FundooNotesApp.Tests
{
    [TestClass]
    public class UserTests
    {
        private AppDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        private AuthService GetAuthService(AppDbContext context)
        {
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(value => value["Jwt:Key"])
                .Returns("TestSecretKeyForUnitTesting123456!");

            return new AuthService(
                new UserRepository(context),
                new PasswordHelper(),
                new JwtTokenHelper(),
                configuration.Object);
        }

        private RegisterRequestDto GetRegisterRequest(string email = "user@example.com")
        {
            return new RegisterRequestDto
            {
                FirstName = "Test",
                LastName = "User",
                Email = email,
                Password = "password123"
            };
        }

        [TestMethod]
        public void Register_ShouldSucceed_WhenEmailIsNew()
        {
            var result = GetAuthService(GetInMemoryContext()).Register(GetRegisterRequest());

            Assert.AreEqual("User registered successfully", result);
        }

        [TestMethod]
        public void Register_ShouldReturnDuplicateMessage_WhenEmailExists()
        {
            var userService = GetAuthService(GetInMemoryContext());
            var request = GetRegisterRequest();
            userService.Register(request);

            var result = userService.Register(request);

            Assert.AreEqual("Email already registered", result);
        }

        [TestMethod]
        public void Login_ShouldReturnToken_WhenCredentialsAreCorrect()
        {
            var userService = GetAuthService(GetInMemoryContext());
            userService.Register(GetRegisterRequest("login@example.com"));

            var token = userService.Login(new LoginRequestDto
            {
                Email = "login@example.com",
                Password = "password123"
            });

            Assert.IsFalse(string.IsNullOrEmpty(token));
        }

        [TestMethod]
        public void Login_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            var userService = GetAuthService(GetInMemoryContext());
            var token = userService.Login(new LoginRequestDto
            {
                Email = "missing@example.com",
                Password = "password123"
            });

            Assert.IsNull(token);
        }

        [TestMethod]
        public void ForgotPassword_ShouldReturnExpectedMessage()
        {
            var userService = GetAuthService(GetInMemoryContext());
            userService.Register(GetRegisterRequest("forgot@example.com"));

            var result = userService.ForgotPassword(new ForgotPasswordRequestDto
            {
                Email = "forgot@example.com"
            });

            Assert.AreEqual("Password reset request received", result);
        }

        [TestMethod]
        public void ResetPassword_ShouldReturnExpectedMessage()
        {
            var result = GetAuthService(GetInMemoryContext()).ResetPassword(
                new ResetPasswordRequestDto
                {
                    Token = "reset-token",
                    NewPassword = "newPassword456"
                });

            Assert.AreEqual("Password reset successfully", result);
        }
    }
}
