
using System.ComponentModel.DataAnnotations.Schema;

namespace LabsAndCoursesManagement.Domain
{
    public class Grade
    {
        public Grade(int value, DateTime gradeDate, bool isLabGrade, bool isExamGrade)
        {
            Value = value;
            GradeDate = gradeDate;
            IsLabGrade = isLabGrade;
            IsExamGrade = isExamGrade;
        }

        public Guid StudentId { get; private set; }
        public Guid CourseId { get; private set; }
        public int Value { get; private set; }
        public DateTime GradeDate { get; private set; }
        public bool IsLabGrade { get; private set; }
        public bool IsExamGrade { get; private set; }
        public void AttachGradeToCourse(Course course)
        {
            CourseId = course.Id;
        }
        public void AttachGradeToStudent(Student student)
        {
            StudentId = student.Id;
        }
    }
}
