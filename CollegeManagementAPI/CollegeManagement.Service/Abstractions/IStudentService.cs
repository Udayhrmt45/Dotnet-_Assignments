using CollegeManagement.Common.DTOs;

namespace CollegeManagement.Service.Abstractions
{
    public interface IStudentService
    {
        Task<List<StudentResponseDTO>> GetStudentsAsync();

        Task<StudentResponseDTO> GetStudentByIdAsync(Guid studentGuid);

        Task<int> InsertStudentAsync(StudentRequestDTO request);

        Task<int> UpdateStudentAsync(Guid studentGuid, StudentRequestDTO request);

        Task<int> DeleteStudentAsync(Guid studentGuid);
    }
}