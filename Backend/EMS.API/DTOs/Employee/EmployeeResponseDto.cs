namespace EMS.API.DTOs.Employee
{
    public class EmployeeResponseDto
    {
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public string DesignationName { get; set; } = string.Empty;

        public string? ReportingManagerName { get; set; }

        public DateTime DateOfJoining { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }
    }
}