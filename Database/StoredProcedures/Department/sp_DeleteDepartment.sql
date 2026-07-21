CREATE OR ALTER PROCEDURE sp_DeleteDepartment
(
    @DepartmentId INT,
    @UpdatedBy INT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Departments
    SET
        IsDeleted = 1,
        UpdatedBy = @UpdatedBy,
        UpdatedAt = GETUTCDATE()
    WHERE DepartmentId = @DepartmentId;
END
GO