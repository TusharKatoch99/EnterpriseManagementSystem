CREATE OR ALTER PROCEDURE sp_SearchEmployees
(
    @Search NVARCHAR(100)=NULL,
    @DepartmentId INT=NULL,
    @DesignationId INT=NULL,
    @IsActive BIT=NULL,
    @PageNumber INT=1,
    @PageSize INT=10
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT

        e.EmployeeId,
        e.EmployeeCode,
        e.FirstName,
        e.LastName,
        e.Email,
        e.PhoneNumber,

        d.DepartmentName,

        des.DesignationName,

        CASE
            WHEN rm.EmployeeId IS NULL THEN NULL
            ELSE rm.FirstName + ' ' + rm.LastName
        END AS ReportingManagerName,

        e.DateOfJoining,
        e.Salary,
        e.IsActive

    FROM Employees e

    INNER JOIN Departments d
        ON e.DepartmentId = d.DepartmentId

    INNER JOIN Designations des
        ON e.DesignationId = des.DesignationId

    LEFT JOIN Employees rm
        ON e.ReportingManagerId = rm.EmployeeId

    WHERE

        e.IsDeleted = 0

        AND
        (
            @Search IS NULL
            OR e.FirstName LIKE '%' + @Search + '%'
            OR e.LastName LIKE '%' + @Search + '%'
            OR e.EmployeeCode LIKE '%' + @Search + '%'
            OR e.Email LIKE '%' + @Search + '%'
        )

        AND
        (
            @DepartmentId IS NULL
            OR e.DepartmentId = @DepartmentId
        )

        AND
        (
            @DesignationId IS NULL
            OR e.DesignationId = @DesignationId
        )

        AND
        (
            @IsActive IS NULL
            OR e.IsActive = @IsActive
        )

    ORDER BY e.EmployeeId DESC

    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END