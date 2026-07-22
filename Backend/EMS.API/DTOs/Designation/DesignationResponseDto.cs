namespace EMS.API.DTOs.Designation
{
    public class DesignationResponseDto
    {
        public int DesignationId { get; set; }
        
        public string DesignationCode { get; set; } = string.Empty;

        public string DesignationName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}