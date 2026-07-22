using System.ComponentModel.DataAnnotations;

namespace EMS.API.DTOs.Employee
{
    public class CreateEmployeeRequestDto
    {
        [Required]
        [MaxLength(20)]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int DesignationId { get; set; }

        public int? ReportingManagerId { get; set; }

        [Required]
        public DateTime DateOfJoining { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;
    }
}