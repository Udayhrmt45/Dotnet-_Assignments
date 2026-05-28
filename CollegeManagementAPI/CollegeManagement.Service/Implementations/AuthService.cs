using BCrypt.Net;
using CollegeManagement.Common.DTOs;
using CollegeManagement.Common.Utilities;
using CollegeManagement.Service.Abstractions;
using CollegeManagement.Store.Abstractions;

namespace CollegeManagement.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAuthStore _authStore;

        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            IAuthStore authStore,
            JwtTokenGenerator jwtTokenGenerator)
        {
            _authStore = authStore;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task RegisterUserAsync(RegisterRequestDTO request)
        {
            request.Password =
                BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _authStore.RegisterUserAsync(request);
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            var user =
                await _authStore.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception("Invalid email");
            }

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash
                );

            if (!isPasswordValid)
            {
                throw new Exception("Invalid password");
            }

            string token =
                _jwtTokenGenerator.GenerateToken(
                    user.Email,
                    user.RoleName
                );

            return new LoginResponseDTO
            {
                Token = token,
                Email = user.Email,
                Role = user.RoleName
            };
        }
    }
}