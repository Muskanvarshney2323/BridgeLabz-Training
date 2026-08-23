using FundooNotesApp.BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using FundooNotesApp.ModelLayer.Dtos.Request;

namespace FundooNotesApp.API.Controllers
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
        public IActionResult Register(RegisterRequestDto dto)
        {
            try
            {
                string message = _service.Register(dto);

                return Ok(new
                {
                    success = true,
                    message = message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto dto)
        {
            try
            {
                var token = _service.Login(dto);

                if (token == null)
                {
                    return Unauthorized(new
                    {
                        success = false,
                        message = "Invalid email or password"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Login successful",
                    token = token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}