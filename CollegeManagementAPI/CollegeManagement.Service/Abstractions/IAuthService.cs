using CollegeManagement.Common.DTOs;

namespace CollegeManagement.Service.Abstractions
{
    public interface IAuthService
    {
        Task RegisterUserAsync(RegisterRequestDTO request);

        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request);
    }
}