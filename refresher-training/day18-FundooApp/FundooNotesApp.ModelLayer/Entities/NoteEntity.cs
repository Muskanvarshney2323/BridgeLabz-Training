using System.ComponentModel.DataAnnotations;

namespace FundooNotesApp.ModelLayer.Entities
{
    public class NoteEntity
    {
        [Key]
        public int NoteId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public int UserId { get; set; }

        public bool IsPinned { get; set; } = false;

        public bool IsArchived { get; set; } = false;

        public bool IsTrashed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public ICollection<LabelEntity> Labels { get; set; } = new List<LabelEntity>();
    }
}