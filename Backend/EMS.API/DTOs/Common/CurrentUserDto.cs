namespace EMS.API.DTOs.Common
{
    public class CurrentUserDto
    {
        public int UserId { get; set; }

        public string? Email { get; set; }

        public string? UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public List<string> Roles { get; set; } = new();
    }
}