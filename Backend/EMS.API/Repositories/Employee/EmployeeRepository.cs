using System.Data;
using EMS.API.Interfaces.Common;
using EMS.API.Interfaces.Employee;
using EMS.API.Models.Employee;
using Microsoft.Data.SqlClient;

namespace EMS.API.Repositories.Employee
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(IDatabaseConnection databaseConnection)
            : base(databaseConnection)
        {
        }

        public async Task<int> CreateEmployeeAsync(EmployeeModel employee)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_CreateEmployee", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@UserId", employee.UserId);
            command.Parameters.AddWithValue("@EmployeeCode", employee.EmployeeCode);
            command.Parameters.AddWithValue("@FirstName", employee.FirstName);
            command.Parameters.AddWithValue("@LastName", employee.LastName);
            command.Parameters.AddWithValue("@Gender", employee.Gender);
            command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
            command.Parameters.AddWithValue("@Email", employee.Email);
            command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
            command.Parameters.AddWithValue("@DesignationId", employee.DesignationId);

            command.Parameters.AddWithValue(
                "@ReportingManagerId",
                (object?)employee.ReportingManagerId ?? DBNull.Value);

            command.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
            command.Parameters.AddWithValue("@Salary", employee.Salary);
            command.Parameters.AddWithValue("@Address", employee.Address);
            command.Parameters.AddWithValue("@CreatedBy", employee.CreatedBy);

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            var employees = new List<EmployeeModel>();

            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_GetEmployees", connection);

            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                employees.Add(MapEmployee(reader));
            }

            return employees;
        }

        public async Task<EmployeeModel?> GetEmployeeByIdAsync(int employeeId)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_GetEmployeeById", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@EmployeeId", employeeId);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapEmployee(reader);
            }

            return null;
        }

        public async Task UpdateEmployeeAsync(EmployeeModel employee)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_UpdateEmployee", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
            command.Parameters.AddWithValue("@UserId", employee.UserId);
            command.Parameters.AddWithValue("@EmployeeCode", employee.EmployeeCode);
            command.Parameters.AddWithValue("@FirstName", employee.FirstName);
            command.Parameters.AddWithValue("@LastName", employee.LastName);
            command.Parameters.AddWithValue("@Gender", employee.Gender);
            command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
            command.Parameters.AddWithValue("@Email", employee.Email);
            command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
            command.Parameters.AddWithValue("@DesignationId", employee.DesignationId);

            command.Parameters.AddWithValue(
                "@ReportingManagerId",
                (object?)employee.ReportingManagerId ?? DBNull.Value);

            command.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
            command.Parameters.AddWithValue("@Salary", employee.Salary);
            command.Parameters.AddWithValue("@Address", employee.Address);
            command.Parameters.AddWithValue("@IsActive", employee.IsActive);
            command.Parameters.AddWithValue("@UpdatedBy", employee.UpdatedBy);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteEmployeeAsync(int employeeId, int updatedBy)
        {
            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_DeleteEmployee", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@EmployeeId", employeeId);
            command.Parameters.AddWithValue("@UpdatedBy", updatedBy);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<List<EmployeeModel>> SearchEmployeesAsync(string? search, int? departmentId, int? designationId, bool? isActive, int pageNumber, int pageSize)
        {
            var employees = new List<EmployeeModel>();

            using var connection = CreateConnection();

            await connection.OpenAsync();

            using var command = new SqlCommand("sp_SearchEmployees", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Search",
                string.IsNullOrWhiteSpace(search)
                    ? DBNull.Value
                    : search);

            command.Parameters.AddWithValue("@DepartmentId",
                (object?)departmentId ?? DBNull.Value);

            command.Parameters.AddWithValue("@DesignationId",
                (object?)designationId ?? DBNull.Value);

            command.Parameters.AddWithValue("@IsActive",
                (object?)isActive ?? DBNull.Value);

            command.Parameters.AddWithValue("@PageNumber", pageNumber);
            command.Parameters.AddWithValue("@PageSize", pageSize);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                employees.Add(MapEmployee(reader));
            }

            return employees;
        }

        private static EmployeeModel MapEmployee(SqlDataReader reader)
        {
            return new EmployeeModel
            {
                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                UserId = reader["UserId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserId"]),
                EmployeeCode = reader["EmployeeCode"].ToString()!,
                FirstName = reader["FirstName"].ToString()!,
                LastName = reader["LastName"].ToString()!,
                Gender = reader["Gender"].ToString()!,
                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                Email = reader["Email"].ToString()!,
                PhoneNumber = reader["PhoneNumber"].ToString()!,
                DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                DepartmentName = reader["DepartmentName"].ToString(),
                DesignationId = Convert.ToInt32(reader["DesignationId"]),
                DesignationName = reader["DesignationName"].ToString(),
                ReportingManagerId = reader["ReportingManagerId"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(reader["ReportingManagerId"]),
                ReportingManagerName = reader["ReportingManagerName"] == DBNull.Value
                    ? null
                    : reader["ReportingManagerName"].ToString(),
                DateOfJoining = Convert.ToDateTime(reader["DateOfJoining"]),
                Salary = Convert.ToDecimal(reader["Salary"]),
                Address = reader["Address"].ToString()!,
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }
    }
}