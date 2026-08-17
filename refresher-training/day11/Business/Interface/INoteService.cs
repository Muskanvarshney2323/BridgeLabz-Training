using Models.DTO;

namespace Business.Interface
{
    public interface INoteService
    {
        NoteDto AddNote(NoteDto dto, int userId);

        List<NoteDto> GetNotes(int userId);

        NoteDto GetNoteById(int id, int userId);

        NoteDto UpdateNote(int id, NoteDto dto, int userId);

        NoteDto PatchNote(int id, NotePatchDto dto, int userId);

        bool DeleteNote(int id, int userId);
    }
}