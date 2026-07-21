CREATE OR ALTER PROCEDURE sp_UpdateDepartment
(
    @DepartmentId INT,
    @DepartmentCode NVARCHAR(20),
    @DepartmentName NVARCHAR(100),
    @Description NVARCHAR(500),
    @IsActive BIT,
    @UpdatedBy INT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Departments
    SET
        DepartmentCode = @DepartmentCode,
        DepartmentName = @DepartmentName,
        Description = @Description,
        IsActive = @IsActive,
        UpdatedBy = @UpdatedBy,
        UpdatedAt = GETUTCDATE()
    WHERE DepartmentId = @DepartmentId
      AND IsDeleted = 0;
END
GO