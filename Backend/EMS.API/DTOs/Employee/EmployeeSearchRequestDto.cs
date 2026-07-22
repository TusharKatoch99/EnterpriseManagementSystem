namespace EMS.API.DTOs.Employee
{
    public class EmployeeSearchRequestDto
    {
        public string? Search { get; set; }

        public int? DepartmentId { get; set; }

        public int? DesignationId { get; set; }

        public bool? IsActive { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}