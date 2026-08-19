using FundooNotesApp.BusinessLayer.Helper;
using FundooNotesApp.BusinessLayer.Interface;
using FundooNotesApp.ModelLayer.Dtos.Request;
using FundooNotesApp.ModelLayer.Entities;
using FundooNotesApp.RepositoryLayer.Interface;

namespace FundooNotesApp.BusinessLayer.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly PasswordHelper _passwordHelper;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AuthService(
            IUserRepository repository,
            PasswordHelper passwordHelper,
            JwtTokenHelper jwtTokenHelper)
        {
            _repository = repository;
            _passwordHelper = passwordHelper;
            _jwtTokenHelper = jwtTokenHelper;
        }

        public string Register(RegisterRequestDto registerDto)
        {
            var existingUser = _repository.GetUserByEmail(
                registerDto.Email
            );

            if (existingUser != null)
            {
                return "Email already registered";
            }

            string hashedPassword = _passwordHelper.HashPassword(
                registerDto.Password
            );

            User user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Password = hashedPassword
            };

            _repository.AddUser(user);

            return "User registered successfully";
        }

        public string? Login(LoginRequestDto loginDto)
        {
            var user = _repository.GetUserByEmail(
                loginDto.Email
            );

            if (user == null)
            {
                return null;
            }

            bool passwordValid = _passwordHelper.VerifyPassword(
                loginDto.Password,
                user.Password
            );

            if (!passwordValid)
            {
                return null;
            }

            return _jwtTokenHelper.CreateToken(
                user.UserId,
                user.FirstName,
                user.Email,
                "day12-development-signing-key"
            );
        }

        public string ForgotPassword(
            ForgotPasswordRequestDto forgotPasswordDto)
        {
            var user = _repository.GetUserByEmail(
                forgotPasswordDto.Email
            );

            if (user == null)
            {
                return "User not found";
            }

            return "Password reset request received";
        }

        public string ResetPassword(
            ResetPasswordRequestDto resetPasswordDto)
        {
            return "Password reset successfully";
        }
    }
}