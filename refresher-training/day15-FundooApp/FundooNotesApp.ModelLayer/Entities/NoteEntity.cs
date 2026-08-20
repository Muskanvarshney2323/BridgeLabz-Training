using System.ComponentModel.DataAnnotations;

namespace FundooNotesApp.ModelLayer.Entities
{
    public class NoteEntity
    {
        [Key]
        public int NoteId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int UserId { get; set; }

        public bool IsPinned { get; set; } = false;

        public bool IsArchived { get; set; } = false;

        public bool IsTrashed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}