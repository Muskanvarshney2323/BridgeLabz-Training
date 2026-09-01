using System.ComponentModel.DataAnnotations;

namespace FundooNotesApp.ModelLayer.Dtos.Request
{
    public class UpdateReminderRequestDto
    {
        [Required]
        public DateTime ReminderTime { get; set; }
    }
}