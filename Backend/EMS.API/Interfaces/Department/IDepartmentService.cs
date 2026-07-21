using EMS.API.Common;
using EMS.API.DTOs.Department;

namespace EMS.API.Interfaces.Department
{
    public interface IDepartmentService
    {
        Task<ApiResponse<int>> CreateDepartmentAsync(CreateDepartmentRequestDto request);

        Task<ApiResponse<List<DepartmentResponseDto>>> GetDepartmentsAsync();

        Task<ApiResponse<DepartmentResponseDto>> GetDepartmentByIdAsync(int departmentId);

        Task<ApiResponse<bool>> UpdateDepartmentAsync(UpdateDepartmentRequestDto request);

        Task<ApiResponse<bool>> DeleteDepartmentAsync(int departmentId);

        Task<ApiResponse<List<DepartmentResponseDto>>> SearchDepartmentsAsync(
            string? search,
            int pageNumber,
            int pageSize);
    }
}