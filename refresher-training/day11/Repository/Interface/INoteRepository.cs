using Models.Entity;

namespace Repository.Interface
{
    public interface INoteRepository
    {
        Note AddNote(Note note);

        List<Note> GetNotes(int userId);

        Note GetNoteById(int id, int userId);

        Note UpdateNote(Note note);

        bool DeleteNote(int id, int userId);
    }
}