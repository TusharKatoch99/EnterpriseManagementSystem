namespace EMS.API.DTOs.Department
{
    public class DepartmentSearchRequestDto
    {
        public string? Search { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}