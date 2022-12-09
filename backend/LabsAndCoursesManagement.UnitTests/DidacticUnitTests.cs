using LabsAndCoursesManagement.Domain;

namespace LabsAndCoursesManagement.UnitTests
{
    public class DidacticUnitTests
    {
        // unit tests for Didactic class
        private readonly Didactic _didactic;

        public DidacticUnitTests()
        {
            _didactic = new Didactic();
        }

        // unit test for constructor
        [Fact]
        public void DidacticConstructorTest()
        {
            // Arrange

            // Act
            var didactic = new Didactic();

            // Assert
            Assert.NotEqual(Guid.Empty, didactic.Id);
          
        }

        // unit test for attach didactic to course
        [Fact]
        public void AttachDidacticToCourseTest()
        {
            // Arrange
            var course = new Course("mockTitle", 1, 5);

            // Act
            _didactic.AttachDidacticToCourse(course);

            // Assert
            Assert.Equal(course.Id, _didactic.CourseId);
        }

        // unit test for attach didactic to teacher
        [Fact]
        public void AttachDidacticToTeacherTest()
        {
            // Arrange
            var teacher = new Teacher("mockFirstName", "mockLastName", "mockEmail", "mockTeachingDegree");

            // Act
            _didactic.AttachDidacticToTeacher(teacher);

            // Assert
            Assert.Equal(teacher.Id, _didactic.TeacherId);
        }

    }
}
