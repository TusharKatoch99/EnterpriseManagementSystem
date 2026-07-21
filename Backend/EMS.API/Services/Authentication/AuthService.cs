using BCrypt.Net;
using EMS.API.Common;
using EMS.API.DTOs.Authentication;
using EMS.API.Interfaces.Authentication;
using EMS.API.Interfaces.Security;
using EMS.API.Models.Authentication;

namespace EMS.API.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IAuthRepository authRepository,  IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<object>> RegisterAsync(RegisterRequestDto request)
        {
            // Check if email already exists
            var existingUser = await _authRepository.GetUserWithRolesByEmailAsync(request.Email);

            if (existingUser != null)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Email already exists.",
                    Data = null
                };
            }

            // Hash password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Create User object
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = passwordHash
            };

            // Save user
            int userId = await _authRepository.CreateUserAsync(user);

            if (userId <= 0)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "User registration failed.",
                    Data = null
                };
            }

            return new ApiResponse<object>
            {
                Success = true,
                Message = "User registered successfully.",
                Data = new
                {
                    UserId = userId
                }
            };
        }

        public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var user = await _authRepository.GetUserWithRolesByEmailAsync(request.Email);

            if (user == null)
            {
                return ApiResponse<LoginResponseDto>.CreateFailure("Invalid email or password.");
            }

            bool validPassword = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

            if (!validPassword)
            {
                return ApiResponse<LoginResponseDto>.CreateFailure("Invalid email or password.");
            }

            
            var tokens = await _jwtService.GenerateTokensAsync(user);

            var refreshToken = new RefreshToken
            {
                UserId = user.UserId,
                Token = tokens.RefreshToken,
                ExpiresAt = tokens.RefreshTokenExpiresAt,
                CreatedByIp = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString()
            };

            await _authRepository.SaveRefreshTokenAsync(refreshToken);

            return ApiResponse<LoginResponseDto>.CreateSuccess(tokens);
        }

        public async Task<ApiResponse<LoginResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            // Find refresh token
            var storedToken =
                await _authRepository.GetRefreshTokenAsync(request.RefreshToken);

            if (storedToken == null)
            {
                return ApiResponse<LoginResponseDto>.CreateFailure(
                    "Invalid refresh token.");
            }

            if (storedToken.IsRevoked)
            {
                return ApiResponse<LoginResponseDto>.CreateFailure(
                    "Refresh token has been revoked.");
            }

            if (storedToken.ExpiresAt <= DateTime.UtcNow)
            {
                return ApiResponse<LoginResponseDto>.CreateFailure(
                    "Refresh token has expired.");
            }

            // Get user
            var user =
                await _authRepository.GetUserWithRolesByIdAsync(storedToken.UserId);

            if (user == null)
            {
                return ApiResponse<LoginResponseDto>.CreateFailure(
                    "User not found.");
            }

            // Generate new tokens
            var newTokens =
                await _jwtService.GenerateTokensAsync(user);

            // Revoke old refresh token
            await _authRepository.RevokeRefreshTokenAsync(storedToken.Token);

            // Save new refresh token
            var refreshToken = new RefreshToken
            {
                UserId = user.UserId,
                Token = newTokens.RefreshToken,
                ExpiresAt = newTokens.RefreshTokenExpiresAt,
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false
            };

            await _authRepository.SaveRefreshTokenAsync(refreshToken);

            return ApiResponse<LoginResponseDto>.CreateSuccess(
                newTokens,
                "Token refreshed successfully.");
        }

        public async Task<ApiResponse<string>> LogoutAsync(LogoutRequestDto request)
        {
            var refreshToken =
                await _authRepository.GetRefreshTokenAsync(request.RefreshToken);

            if (refreshToken == null)
            {
                return ApiResponse<string>.CreateFailure(
                    "Invalid refresh token.");
            }

            if (refreshToken.IsRevoked)
            {
                return ApiResponse<string>.CreateFailure(
                    "Already logged out.");
            }

            await _authRepository.RevokeRefreshTokenAsyncById(refreshToken.RefreshTokenId);

            return ApiResponse<string>.CreateSuccess(
                "Logout successful.",
                "User logged out successfully.");
        }
    }
}