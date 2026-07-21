using System.Data;
using EMS.API.Interfaces.Common;
using EMS.API.Interfaces.Department;
using EMS.API.Models.Department;
using Microsoft.Data.SqlClient;

namespace EMS.API.Repositories.Department
{
    public class DepartmentRepository : BaseRepository, IDepartmentRepository
    {
        public DepartmentRepository(IDatabaseConnection databaseConnection)
            : base(databaseConnection)
        {
        }

        public async Task<int> CreateDepartmentAsync(DepartmentModel department)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command =
                new SqlCommand("sp_CreateDepartment", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@DepartmentCode", department.DepartmentCode);
            command.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            command.Parameters.AddWithValue("@Description",
                (object?)department.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreatedBy", department.CreatedBy);

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        public async Task<List<DepartmentModel>> GetDepartmentsAsync()
        {
            var departments = new List<DepartmentModel>();

            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command =
                new SqlCommand("sp_GetDepartments", connection);

            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                departments.Add(new DepartmentModel
                {
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                    DepartmentCode = reader["DepartmentCode"].ToString()!,
                    DepartmentName = reader["DepartmentName"].ToString()!,
                    Description = reader["Description"] == DBNull.Value
                        ? null
                        : reader["Description"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                });
            }

            return departments;
        }

        public async Task<DepartmentModel?> GetDepartmentByIdAsync(int departmentId)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command =
                new SqlCommand("sp_GetDepartmentById", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@DepartmentId", departmentId);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new DepartmentModel
                {
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                    DepartmentCode = reader["DepartmentCode"].ToString()!,
                    DepartmentName = reader["DepartmentName"].ToString()!,
                    Description = reader["Description"] == DBNull.Value
                        ? null
                        : reader["Description"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["UpdatedAt"])
                };
            }

            return null;
        }

        public async Task UpdateDepartmentAsync(DepartmentModel department)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command =
                new SqlCommand("sp_UpdateDepartment", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);
            command.Parameters.AddWithValue("@DepartmentCode", department.DepartmentCode);
            command.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            command.Parameters.AddWithValue("@Description",
                (object?)department.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", department.IsActive);
            command.Parameters.AddWithValue("@UpdatedBy", department.UpdatedBy);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteDepartmentAsync(int departmentId, int updatedBy)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command =
                new SqlCommand("sp_DeleteDepartment", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@DepartmentId", departmentId);
            command.Parameters.AddWithValue("@UpdatedBy", updatedBy);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<List<DepartmentModel>> SearchDepartmentsAsync(string? search, int pageNumber, int pageSize)
        {
            var departments = new List<DepartmentModel>();

            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command =
                new SqlCommand("sp_SearchDepartments", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@Search",
                (object?)search ?? DBNull.Value);

            command.Parameters.AddWithValue("@PageNumber", pageNumber);
            command.Parameters.AddWithValue("@PageSize", pageSize);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                departments.Add(new DepartmentModel
                {
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                    DepartmentCode = reader["DepartmentCode"].ToString()!,
                    DepartmentName = reader["DepartmentName"].ToString()!,
                    Description = reader["Description"] == DBNull.Value
                        ? null
                        : reader["Description"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                });
            }

            return departments;
        }
    }
}