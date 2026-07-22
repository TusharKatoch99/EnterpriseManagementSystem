CREATE OR ALTER PROCEDURE sp_CreateEmployee
(
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
    @CreatedBy INT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Employees
    (
        UserId,
        EmployeeCode,
        FirstName,
        LastName,
        Gender,
        DateOfBirth,
        Email,
        PhoneNumber,
        DepartmentId,
        DesignationId,
        ReportingManagerId,
        DateOfJoining,
        Salary,
        Address,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedAt
    )
    VALUES
    (
        @UserId,
        @EmployeeCode,
        @FirstName,
        @LastName,
        @Gender,
        @DateOfBirth,
        @Email,
        @PhoneNumber,
        @DepartmentId,
        @DesignationId,
        @ReportingManagerId,
        @DateOfJoining,
        @Salary,
        @Address,
        1,
        0,
        @CreatedBy,
        GETDATE()
    );

    SELECT SCOPE_IDENTITY();
END