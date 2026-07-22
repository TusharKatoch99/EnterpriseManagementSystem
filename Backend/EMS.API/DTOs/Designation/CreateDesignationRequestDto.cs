using System.ComponentModel.DataAnnotations;

namespace EMS.API.DTOs.Designation
{
    public class CreateDesignationRequestDto
    {

        [Required]
        [MaxLength(20)]
        public string DesignationCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string DesignationName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}