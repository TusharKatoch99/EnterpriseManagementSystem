using System.Data;
using EMS.API.Interfaces.Authentication;
using EMS.API.Interfaces.Common;
using EMS.API.Models.Authentication;
using Microsoft.Data.SqlClient;

namespace EMS.API.Repositories.Authentication
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        public AuthRepository(IDatabaseConnection databaseConnection)
            : base(databaseConnection)
        {
        }

        public async Task<User?> GetUserWithRolesByEmailAsync(string email)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_GetUserWithRolesByEmail", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Email", email);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new User
                {
                    UserId = Convert.ToInt32(reader["UserId"]),
                    FirstName = reader["FirstName"].ToString()!,
                    LastName = reader["LastName"].ToString()!,
                    UserName = reader["UserName"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    PasswordHash = reader["PasswordHash"].ToString()!,
                    Roles = new List<string>()
                };
            }

            return null;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_RegisterUser", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);
            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@PhoneNumber",
                string.IsNullOrWhiteSpace(user.PhoneNumber)
                    ? DBNull.Value
                    : user.PhoneNumber);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_SaveRefreshToken", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@UserId", refreshToken.UserId);
            command.Parameters.AddWithValue("@Token", refreshToken.Token);
            command.Parameters.AddWithValue("@ExpiresAt", refreshToken.ExpiresAt);

            await command.ExecuteNonQueryAsync();
        }
    }
}