using LabsAndCoursesManagement.Domain;

namespace LabsAndCoursesManagement.UnitTests
{
    public class TeacherUnitTests
    {
        // unit tests for Teacher class
        private readonly Teacher _teacher;

        public TeacherUnitTests()
        {
            _teacher = new Teacher("mockFirstName", "mockLastName", "mockEmail", "mockTeachingDegree");
        }

        // unit test for constructor
        [Fact]
        public void TeacherConstructorTest()
        {
            // Arrange
            var firstName = "mockFirstName";
            var lastName = "mockLastName";
            var email = "mockEmail";
            var teachingDegree = "mockTeachingDegree";

            // Act
            var teacher = new Teacher(firstName, lastName, email, teachingDegree);

            // Assert
            Assert.NotEqual(Guid.Empty, teacher.Id);
            Assert.Equal(firstName, teacher.FirstName);
            Assert.Equal(lastName, teacher.LastName);
            Assert.Equal(email, teacher.Email);
            Assert.Equal(teachingDegree, teacher.TeachingDegree);
        }

        // unit test for update teacher
        [Fact]
        public void UpdateTeacherTest()
        {
            // Arrange
            Teacher sameTeacher = new Teacher("mockFirstName", "mockLastName", "mockEmail", "mockTeachingDegree");

            // Act
            _teacher.Update(sameTeacher);

            // Assert
            Assert.Equal(sameTeacher.FirstName, _teacher.FirstName);
            Assert.Equal(sameTeacher.LastName, _teacher.LastName);
            Assert.Equal(sameTeacher.Email, _teacher.Email);
            Assert.Equal(sameTeacher.TeachingDegree, _teacher.TeachingDegree);
        }
    }
}
