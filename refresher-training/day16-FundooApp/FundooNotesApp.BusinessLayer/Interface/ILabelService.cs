using FundooNotesApp.ModelLayer.Dtos.Request;
using FundooNotesApp.ModelLayer.Dtos.Response;

namespace FundooNotesApp.BusinessLayer.Interface
{
    public interface ILabelService
    {
        Task<LabelResponseDto> CreateLabelAsync(
            CreateLabelRequestDto request,
            int userId);

        Task<List<LabelResponseDto>> GetAllLabelsAsync(
            int userId);

        Task<LabelResponseDto?> GetLabelByIdAsync(
            int labelId,
            int userId);

        Task<bool> UpdateLabelAsync(
            int labelId,
            UpdateLabelRequestDto request,
            int userId);

        Task<bool> DeleteLabelAsync(
            int labelId,
            int userId);

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