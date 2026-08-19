using System.ComponentModel.DataAnnotations;

namespace FundooNotesApp.ModelLayer.Dtos.Request
{
    public class LoginRequestDto
    {
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}