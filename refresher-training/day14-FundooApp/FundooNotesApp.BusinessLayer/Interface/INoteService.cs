using FundooNotesApp.ModelLayer.Dtos.Request;
using FundooNotesApp.ModelLayer.Dtos.Response;

namespace FundooNotesApp.BusinessLayer.Interface
{
    public interface INoteService
    {
        Task<NoteResponseDto> CreateNoteAsync(
            CreateNoteRequestDto request,
            int userId);

        Task<List<NoteResponseDto>> GetAllNotesAsync(int userId);

        Task<bool> DeleteNoteAsync(
            int noteId,
            int userId);
    }
}