namespace FundooNotesApp.ModelLayer.Exceptions
{
    public class NoteNotFoundException : Exception
    {
        public NoteNotFoundException(string message)
            : base(message)
        {
        }
    }
}
