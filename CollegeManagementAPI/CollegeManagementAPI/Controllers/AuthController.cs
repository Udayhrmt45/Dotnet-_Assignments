using CollegeManagement.Common.DTOs;
using CollegeManagement.Common.Responses;
using CollegeManagement.Service.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequestDTO request)
        {
            await _authService.RegisterUserAsync(request);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "User registered successfully"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequestDTO request)
        {
            var result = await _authService.LoginAsync(request);

            return Ok(new ApiResponse<LoginResponseDTO>
            {
                Success = true,
                Message = "Login successful",
                Data = result
            });
        }
    }
}