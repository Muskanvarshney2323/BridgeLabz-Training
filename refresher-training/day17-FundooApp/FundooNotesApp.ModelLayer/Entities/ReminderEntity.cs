using System.ComponentModel.DataAnnotations;

namespace FundooNotesApp.ModelLayer.Entities
{
    public class ReminderEntity
    {
        [Key]
        public int ReminderId { get; set; }

        [Required]
        public int NoteId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime ReminderTime { get; set; }

        public bool IsCompleted { get; set; } = false;

        public bool IsNotificationSent { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}