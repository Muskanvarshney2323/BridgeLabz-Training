using FundooNotesApp.ModelLayer.Dtos.Request;
using FundooNotesApp.ModelLayer.Dtos.Response;

namespace FundooNotesApp.BusinessLayer.Interface
{
    public interface INotesService
    {
        NoteResponseDto CreateNote(
            CreateNoteRequestDto notesRequestDto,
            int userId);

        List<NoteResponseDto> GetAllNotes(int userId);

        string DeleteNote(long noteId, int userId);

        string TogglePin(int noteId, int userId);

        string ToggleArchive(int noteId, int userId);

        string MoveToTrash(int noteId, int userId);

        string RestoreFromTrash(int noteId, int userId);

        List<NoteResponseDto> SearchNotes(
            int userId,
            string keyword);

        List<NoteResponseDto> FilterNotes(
            int userId,
            bool? isPinned,
            bool? isArchived,
            bool? isTrashed);
    }
}