using System.ComponentModel.DataAnnotations;

namespace CollegeManagement.Common.DTOs
{
    public class StudentRequestDTO
    {
        [Required]
        public string StudentName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}