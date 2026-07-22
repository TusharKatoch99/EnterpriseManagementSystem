using EMS.API.Common;
using EMS.API.DTOs.Designation;
using EMS.API.Interfaces.Designation;
using EMS.API.Models.Designation;

namespace EMS.API.Services.Designation
{
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository _designationRepository;

        public DesignationService(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }

        #region Create

        public async Task<ApiResponse<int>> CreateDesignationAsync(CreateDesignationRequestDto request)
        {
            var designation = new DesignationModel
            {
                DesignationCode = request.DesignationCode,
                DesignationName = request.DesignationName,
                Description = request.Description
            };

            var designationId = await _designationRepository.CreateDesignationAsync(designation);

            return ApiResponse<int>.CreateSuccess(
                designationId,
                "Designation created successfully.");
        }

        #endregion

        #region Get All

        public async Task<ApiResponse<List<DesignationResponseDto>>> GetDesignationsAsync()
        {
            var designations = await _designationRepository.GetDesignationsAsync();

            var response = designations.Select(d => new DesignationResponseDto
            {
                DesignationCode = d.DesignationCode,
                DesignationId = d.DesignationId,
                DesignationName = d.DesignationName,
                Description = d.Description,
                IsActive = d.IsActive,
                CreatedAt = d.CreatedAt
            }).ToList();

            return ApiResponse<List<DesignationResponseDto>>
                .CreateSuccess(response);
        }

        #endregion

        #region Get By Id

        public async Task<ApiResponse<DesignationResponseDto>> GetDesignationByIdAsync(int designationId)
        {
            var designation = await _designationRepository.GetDesignationByIdAsync(designationId);

            if (designation == null)
            {
                return ApiResponse<DesignationResponseDto>
                    .CreateFailure("Designation not found.");
            }

            var response = new DesignationResponseDto
            {
                DesignationCode = designation.DesignationCode,
                DesignationId = designation.DesignationId,
                DesignationName = designation.DesignationName,
                Description = designation.Description,
                IsActive = designation.IsActive,
                CreatedAt = designation.CreatedAt
            };

            return ApiResponse<DesignationResponseDto>
                .CreateSuccess(response);
        }

        #endregion

        #region Update

        public async Task<ApiResponse<string>> UpdateDesignationAsync(UpdateDesignationRequestDto request)
        {
            var designation = await _designationRepository.GetDesignationByIdAsync(request.DesignationId);

            if (designation == null)
            {
                return ApiResponse<string>
                    .CreateFailure("Designation not found.");
            }

            designation.DesignationCode = request.DesignationCode;
            designation.DesignationName = request.DesignationName;
            designation.Description = request.Description;
            designation.IsActive = request.IsActive;

            await _designationRepository.UpdateDesignationAsync(designation);

            return ApiResponse<string>
                .CreateSuccess(null!, "Designation updated successfully.");
        }

        #endregion

        #region Delete

        public async Task<ApiResponse<string>> DeleteDesignationAsync(int designationId)
        {
            var designation = await _designationRepository.GetDesignationByIdAsync(designationId);

            if (designation == null)
            {
                return ApiResponse<string>
                    .CreateFailure("Designation not found.");
            }

            await _designationRepository.DeleteDesignationAsync(designationId);

            return ApiResponse<string>
                .CreateSuccess(null!, "Designation deleted successfully.");
        }

        #endregion

        #region Search

        public async Task<ApiResponse<List<DesignationResponseDto>>> SearchDesignationsAsync(
            DesignationSearchRequestDto request)
        {
            var designations = await _designationRepository.SearchDesignationsAsync(
                request.Search,
                request.PageNumber,
                request.PageSize);

            var response = designations.Select(d => new DesignationResponseDto
            {
                DesignationCode = d.DesignationCode,
                DesignationId = d.DesignationId,
                DesignationName = d.DesignationName,
                Description = d.Description,
                IsActive = d.IsActive,
                CreatedAt = d.CreatedAt
            }).ToList();

            return ApiResponse<List<DesignationResponseDto>>
                .CreateSuccess(response);
        }

        #endregion
    }
}