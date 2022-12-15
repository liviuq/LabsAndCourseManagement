using LabsAndCoursesManagement.Domain;

namespace LabsAndCoursesManagement.UnitTests
{
    public class CourseUnitTests
    {
        // unit tests for Course class
        private readonly Course _course;

        public CourseUnitTests()
        {
            _course = new Course("mockTitle", 1, 5);
        }

        // unit test for constructor
        [Fact]
        public void CourseConstructorTest()
        {
            // Arrange
            var title = "mockTitle";
            var semester = 2;
            var credits = 5;

            // Act
            var course = new Course(title, semester, credits);

            // Assert
            Assert.Equal(title, course.Title);
            Assert.Equal(semester, course.Semester);
            Assert.Equal(credits, course.Credits);
        }
        

        [Fact]
        public void Course_Update_UpdatesCourse()
        {
            // arrange
            Course sameCourse = new Course("mockTitle", 1, 5);

            // act
            _course.Update(sameCourse);

            // assert
            Assert.NotEqual(Guid.Empty, _course.Id);
            Assert.Equal(sameCourse.Title, _course.Title);
            Assert.Equal(sameCourse.Semester, _course.Semester);
            Assert.Equal(sameCourse.Credits, _course.Credits);
        }
    }
}
