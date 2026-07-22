namespace EMS.API.DTOs.Designation
{
    public class DesignationSearchRequestDto
    {
        public string? Search { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}