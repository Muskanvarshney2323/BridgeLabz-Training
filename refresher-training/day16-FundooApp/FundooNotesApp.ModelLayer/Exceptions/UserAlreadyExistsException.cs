namespace FundooNotesApp.ModelLayer.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException()
            : base("A user with this email address already exists.")
        {
        }
    }
}