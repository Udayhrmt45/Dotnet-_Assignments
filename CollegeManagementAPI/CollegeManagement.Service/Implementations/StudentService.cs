using CollegeManagement.Common.DTOs;
using CollegeManagement.Service.Abstractions;
using CollegeManagement.Store.Abstractions;

namespace CollegeManagement.Service.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentStore _studentStore;

        public StudentService(IStudentStore studentStore)
        {
            _studentStore = studentStore;
        }

        public async Task<List<StudentResponseDTO>> GetStudentsAsync()
        {
            return await _studentStore.GetStudentsAsync();
        }

        public async Task<StudentResponseDTO> GetStudentByIdAsync(Guid studentGuid)
        {
            return await _studentStore.GetStudentByIdAsync(studentGuid);
        }

        public async Task<int> InsertStudentAsync(StudentRequestDTO request)
        {
            return await _studentStore.InsertStudentAsync(request);
        }

        public async Task<int> UpdateStudentAsync(Guid studentGuid, StudentRequestDTO request)
        {
            return await _studentStore.UpdateStudentAsync(studentGuid, request);
        }

        public async Task<int> DeleteStudentAsync(Guid studentGuid)
        {
            return await _studentStore.DeleteStudentAsync(studentGuid);
        }
    }
}