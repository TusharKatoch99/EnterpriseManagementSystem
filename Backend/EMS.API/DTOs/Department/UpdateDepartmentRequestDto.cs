using System.ComponentModel.DataAnnotations;

namespace EMS.API.DTOs.Department
{
    public class UpdateDepartmentRequestDto
    {
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(20)]
        public string DepartmentCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}