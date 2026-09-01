using FundooNotesApp.BusinessLayer.Interface;
using FundooNotesApp.ModelLayer.Dtos.Request;
using FundooNotesApp.ModelLayer.Dtos.Response;
using FundooNotesApp.ModelLayer.Entities;
using FundooNotesApp.RepositoryLayer.Interface;

namespace FundooNotesApp.BusinessLayer.Service
{
    public class LabelService : ILabelService
    {
        private readonly ILabelRepository _labelRepository;

        public LabelService(ILabelRepository labelRepository)
        {
            _labelRepository = labelRepository;
        }

        public async Task<LabelResponseDto> CreateLabelAsync(
            CreateLabelRequestDto request,
            int userId)
        {
            var label = new LabelEntity
            {
                Name = request.Name.Trim(),
                UserId = userId
            };

            var result =
                await _labelRepository.CreateLabelAsync(label);

            return MapToResponse(result);
        }

        public async Task<List<LabelResponseDto>> GetAllLabelsAsync(
            int userId)
        {
            var labels =
                await _labelRepository.GetAllLabelsAsync(userId);

            return labels
                .Select(MapToResponse)
                .ToList();
        }

        public async Task<LabelResponseDto?> GetLabelByIdAsync(
            int labelId,
            int userId)
        {
            var label =
                await _labelRepository.GetLabelByIdAsync(
                    labelId,
                    userId);

            if (label == null)
                return null;

            return MapToResponse(label);
        }

        public async Task<bool> UpdateLabelAsync(
            int labelId,
            UpdateLabelRequestDto request,
            int userId)
        {
            var label =
                await _labelRepository.GetLabelByIdAsync(
                    labelId,
                    userId);

            if (label == null)
                return false;

            label.Name = request.Name.Trim();

            return await _labelRepository.UpdateLabelAsync(label);
        }

        public async Task<bool> DeleteLabelAsync(
            int labelId,
            int userId)
        {
            return await _labelRepository.DeleteLabelAsync(
                labelId,
                userId);
        }

        public async Task<bool> AddLabelToNoteAsync(
            int noteId,
            int labelId,
            int userId)
        {
            return await _labelRepository.AddLabelToNoteAsync(
                noteId,
                labelId,
                userId);
        }

        public async Task<bool> RemoveLabelFromNoteAsync(
            int noteId,
            int labelId,
            int userId)
        {
            return await _labelRepository.RemoveLabelFromNoteAsync(
                noteId,
                labelId,
                userId);
        }

        private static LabelResponseDto MapToResponse(
            LabelEntity label)
        {
            return new LabelResponseDto
            {
                LabelId = label.LabelId,
                Name = label.Name,
                UserId = label.UserId
            };
        }
    }
}