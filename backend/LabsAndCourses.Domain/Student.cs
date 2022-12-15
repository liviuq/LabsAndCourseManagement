

using LabsAndCoursesManagement.Domain.Helpers;

namespace LabsAndCoursesManagement.Domain
{
    public class Student
    {
        public Student( string email, string firstName, string lastName, int semester, string group, int? scholarship)
        {
            Id = Guid.NewGuid();
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Semester = semester;
            Group = group;
            Scholarship = scholarship;
        }

        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Semester { get; private set; }
        public string Group { get; private set; }
        public int? Scholarship { get; private set; }
        public List<Grade> Grades { get; private set; } = new List<Grade>();
        public Result RegisterGradesToStudent(List<Grade> grades)
        {
            if (!grades.Any())
            {
                return Result.Failure("Add at least a grade to the Student");
            }
            grades.ForEach(g =>
            {
                g.AttachGradeToStudent(this);
                Grades.Add(g);
            });
            return Result.Success();
        }

        // update
        public void Update(Student updatedStudent)
        {
            Email = updatedStudent.Email;
            FirstName = updatedStudent.FirstName;
            LastName = updatedStudent.LastName;
            Semester = updatedStudent.Semester;
            Group = updatedStudent.Group;
            Scholarship = updatedStudent.Scholarship;
        }
    }
}
