using EMS.API.Common;
using EMS.API.DTOs.Designation;

namespace EMS.API.Interfaces.Designation
{
    public interface IDesignationService
    {
        Task<ApiResponse<int>> CreateDesignationAsync(CreateDesignationRequestDto request);

        Task<ApiResponse<List<DesignationResponseDto>>> GetDesignationsAsync();

        Task<ApiResponse<DesignationResponseDto>> GetDesignationByIdAsync(int designationId);

        Task<ApiResponse<string>> UpdateDesignationAsync(UpdateDesignationRequestDto request);

        Task<ApiResponse<string>> DeleteDesignationAsync(int designationId);

        Task<ApiResponse<List<DesignationResponseDto>>> SearchDesignationsAsync(
            DesignationSearchRequestDto request);
    }
}