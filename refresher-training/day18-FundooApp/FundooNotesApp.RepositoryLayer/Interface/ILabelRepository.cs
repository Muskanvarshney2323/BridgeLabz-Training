using FundooNotesApp.ModelLayer.Entities;

namespace FundooNotesApp.RepositoryLayer.Interface
{
    public interface ILabelRepository
    {
        Task<LabelEntity> CreateLabelAsync(LabelEntity label);

        Task<List<LabelEntity>> GetAllLabelsAsync(int userId);

        Task<LabelEntity?> GetLabelByIdAsync(int labelId, int userId);

        Task<bool> UpdateLabelAsync(LabelEntity label);

        Task<bool> DeleteLabelAsync(int labelId, int userId);

        Task<bool> AddLabelToNoteAsync(
            int noteId,
            int labelId,
            int userId);

        Task<bool> RemoveLabelFromNoteAsync(
            int noteId,
            int labelId,
            int userId);
    }
}