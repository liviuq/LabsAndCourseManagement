

namespace LabsAndCoursesManagement.Domain
{
    public class Didactic
    {
        public Didactic()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public Guid TeacherId { get; private set; }
        public Guid CourseId { get; private set; }

        public Teacher Teacher { get; private set; }
        public Course Course { get; private set; }


        public void AttachDidacticToCourse(Course course)
        {
            CourseId = course.Id;
            Course = course;
        }
        public void AttachDidacticToTeacher(Teacher teacher)
        {
            TeacherId = teacher.Id;
            Teacher = teacher;
        }
    }
}
