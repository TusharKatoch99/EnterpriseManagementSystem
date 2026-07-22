CREATE OR ALTER PROCEDURE sp_GetEmployees
AS
BEGIN
    SET NOCOUNT ON;

    SELECT

        e.EmployeeId,
        e.UserId,
        e.EmployeeCode,
        e.FirstName,
        e.LastName,
        e.Gender,
        e.DateOfBirth,
        e.Email,
        e.PhoneNumber,

        e.DepartmentId,
        d.DepartmentName,

        e.DesignationId,
        des.DesignationName,

        e.ReportingManagerId,

        CASE
            WHEN rm.EmployeeId IS NULL THEN NULL
            ELSE rm.FirstName + ' ' + rm.LastName
        END AS ReportingManagerName,

        e.DateOfJoining,
        e.Salary,
        e.Address,

        e.IsActive,
        e.CreatedAt

    FROM Employees e

    INNER JOIN Departments d
        ON e.DepartmentId = d.DepartmentId

    INNER JOIN Designations des
        ON e.DesignationId = des.DesignationId

    LEFT JOIN Employees rm
        ON e.ReportingManagerId = rm.EmployeeId

    WHERE e.IsDeleted = 0

    ORDER BY e.EmployeeId DESC;
END