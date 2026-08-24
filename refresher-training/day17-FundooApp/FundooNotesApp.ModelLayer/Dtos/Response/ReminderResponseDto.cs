namespace FundooNotesApp.ModelLayer.Dtos.Response
{
    public class ReminderResponseDto
    {
        public int ReminderId { get; set; }

        public int NoteId { get; set; }

        public DateTime ReminderTime { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsNotificationSent { get; set; }
    }
}