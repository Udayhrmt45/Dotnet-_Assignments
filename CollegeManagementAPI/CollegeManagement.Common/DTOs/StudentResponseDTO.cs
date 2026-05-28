namespace CollegeManagement.Common.DTOs
{
    public class StudentResponseDTO
    {
        public Guid StudentGuid { get; set; }

        public string StudentGuidString { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}