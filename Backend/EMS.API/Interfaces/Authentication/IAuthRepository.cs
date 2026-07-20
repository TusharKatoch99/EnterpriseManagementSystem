using EMS.API.Models.Authentication;

namespace EMS.API.Interfaces.Authentication
{
    public interface IAuthRepository
    {
        Task<User?> GetUserWithRolesByEmailAsync(string email);

        Task<int> CreateUserAsync(User user);

        Task SaveRefreshTokenAsync(RefreshToken refreshToken);
    }
}