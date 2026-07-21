using EMS.API.Models.Department;

namespace EMS.API.Interfaces.Department
{
    public interface IDepartmentRepository
    {
        Task<int> CreateDepartmentAsync(DepartmentModel department);

        Task<List<DepartmentModel>> GetDepartmentsAsync();

        Task<DepartmentModel?> GetDepartmentByIdAsync(int departmentId);

        Task UpdateDepartmentAsync(DepartmentModel department);

        Task DeleteDepartmentAsync(int departmentId, int updatedBy);

        Task<List<DepartmentModel>> SearchDepartmentsAsync(
            string? search,
            int pageNumber,
            int pageSize);
    }
}