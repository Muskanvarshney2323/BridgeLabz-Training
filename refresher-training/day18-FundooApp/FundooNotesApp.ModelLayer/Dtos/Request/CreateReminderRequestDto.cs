using System.ComponentModel.DataAnnotations;

namespace FundooNotesApp.ModelLayer.Dtos.Request
{
    public class CreateReminderRequestDto
    {
        [Required]
        public int NoteId { get; set; }

        [Required]
        public DateTime ReminderTime { get; set; }
    }
}