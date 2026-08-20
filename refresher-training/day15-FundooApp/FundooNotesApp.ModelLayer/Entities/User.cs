namespace FundooNotesApp.ModelLayer.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        // Used for forgot-password functionality
        public string? ResetPasswordToken { get; set; }

        public DateTime? ResetTokenExpiry { get; set; }
    }
}