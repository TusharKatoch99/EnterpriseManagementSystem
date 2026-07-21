using System.Security.Claims;
using EMS.API.Interfaces.Common;
using Microsoft.AspNetCore.Http;

namespace EMS.API.Services.Common
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

        public int UserId
        {
            get
            {
                var value = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return int.TryParse(value, out int id) ? id : 0;
            }
        }

        public string? Email =>
            User?.FindFirst(ClaimTypes.Email)?.Value;

        public string? UserName =>
            User?.FindFirst(ClaimTypes.Name)?.Value;

        public string? FirstName =>
            User?.FindFirst(ClaimTypes.GivenName)?.Value;

        public string? LastName =>
            User?.FindFirst(ClaimTypes.Surname)?.Value;

        public IReadOnlyList<string> Roles =>
            User?
                .FindAll(ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList()
            ?? new List<string>();
    }
}