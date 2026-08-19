using System.ComponentModel.DataAnnotations;

namespace FundooNotesApp.ModelLayer.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter your first name.")]
        [StringLength(20, ErrorMessage = "First name cannot contain more than 20 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Last name cannot contain more than 20 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is mandatory.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;
    }
}