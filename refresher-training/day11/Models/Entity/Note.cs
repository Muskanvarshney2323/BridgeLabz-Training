namespace Models.Entity
{
    public class Note
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
    }
}