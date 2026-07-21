using System.ComponentModel.DataAnnotations;

namespace EMS.API.DTOs.Department
{
    public class CreateDepartmentRequestDto
    {
        [Required]
        [MaxLength(20)]
        public string DepartmentCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}