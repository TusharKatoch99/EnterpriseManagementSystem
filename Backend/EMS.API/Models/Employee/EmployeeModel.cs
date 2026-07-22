namespace EMS.API.Models.Employee
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }

        public int UserId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public int DesignationId { get; set; }

        public string? DesignationName { get; set; }

        public int? ReportingManagerId { get; set; }

        public string? ReportingManagerName { get; set; }

        public DateTime DateOfJoining { get; set; }

        public decimal Salary { get; set; }

        public string Address { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}