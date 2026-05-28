using Microsoft.Data.SqlClient;
using System.Data;
using WebAPI.common.Models;
using WebAPI.data.Implementation;
using WebAPI.store.Abstraction;

namespace WebAPI.store.Implementation
{
    public class EmployeeStore : IEmployeeStore
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public EmployeeStore(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            List<Employee> employees = new List<Employee>();

            using SqlConnection connection = _dbConnectionFactory.CreateConnection();

            using SqlCommand command =
                new SqlCommand(SqlConstants.GetEmployees, connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                employees.Add(new Employee
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    GuidId = Guid.Parse(reader["GuidId"].ToString()),
                    GuidText = reader["GuidText"].ToString(),
                    Name = reader["Name"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    Department = reader["Department"].ToString(),
                    Email = reader["Email"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"])
                });
            }

            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid guidId)
        {
            Employee employee = null;

            using SqlConnection connection = _dbConnectionFactory.CreateConnection();

            using SqlCommand command =
                new SqlCommand(SqlConstants.GetEmployeeById, connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@GuidId", guidId);

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                employee = new Employee
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    GuidId = Guid.Parse(reader["GuidId"].ToString()),
                    GuidText = reader["GuidText"].ToString(),
                    Name = reader["Name"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    Department = reader["Department"].ToString(),
                    Email = reader["Email"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"])
                };
            }

            return employee;
        }

        public async Task<bool> InsertEmployeeAsync(Employee employee)
        {
            using SqlConnection connection = _dbConnectionFactory.CreateConnection();

            using SqlCommand command =
                new SqlCommand(SqlConstants.InsertEmployee, connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Name", employee.Name);

            command.Parameters.AddWithValue("@Age", employee.Age);

            command.Parameters.AddWithValue("@Department", employee.Department);

            command.Parameters.AddWithValue("@Email", employee.Email);

            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            using SqlConnection connection = _dbConnectionFactory.CreateConnection();

            using SqlCommand command =
                new SqlCommand(SqlConstants.UpdateEmployee, connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@GuidId", employee.GuidId);

            command.Parameters.AddWithValue("@Name", employee.Name);

            command.Parameters.AddWithValue("@Age", employee.Age);

            command.Parameters.AddWithValue("@Department", employee.Department);

            command.Parameters.AddWithValue("@Email", employee.Email);

            command.Parameters.AddWithValue("@IsActive", employee.IsActive);

            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteEmployeeAsync(Guid guidId)
        {
            using SqlConnection connection = _dbConnectionFactory.CreateConnection();

            using SqlCommand command =
                new SqlCommand(SqlConstants.DeleteEmployee, connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@GuidId", guidId);

            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }
    }
}
