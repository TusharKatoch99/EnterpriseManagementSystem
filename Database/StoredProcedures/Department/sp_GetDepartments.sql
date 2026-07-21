CREATE OR ALTER PROCEDURE sp_GetDepartments
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        DepartmentId,
        DepartmentCode,
        DepartmentName,
        Description,
        IsActive,
        CreatedAt
    FROM Departments
    WHERE IsDeleted = 0
    ORDER BY DepartmentName;
END
GO