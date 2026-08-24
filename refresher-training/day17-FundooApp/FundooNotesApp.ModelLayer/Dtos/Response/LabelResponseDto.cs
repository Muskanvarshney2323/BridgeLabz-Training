namespace FundooNotesApp.ModelLayer.Dtos.Response
{
    public class LabelResponseDto
    {
        public int LabelId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}