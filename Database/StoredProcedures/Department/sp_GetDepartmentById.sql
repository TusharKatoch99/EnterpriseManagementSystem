CREATE OR ALTER PROCEDURE sp_GetDepartmentById
(
    @DepartmentId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        DepartmentId,
        DepartmentCode,
        DepartmentName,
        Description,
        IsActive,
        CreatedBy,
        CreatedAt,
        UpdatedBy,
        UpdatedAt
    FROM Departments
    WHERE DepartmentId = @DepartmentId
      AND IsDeleted = 0;
END
GO