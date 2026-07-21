using EMS.API.Common;
using EMS.API.DTOs.Department;
using EMS.API.Interfaces.Common;
using EMS.API.Interfaces.Department;
using EMS.API.Models.Department;

namespace EMS.API.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICurrentUserService _currentUser;

        public DepartmentService(
            IDepartmentRepository departmentRepository,
            ICurrentUserService currentUser)
        {
            _departmentRepository = departmentRepository;
            _currentUser = currentUser;
        }


        public async Task<ApiResponse<int>> CreateDepartmentAsync( CreateDepartmentRequestDto request)
        {
            var department = new Models.Department.DepartmentModel
            {
                DepartmentCode = request.DepartmentCode,
                DepartmentName = request.DepartmentName,
                Description = request.Description,
                CreatedBy = _currentUser.UserId
            };

            int departmentId =
                await _departmentRepository.CreateDepartmentAsync(department);

            return ApiResponse<int>.CreateSuccess(
                departmentId,
                "Department created successfully.");
        }

        public async Task<ApiResponse<List<DepartmentResponseDto>>> GetDepartmentsAsync()
        {
            var departments =
                await _departmentRepository.GetDepartmentsAsync();

            var response = departments.Select(x => new DepartmentResponseDto
            {
                DepartmentId = x.DepartmentId,
                DepartmentCode = x.DepartmentCode,
                DepartmentName = x.DepartmentName,
                Description = x.Description,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt
            }).ToList();

            return ApiResponse<List<DepartmentResponseDto>>
                .CreateSuccess(response);
        }

        public async Task<ApiResponse<DepartmentResponseDto>> GetDepartmentByIdAsync(int departmentId)
        {
            var department =
                await _departmentRepository.GetDepartmentByIdAsync(departmentId);

            if (department == null)
            {
                return ApiResponse<DepartmentResponseDto>.CreateFailure(
                    "Department not found.");
            }

            var response = new DepartmentResponseDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentCode = department.DepartmentCode,
                DepartmentName = department.DepartmentName,
                Description = department.Description,
                IsActive = department.IsActive,
                CreatedAt = department.CreatedAt
            };

            return ApiResponse<DepartmentResponseDto>.CreateSuccess(response);
        }

        public async Task<ApiResponse<bool>> UpdateDepartmentAsync(UpdateDepartmentRequestDto request)
        {
            var department =
                await _departmentRepository.GetDepartmentByIdAsync(request.DepartmentId);

            if (department == null)
            {
                return ApiResponse<bool>.CreateFailure("Department not found.");
            }

            department.DepartmentCode = request.DepartmentCode;
            department.DepartmentName = request.DepartmentName;
            department.Description = request.Description;
            department.IsActive = request.IsActive;
            department.UpdatedBy = _currentUser.UserId;

            await _departmentRepository.UpdateDepartmentAsync(department);

            return ApiResponse<bool>.CreateSuccess(
                true,
                "Department updated successfully.");
        }

        public async Task<ApiResponse<bool>> DeleteDepartmentAsync(int departmentId)
        {
            var department =
                await _departmentRepository.GetDepartmentByIdAsync(departmentId);

            if (department == null)
            {
                return ApiResponse<bool>.CreateFailure("Department not found.");
            }

            await _departmentRepository.DeleteDepartmentAsync(
                departmentId,
                _currentUser.UserId);

            return ApiResponse<bool>.CreateSuccess(
                true,
                "Department deleted successfully.");
        }

        public async Task<ApiResponse<List<DepartmentResponseDto>>> SearchDepartmentsAsync(string? search, int pageNumber, int pageSize)
        {
            var departments =
                await _departmentRepository.SearchDepartmentsAsync(
                    search,
                    pageNumber,
                    pageSize);

            var response = departments.Select(x => new DepartmentResponseDto
            {
                DepartmentId = x.DepartmentId,
                DepartmentCode = x.DepartmentCode,
                DepartmentName = x.DepartmentName,
                Description = x.Description,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt
            }).ToList();

            return ApiResponse<List<DepartmentResponseDto>>
                .CreateSuccess(response);
        }
    }
}