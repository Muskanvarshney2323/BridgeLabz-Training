using System.ComponentModel.DataAnnotations;

namespace FundooNotesApp.ModelLayer.Entities
{
    public class LabelEntity
    {
        [Key]
        public int LabelId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public int UserId { get; set; }

        public ICollection<NoteEntity> Notes { get; set; } = new List<NoteEntity>();
    }
}