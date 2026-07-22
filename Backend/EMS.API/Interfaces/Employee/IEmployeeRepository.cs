using EMS.API.Models.Employee;

namespace EMS.API.Interfaces.Employee
{
    public interface IEmployeeRepository
    {
        Task<int> CreateEmployeeAsync(EmployeeModel employee);

        Task<List<EmployeeModel>> GetEmployeesAsync();

        Task<EmployeeModel?> GetEmployeeByIdAsync(int employeeId);

        Task UpdateEmployeeAsync(EmployeeModel employee);

        Task DeleteEmployeeAsync(int employeeId, int updatedBy);

        Task<List<EmployeeModel>> SearchEmployeesAsync(
            string? search,
            int? departmentId,
            int? designationId,
            bool? isActive,
            int pageNumber,
            int pageSize);
    }
}