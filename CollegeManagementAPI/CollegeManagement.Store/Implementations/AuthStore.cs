using CollegeManagement.Common.DTOs;
using CollegeManagement.Data.DbHelpers;
using CollegeManagement.Store.Abstractions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CollegeManagement.Store.Implementations
{
    public class AuthStore : IAuthStore
    {
        private readonly DbHelper _dbHelper;

        public AuthStore(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<int> RegisterUserAsync(RegisterRequestDTO request)
        {
            using SqlConnection connection = _dbHelper.GetConnection();

            using SqlCommand command =
                new SqlCommand("usp_RegisterUser", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@full_name", request.FullName);
            command.Parameters.AddWithValue("@email", request.Email);
            command.Parameters.AddWithValue("@password_hash", request.Password);
            command.Parameters.AddWithValue("@role_name", request.RoleName);

            await connection.OpenAsync();

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<UserResponseDTO> GetUserByEmailAsync(string email)
        {
            UserResponseDTO user = null;

            using SqlConnection connection = _dbHelper.GetConnection();

            using SqlCommand command =
                new SqlCommand("usp_GetUserByEmail", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@email", email);

            await connection.OpenAsync();

            using SqlDataReader reader =
                await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                user = new UserResponseDTO
                {
                    UserGuid = Guid.Parse(reader["user_guid"].ToString()),
                    FullName = reader["full_name"].ToString(),
                    Email = reader["email"].ToString(),
                    PasswordHash = reader["password_hash"].ToString(),
                    RoleName = reader["role_name"].ToString()
                };
            }

            return user;
        }
    }
}