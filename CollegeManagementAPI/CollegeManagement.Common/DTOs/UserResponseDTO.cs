namespace CollegeManagement.Common.DTOs
{
    public class UserResponseDTO
    {
        public Guid UserGuid { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string RoleName { get; set; }
    }
}