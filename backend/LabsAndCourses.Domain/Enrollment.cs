

namespace LabsAndCoursesManagement.Domain
{
    public class Enrollment

    {
        public Enrollment()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public Guid StudentId { get; private set; }
        public Guid CourseId { get; private set; }

        public Student Student { get; private set; }
        public Course Course { get; private set; }


        public void AttachEnrollmentToCourse(Course course)
        {
            CourseId = course.Id;
            Course = course;
        }
        public void AttachEnrollmentToStudent(Student student)
        {
            StudentId = student.Id;
            Student = student;
        }
    }
}
