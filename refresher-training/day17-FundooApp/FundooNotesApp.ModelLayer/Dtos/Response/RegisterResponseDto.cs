namespace FundooNotesApp.ModelLayer.Dtos.Response
{
    public class RegisterResponseDto
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}