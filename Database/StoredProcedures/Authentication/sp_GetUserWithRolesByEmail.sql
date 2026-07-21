CREATE   PROCEDURE sp_GetUserWithRolesByEmail

(

    @Email NVARCHAR(255)

)

AS

BEGIN

    SET NOCOUNT ON;



    SELECT

        u.UserId,

        u.FirstName,

        u.LastName,

        u.UserName,

        u.Email,

        u.PasswordHash,

        r.RoleName

    FROM Users u

    INNER JOIN UserRoles ur

        ON u.UserId = ur.UserId

    INNER JOIN Roles r

        ON ur.RoleId = r.RoleId

    WHERE u.Email = @Email

      AND u.IsDeleted = 0

      AND u.IsActive = 1;

END