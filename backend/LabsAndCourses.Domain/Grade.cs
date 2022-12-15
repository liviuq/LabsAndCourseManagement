
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace LabsAndCoursesManagement.Domain
{
    public class Grade
    {


        public Guid Id { get; private set; }
        public Guid StudentId { get; private set; }
        public Guid CourseId { get; private set; }
        public int Value { get; private set; }
        public DateTime GradeDate { get; private set; }
        public bool IsLabGrade { get; private set; }
        public bool IsExamGrade { get; private set; }

        public Grade(int value, DateTime gradeDate, bool isLabGrade, bool isExamGrade)
        {
            Id = Guid.NewGuid();
            Value = value;
            GradeDate = gradeDate;
            IsLabGrade = isLabGrade;
            IsExamGrade = isExamGrade;
        }
        
        public void AttachGradeToCourse(Course course)
        {
            CourseId = course.Id;
        }
        public void AttachGradeToStudent(Student student)
        {
            StudentId = student.Id;
        }

        // update
        public void Update(Grade updatedGrade)
        {
            Value = updatedGrade.Value;
            GradeDate = updatedGrade.GradeDate;
            IsLabGrade = updatedGrade.IsLabGrade;
            IsExamGrade = updatedGrade.IsExamGrade;
        }
    }
}
