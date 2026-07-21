using EMS.API.DTOs.Authentication;
using EMS.API.Models.Authentication;

namespace EMS.API.Interfaces.Security
{
    public interface IJwtService
    {
        Task<LoginResponseDto> GenerateTokensAsync(User user);
        
    }
}