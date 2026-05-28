using CollegeManagement.Common.Constants;
using CollegeManagement.Common.DTOs;
using CollegeManagement.Data.DbHelpers;
using CollegeManagement.Store.Abstractions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CollegeManagement.Store.Implementations
{
    public class StudentStore : IStudentStore
    {
        private readonly DbHelper _dbHelper;

        public StudentStore(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<StudentResponseDTO>> GetStudentsAsync()
        {
            List<StudentResponseDTO> students = new();

            try
            {
                using SqlConnection connection = _dbHelper.GetConnection();

                using SqlCommand command = new SqlCommand(SqlConstants.GetStudents, connection);

                command.CommandType = CommandType.StoredProcedure;

                await connection.OpenAsync();

                using SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    students.Add(new StudentResponseDTO
                    {
                        StudentGuid = Guid.Parse(reader["student_guid"].ToString()),

                        StudentGuidString = reader["student_guid_string"].ToString(),

                        StudentId = Convert.ToInt32(reader["student_id"]),

                        StudentName = reader["student_name"].ToString(),

                        Email = reader["email"].ToString(),

                        PhoneNumber = reader["phone_number"].ToString(),

                        DepartmentId = Convert.ToInt32(reader["department_id"]),

                        DepartmentName = reader["department_name"].ToString()
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }

            return students;
        }

        public async Task<StudentResponseDTO> GetStudentByIdAsync(Guid studentGuid)
        {
            StudentResponseDTO student = null;

            try
            {
                using SqlConnection connection = _dbHelper.GetConnection();

                using SqlCommand command = new SqlCommand(SqlConstants.GetStudentById, connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@student_guid", studentGuid);

                await connection.OpenAsync();

                using SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    student = new StudentResponseDTO
                    {
                        StudentGuid = Guid.Parse(reader["student_guid"].ToString()),

                        StudentGuidString = reader["student_guid_string"].ToString(),

                        StudentId = Convert.ToInt32(reader["student_id"]),

                        StudentName = reader["student_name"].ToString(),

                        Email = reader["email"].ToString(),

                        PhoneNumber = reader["phone_number"].ToString(),

                        DepartmentId = Convert.ToInt32(reader["department_id"]),

                        DepartmentName = reader["department_name"].ToString()
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }

            return student;
        }

        public async Task<int> InsertStudentAsync(StudentRequestDTO request)
        {
            try
            {
                using SqlConnection connection = _dbHelper.GetConnection();

                using SqlCommand command = new SqlCommand(SqlConstants.InsertStudent, connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@student_name", request.StudentName);
                command.Parameters.AddWithValue("@email", request.Email);
                command.Parameters.AddWithValue("@phone_number", request.PhoneNumber);
                command.Parameters.AddWithValue("@department_id", request.DepartmentId);

                await connection.OpenAsync();

                return await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateStudentAsync(Guid studentGuid, StudentRequestDTO request)
        {
            try
            {
                using SqlConnection connection = _dbHelper.GetConnection();

                using SqlCommand command = new SqlCommand(SqlConstants.UpdateStudent, connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@student_guid", studentGuid);
                command.Parameters.AddWithValue("@student_name", request.StudentName);
                command.Parameters.AddWithValue("@email", request.Email);
                command.Parameters.AddWithValue("@phone_number", request.PhoneNumber);
                command.Parameters.AddWithValue("@department_id", request.DepartmentId);

                await connection.OpenAsync();

                return await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeleteStudentAsync(Guid studentGuid)
        {
            try
            {
                using SqlConnection connection = _dbHelper.GetConnection();

                using SqlCommand command = new SqlCommand(SqlConstants.DeleteStudent, connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@student_guid", studentGuid);

                await connection.OpenAsync();

                return await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
