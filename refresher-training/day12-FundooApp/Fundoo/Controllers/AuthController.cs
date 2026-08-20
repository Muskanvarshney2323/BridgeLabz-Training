using Business.Interface;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace Fundoo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var result = _service.Register(dto);

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var token = _service.Login(dto);

            if (token == null)
            {
                return Unauthorized(
                    "Invalid email or password"
                );
            }

            return Ok(new
            {
                message = "Login successful",
                token = token
            });
        }
    }
}