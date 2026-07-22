using EMS.API.Common;
using EMS.API.DTOs.Employee;

namespace EMS.API.Interfaces.Employee
{
    public interface IEmployeeService
    {
        Task<ApiResponse<int>> CreateEmployeeAsync(CreateEmployeeRequestDto request);

        Task<ApiResponse<List<EmployeeResponseDto>>> GetEmployeesAsync();

        Task<ApiResponse<EmployeeResponseDto>> GetEmployeeByIdAsync(int employeeId);

        Task<ApiResponse<string>> UpdateEmployeeAsync(UpdateEmployeeRequestDto request);

        Task<ApiResponse<string>> DeleteEmployeeAsync(int employeeId);

        Task<ApiResponse<List<EmployeeResponseDto>>> SearchEmployeesAsync(EmployeeSearchRequestDto request);
    }
}