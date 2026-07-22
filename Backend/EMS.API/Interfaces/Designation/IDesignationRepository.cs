using EMS.API.Models.Designation;

namespace EMS.API.Interfaces.Designation
{
    public interface IDesignationRepository
    {
        Task<int> CreateDesignationAsync(DesignationModel designation);

        Task<List<DesignationModel>> GetDesignationsAsync();

        Task<DesignationModel?> GetDesignationByIdAsync(int designationId);

        Task UpdateDesignationAsync(DesignationModel designation);

        Task DeleteDesignationAsync(int designationId);

        Task<List<DesignationModel>> SearchDesignationsAsync(
            string? search,
            int pageNumber,
            int pageSize);
    }
}