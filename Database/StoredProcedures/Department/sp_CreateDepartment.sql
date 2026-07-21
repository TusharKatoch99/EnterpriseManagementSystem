CREATE OR ALTER PROCEDURE sp_CreateDepartment
(
    @DepartmentCode NVARCHAR(20),
    @DepartmentName NVARCHAR(100),
    @Description NVARCHAR(500),
    @CreatedBy INT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Departments
    (
        DepartmentCode,
        DepartmentName,
        Description,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedAt
    )
    VALUES
    (
        @DepartmentCode,
        @DepartmentName,
        @Description,
        1,
        0,
        @CreatedBy,
        GETUTCDATE()
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS DepartmentId;
END
GO