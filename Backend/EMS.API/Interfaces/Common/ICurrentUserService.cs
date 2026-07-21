namespace EMS.API.Interfaces.Common
{
    public interface ICurrentUserService
    {
        int UserId { get; }

        string? Email { get; }

        string? UserName { get; }

        string? FirstName { get; }

        string? LastName { get; }

        IReadOnlyList<string> Roles { get; }

        bool IsAuthenticated { get; }

    }
}