namespace LabsAndCoursesManagement.API.DTOs
{
    public class CreateStudentDto
    {
        public string Email { get;  set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public int Semester { get;  set; }
        public string Group { get;  set; }
        public int? Scholarship { get;  set; }

    }
}
