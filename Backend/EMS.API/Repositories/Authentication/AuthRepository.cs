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

            User? user = null;

            while (await reader.ReadAsync())
            {
                if (user == null)
                {
                    user = new User
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

                if (reader["RoleName"] != DBNull.Value)
                {
                    user.Roles.Add(reader["RoleName"].ToString()!);
                }
            }

            return user;
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
            command.Parameters.AddWithValue("@CreatedByIp",
                (object?)refreshToken.CreatedByIp ?? DBNull.Value);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_GetRefreshToken", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Token", token);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new RefreshToken
                {
                    RefreshTokenId = Convert.ToInt32(reader["RefreshTokenId"]),
                    UserId = Convert.ToInt32(reader["UserId"]),
                    Token = reader["Token"].ToString()!,
                    ExpiresAt = Convert.ToDateTime(reader["ExpiresAt"]),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                    RevokedAt = reader["RevokedAt"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["RevokedAt"]),
                    IsRevoked = Convert.ToBoolean(reader["IsRevoked"]),
                    CreatedByIp = reader["CreatedByIp"] == DBNull.Value
                        ? null
                        : reader["CreatedByIp"].ToString()
                };
            }

            return null;
        }

        public async Task RevokeRefreshTokenAsync(string token)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_RevokeRefreshToken", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Token", token);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<User?> GetUserWithRolesByIdAsync(int userId)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_GetUserWithRolesById", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@UserId", userId);

            using var reader = await command.ExecuteReaderAsync();

            User? user = null;

            while (await reader.ReadAsync())
            {
                if (user == null)
                {
                    user = new User
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

                if (reader["RoleName"] != DBNull.Value)
                {
                    user.Roles.Add(reader["RoleName"].ToString()!);
                }
            }

            return user;
        }
    }
}