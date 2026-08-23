using FundooNotesApp.ModelLayer.Dtos.Request;

namespace FundooNotesApp.BusinessLayer.Interface
{
    public interface IAuthService
    {
        string Register(RegisterRequestDto registerDto);

        string? Login(LoginRequestDto loginDto);

        string ForgotPassword(ForgotPasswordRequestDto forgotPasswordDto);

        string ResetPassword(ResetPasswordRequestDto resetPasswordDto);
    }
}