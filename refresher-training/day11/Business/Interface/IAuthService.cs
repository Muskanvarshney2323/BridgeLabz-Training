using Models.DTO;

namespace Business.Interface
{
    public interface IAuthService
    {
        string Register(RegisterDto dto);

        string Login(LoginDto dto);
    }
}