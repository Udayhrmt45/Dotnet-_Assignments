using CollegeManagement.Common.Constants;
using CollegeManagement.Common.DTOs;
using CollegeManagement.Common.Responses;
using CollegeManagement.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var result = await _studentService.GetStudentsAsync();

            var response = new ApiResponse<List<StudentResponseDTO>>
            {
                Success = true,
                Message = AppConstants.Success,
                Data = result
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var result = await _studentService.GetStudentByIdAsync(id);

            if (result == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = AppConstants.StudentNotFound
                });
            }

            return Ok(new ApiResponse<StudentResponseDTO>
            {
                Success = true,
                Message = AppConstants.Success,
                Data = result
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> InsertStudent(StudentRequestDTO request)
        {
            await _studentService.InsertStudentAsync(request);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = AppConstants.StudentCreated
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, StudentRequestDTO request)
        {
            await _studentService.UpdateStudentAsync(id, request);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = AppConstants.StudentUpdated
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            await _studentService.DeleteStudentAsync(id);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = AppConstants.StudentDeleted
            });
        }
    }
}