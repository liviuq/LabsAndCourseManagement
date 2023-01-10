using LabsAndCoursesManagement.Domain;

namespace LabsAndCoursesManagement.UnitTests
{
    public class EnrollmentUnitTests
    {
        // unit tests for Didactic class
        private readonly Enrollment _enrollment;

        public EnrollmentUnitTests()
        {
            _enrollment = new Enrollment();
        }

        // unit test for constructor
        [Fact]
        public void EnrollmentConstructorTest()
        {
            // Arrange

            // Act
            var enrollment = new Enrollment();

            // Assert
            Assert.NotEqual(Guid.Empty, enrollment.Id);
          
        }

        // unit test for attach didactic to course
        [Fact]
        public void AttachEnrollmentToCourseTest()
        {
            // Arrange
            var course = new Course("mockTitle", 1, 5);

            // Act
            _enrollment.AttachEnrollmentToCourse(course);

            // Assert
            Assert.Equal(course.Id, _enrollment.CourseId);
        }

        // unit test for attach didactic to teacher
        [Fact]
        public void AttachEnrollmentToStudentTest()
        {
            // Arrange
            var student = new Student("mockEmail", "mockFirstName", "mockLastName", 3, "A2", 400);

            // Act
            _enrollment.AttachEnrollmentToStudent(student);

            // Assert
            Assert.Equal(student.Id, _enrollment.StudentId);
        }

    }
}
