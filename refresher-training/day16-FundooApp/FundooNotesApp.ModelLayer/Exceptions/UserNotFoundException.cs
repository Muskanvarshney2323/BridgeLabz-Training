namespace FundooNotesApp.ModelLayer.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base("The requested user could not be found.")
        {
        }
    }
}