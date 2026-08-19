using System.ComponentModel.DataAnnotations;

namespace FundooNotesApp.ModelLayer.Dtos.Request
{
    public class ForgotPasswordRequestDto
    {
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
        public string Email { get; set; } = string.Empty;
    }
}