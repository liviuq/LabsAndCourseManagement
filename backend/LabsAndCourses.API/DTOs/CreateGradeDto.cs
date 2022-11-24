namespace LabsAndCoursesManagement.API.DTOs
{
    public class CreateGradeDto
    {
        public int Value { get;  set; }
        public DateTime GradeDate { get; set; }
        public bool IsLabGrade { get; set; }
        public bool IsExamGrade { get; set; }

    }
}
