using FundooNotesApp.ModelLayer.Entities;

namespace FundooNotesApp.RepositoryLayer.Interface
{
    public interface INoteRepository
    {
        Task<NoteEntity> CreateNoteAsync(NoteEntity note);

        Task<List<NoteEntity>> GetAllNotesAsync(int userId);

        Task<NoteEntity?> GetNoteByIdAsync(int noteId);

        Task<bool> DeleteNoteAsync(int noteId);

        Task<bool> TogglePinAsync(int noteId, int userId);

        Task<bool> ToggleArchiveAsync(int noteId, int userId);

        Task<bool> MoveToTrashAsync(int noteId, int userId);

        Task<bool> RestoreFromTrashAsync(int noteId, int userId);

        Task<List<NoteEntity>> SearchNotesAsync(
            int userId,
            string keyword);

        Task<List<NoteEntity>> FilterNotesAsync(
            int userId,
            bool? isPinned,
            bool? isArchived,
            bool? isTrashed);
    }
}