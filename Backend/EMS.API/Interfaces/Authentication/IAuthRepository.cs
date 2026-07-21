using EMS.API.Models.Authentication;

namespace EMS.API.Interfaces.Authentication
{
    public interface IAuthRepository
    {
        Task<User?> GetUserWithRolesByEmailAsync(string email);

        Task<int> CreateUserAsync(User user);

        Task<RefreshToken?> GetRefreshTokenAsync(string token);

        Task<User?> GetUserWithRolesByIdAsync(int userId);

        Task RevokeRefreshTokenAsync(string token);

        Task RevokeRefreshTokenAsyncById(int refreshTokenId);

        Task SaveRefreshTokenAsync(RefreshToken refreshToken);
    }
}