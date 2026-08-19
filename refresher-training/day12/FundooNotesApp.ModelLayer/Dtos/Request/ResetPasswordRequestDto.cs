using System.ComponentModel.DataAnnotations;

namespace FundooNotesApp.ModelLayer.Dtos.Request
{
    public class ResetPasswordRequestDto
    {
        [Required(ErrorMessage = "Reset token is required.")]
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "New password cannot be empty.")]
        [MinLength(6, ErrorMessage = "New password must contain at least 6 characters.")]
        public string NewPassword { get; set; } = string.Empty;
    }
}