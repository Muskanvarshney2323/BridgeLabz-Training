namespace FundooNotesApp.ModelLayer.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
            : base("The email or password provided is incorrect.")
        {
        }
    }
}