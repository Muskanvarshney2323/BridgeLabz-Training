namespace FundooNotesApp.ModelLayer.Dtos.Response
{
    public class NoteResponseDto
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public bool IsPinned { get; set; }
        public bool IsArchived { get; set; }
        public bool IsTrashed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}