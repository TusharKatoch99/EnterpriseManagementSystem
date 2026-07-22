CREATE OR ALTER PROCEDURE sp_UpdateEmployee
(
    @EmployeeId INT,
    @UserId INT,
    @EmployeeCode NVARCHAR(20),
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Gender NVARCHAR(20),
    @DateOfBirth DATE,
    @Email NVARCHAR(255),
    @PhoneNumber NVARCHAR(20),
    @DepartmentId INT,
    @DesignationId INT,
    @ReportingManagerId INT = NULL,
    @DateOfJoining DATE,
    @Salary DECIMAL(18,2),
    @Address NVARCHAR(500),
    @IsActive BIT,
    @UpdatedBy INT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Employees
    SET
        UserId = @UserId,
        EmployeeCode = @EmployeeCode,
        FirstName = @FirstName,
        LastName = @LastName,
        Gender = @Gender,
        DateOfBirth = @DateOfBirth,
        Email = @Email,
        PhoneNumber = @PhoneNumber,
        DepartmentId = @DepartmentId,
        DesignationId = @DesignationId,
        ReportingManagerId = @ReportingManagerId,
        DateOfJoining = @DateOfJoining,
        Salary = @Salary,
        Address = @Address,
        IsActive = @IsActive,
        UpdatedBy = @UpdatedBy,
        UpdatedAt = GETDATE()
    WHERE
        EmployeeId = @EmployeeId
        AND IsDeleted = 0;
END