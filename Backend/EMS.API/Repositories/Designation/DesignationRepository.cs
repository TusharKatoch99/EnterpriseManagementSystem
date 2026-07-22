using System.Data;
using EMS.API.Interfaces.Common;
using EMS.API.Interfaces.Designation;
using EMS.API.Models.Designation;
using Microsoft.Data.SqlClient;

namespace EMS.API.Repositories.Designation
{
    public class DesignationRepository : BaseRepository, IDesignationRepository
    {
        public DesignationRepository(IDatabaseConnection databaseConnection)
            : base(databaseConnection)
        {
        }

        #region Create

        public async Task<int> CreateDesignationAsync(DesignationModel designation)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_CreateDesignation", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@DesignationCode", designation.DesignationCode);
            command.Parameters.AddWithValue("@DesignationName", designation.DesignationName);
            command.Parameters.AddWithValue("@Description",
                (object?)designation.Description ?? DBNull.Value);

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        #endregion

        #region Get All

        public async Task<List<DesignationModel>> GetDesignationsAsync()
        {
            var designations = new List<DesignationModel>();

            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_GetDesignations", connection);

            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                designations.Add(new DesignationModel
                {
                    DesignationCode = reader["DesignationCode"].ToString()!,
                    DesignationId = Convert.ToInt32(reader["DesignationId"]),
                    DesignationName = reader["DesignationName"].ToString()!,
                    Description = reader["Description"] as string,
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                });
            }

            return designations;
        }

        #endregion

        #region Get By Id

        public async Task<DesignationModel?> GetDesignationByIdAsync(int designationId)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_GetDesignationById", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@DesignationId", designationId);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new DesignationModel
                {
                    DesignationCode = reader["DesignationCode"].ToString()!,
                    DesignationId = Convert.ToInt32(reader["DesignationId"]),
                    DesignationName = reader["DesignationName"].ToString()!,
                    Description = reader["Description"] as string,
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                };
            }

            return null;
        }

        #endregion

        #region Update

        public async Task UpdateDesignationAsync(DesignationModel designation)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_UpdateDesignation", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@DesignationCode", designation.DesignationCode);
            command.Parameters.AddWithValue("@DesignationId", designation.DesignationId);
            command.Parameters.AddWithValue("@DesignationName", designation.DesignationName);
            command.Parameters.AddWithValue("@Description",
                (object?)designation.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", designation.IsActive);

            await command.ExecuteNonQueryAsync();
        }

        #endregion

        #region Delete

        public async Task DeleteDesignationAsync(int designationId)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_DeleteDesignation", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@DesignationId", designationId);

            await command.ExecuteNonQueryAsync();
        }

        #endregion

        #region Search

        public async Task<List<DesignationModel>> SearchDesignationsAsync(
            string? search,
            int pageNumber,
            int pageSize)
        {
            var designations = new List<DesignationModel>();

            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_SearchDesignations", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Search",
                string.IsNullOrWhiteSpace(search) ? DBNull.Value : search);

            command.Parameters.AddWithValue("@PageNumber", pageNumber);

            command.Parameters.AddWithValue("@PageSize", pageSize);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                designations.Add(new DesignationModel
                {
                    DesignationCode = reader["DesignationCode"].ToString()!,
                    DesignationId = Convert.ToInt32(reader["DesignationId"]),
                    DesignationName = reader["DesignationName"].ToString()!,
                    Description = reader["Description"] as string,
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                });
            }

            return designations;
        }

        #endregion
    }
}