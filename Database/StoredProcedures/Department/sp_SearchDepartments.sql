CREATE OR ALTER PROCEDURE sp_SearchDepartments
(
    @Search NVARCHAR(100) = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 10
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
        CreatedAt
    FROM Departments
    WHERE
        IsDeleted = 0
        AND
        (
            @Search IS NULL
            OR DepartmentCode LIKE '%' + @Search + '%'
            OR DepartmentName LIKE '%' + @Search + '%'
        )
    ORDER BY DepartmentName
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
GO