using CollegeManagement.Common.DTOs;

namespace CollegeManagement.Store.Abstractions
{
    public interface IAuthStore
    {
        Task<int> RegisterUserAsync(RegisterRequestDTO request);

        Task<UserResponseDTO> GetUserByEmailAsync(string email);
    }
}