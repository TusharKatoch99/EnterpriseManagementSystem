using EMS.API.Common;
using EMS.API.DTOs.Authentication;

namespace EMS.API.Interfaces.Authentication
{
    public interface IAuthService
    {
        Task<ApiResponse<object>> RegisterAsync(RegisterRequestDto request);

        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    }
}