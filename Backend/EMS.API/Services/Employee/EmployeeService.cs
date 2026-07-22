using EMS.API.Common;
using EMS.API.DTOs.Employee;
using EMS.API.Interfaces.Authentication;
using EMS.API.Interfaces.Common;
using EMS.API.Interfaces.Employee;
using EMS.API.Models.Employee;

namespace EMS.API.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICurrentUserService _currentUserService;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            ICurrentUserService currentUserService)
        {
            _employeeRepository = employeeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<int>> CreateEmployeeAsync(CreateEmployeeRequestDto request)
        {
            var employee = new Models.Employee.EmployeeModel
            {
                UserId = request.UserId,
                EmployeeCode = request.EmployeeCode,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DepartmentId = request.DepartmentId,
                DesignationId = request.DesignationId,
                ReportingManagerId = request.ReportingManagerId,
                DateOfJoining = request.DateOfJoining,
                Salary = request.Salary,
                Address = request.Address,
                CreatedBy = _currentUserService.UserId
            };

            var employeeId = await _employeeRepository.CreateEmployeeAsync(employee);

            return ApiResponse<int>.CreateSuccess(
                employeeId,
                "Employee created successfully.");
        }

        public async Task<ApiResponse<List<EmployeeResponseDto>>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();

            var response = employees.Select(e => new EmployeeResponseDto
            {
                EmployeeId = e.EmployeeId,
                EmployeeCode = e.EmployeeCode,
                FullName = $"{e.FirstName} {e.LastName}",
                Gender = e.Gender,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                DepartmentName = e.DepartmentName ?? string.Empty,
                DesignationName = e.DesignationName ?? string.Empty,
                ReportingManagerName = e.ReportingManagerName,
                DateOfJoining = e.DateOfJoining,
                Salary = e.Salary,
                IsActive = e.IsActive
            }).ToList();

            return ApiResponse<List<EmployeeResponseDto>>
                .CreateSuccess(response);
        }

        public async Task<ApiResponse<EmployeeResponseDto>> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                return ApiResponse<EmployeeResponseDto>
                    .CreateFailure("Employee not found.");
            }

            var response = new EmployeeResponseDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeCode = employee.EmployeeCode,
                FullName = $"{employee.FirstName} {employee.LastName}",
                Gender = employee.Gender,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                DepartmentName = employee.DepartmentName ?? string.Empty,
                DesignationName = employee.DesignationName ?? string.Empty,
                ReportingManagerName = employee.ReportingManagerName,
                DateOfJoining = employee.DateOfJoining,
                Salary = employee.Salary,
                IsActive = employee.IsActive
            };

            return ApiResponse<EmployeeResponseDto>
                .CreateSuccess(response);
        }

        public async Task<ApiResponse<string>> UpdateEmployeeAsync(UpdateEmployeeRequestDto request)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId);

            if (employee == null)
            {
                return ApiResponse<string>
                    .CreateFailure("Employee not found.");
            }

            employee.UserId = request.UserId;
            employee.EmployeeCode = request.EmployeeCode;
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Gender = request.Gender;
            employee.DateOfBirth = request.DateOfBirth;
            employee.Email = request.Email;
            employee.PhoneNumber = request.PhoneNumber;
            employee.DepartmentId = request.DepartmentId;
            employee.DesignationId = request.DesignationId;
            employee.ReportingManagerId = request.ReportingManagerId;
            employee.DateOfJoining = request.DateOfJoining;
            employee.Salary = request.Salary;
            employee.Address = request.Address;
            employee.IsActive = request.IsActive;

            employee.UpdatedBy = _currentUserService.UserId;

            await _employeeRepository.UpdateEmployeeAsync(employee);

            return ApiResponse<string>
                .CreateSuccess(null!, "Employee updated successfully.");
        }

        public async Task<ApiResponse<string>> DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                return ApiResponse<string>
                    .CreateFailure("Employee not found.");
            }

            await _employeeRepository.DeleteEmployeeAsync(
                employeeId,
                _currentUserService.UserId);

            return ApiResponse<string>
                .CreateSuccess(null!, "Employee deleted successfully.");
        }

        public async Task<ApiResponse<List<EmployeeResponseDto>>> SearchEmployeesAsync(EmployeeSearchRequestDto request)
        {
            var employees = await _employeeRepository.SearchEmployeesAsync(
                request.Search,
                request.DepartmentId,
                request.DesignationId,
                request.IsActive,
                request.PageNumber,
                request.PageSize);

            var response = employees.Select(e => new EmployeeResponseDto
            {
                EmployeeId = e.EmployeeId,
                EmployeeCode = e.EmployeeCode,
                FullName = $"{e.FirstName} {e.LastName}",
                Gender = e.Gender,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                DepartmentName = e.DepartmentName ?? string.Empty,
                DesignationName = e.DesignationName ?? string.Empty,
                ReportingManagerName = e.ReportingManagerName,
                DateOfJoining = e.DateOfJoining,
                Salary = e.Salary,
                IsActive = e.IsActive
            }).ToList();

            return ApiResponse<List<EmployeeResponseDto>>
                .CreateSuccess(response);
        }
    }
}