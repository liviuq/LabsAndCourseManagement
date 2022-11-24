

using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace LabsAndCoursesManagement.Domain
{
    public class Didactic
    {
        public Guid TeacherId { get; private set; }
        public Guid CourseId { get; private set; }

        public void AttachDidacticToCourse(Course course)
        {
            CourseId = course.Id;
        }
        public void AttachDidacticToTeacher(Teacher teacher)
        {
            TeacherId = teacher.Id;
        }
    }
}
