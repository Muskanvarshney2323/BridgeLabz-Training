using FundooNotesApp.ModelLayer.Entities;

namespace FundooNotesApp.RepositoryLayer.Interface
{
    public interface INoteRepository
    {
        Task<NoteEntity> CreateNoteAsync(NoteEntity note);

        Task<List<NoteEntity>> GetAllNotesAsync(int userId);

        Task<NoteEntity?> GetNoteByIdAsync(int noteId);

        Task<bool> DeleteNoteAsync(int noteId);
    }
}