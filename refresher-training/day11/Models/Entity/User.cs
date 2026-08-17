namespace Models.Entity
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; }

        public string Password { get; set; }
    }
}