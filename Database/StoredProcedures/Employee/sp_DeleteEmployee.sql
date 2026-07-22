CREATE OR ALTER PROCEDURE sp_DeleteEmployee
(
    @EmployeeId INT,
    @UpdatedBy INT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Employees
    SET
        IsDeleted = 1,
        UpdatedBy = @UpdatedBy,
        UpdatedAt = GETDATE()
    WHERE EmployeeId = @EmployeeId;
END