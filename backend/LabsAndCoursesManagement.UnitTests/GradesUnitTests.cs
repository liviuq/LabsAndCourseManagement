using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure.Generics;
using Moq;

namespace LabsAndCoursesManagement.UnitTests
{
    public class GradesUnitTests
    {
        // unit tests for Grade class
        private readonly Grade _grade;
        // moq course db
        private readonly Mock<IRepository<Course>> _courseDbMock;
        // moq student db
        private readonly Mock<IRepository<Student>> _studentDbMock;

        public GradesUnitTests()
        {
            _grade = new Grade(10, gradeDate: DateTime.Now, false, true);
            _courseDbMock = new Mock<IRepository<Course>>();
            _studentDbMock = new Mock<IRepository<Student>>();
        }

        // unit test for Grade constructor
        [Fact]
        public void GradeConstructorTest()
        {
            // Arrange
            var value = 80;
            var gradeDate = new DateTime(2022, 1, 1);
            var isLabGrade = true;
            var isExamGrade = false;

            // Act
            var grade = new Grade(value, gradeDate, isLabGrade, isExamGrade);

            // Assert
            Assert.Equal(value, grade.Value);
            Assert.Equal(gradeDate, grade.GradeDate);
            Assert.Equal(isLabGrade, grade.IsLabGrade);
            Assert.Equal(isExamGrade, grade.IsExamGrade);
        }

        [Fact]
        public void Grade_Update_UpdatesGrade()
        {
            // arrange
            Grade sameGrade = new Grade(10, DateTime.Now, false, true);

            // act
            _grade.Update(sameGrade);

            // assert
            Assert.NotEqual(Guid.Empty, _grade.Id);
            Assert.Equal(sameGrade.Value, _grade.Value);
            Assert.Equal(sameGrade.GradeDate, _grade.GradeDate);
            Assert.Equal(sameGrade.IsLabGrade, _grade.IsLabGrade);
            Assert.Equal(sameGrade.IsExamGrade, _grade.IsExamGrade);
        }

        [Fact]
        public async Task Grade_AttachGradeToCourse_AttachesGradeToCourse()
        {
            // arrange
            // setup get course
            var courseMock = new Course("mockCourse", 3, 4);
            _courseDbMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(courseMock);

            // act
            // attach grade to course get from db
            var tempCourse = await _courseDbMock.Object.Get(courseMock.Id);
            Assert.NotNull(tempCourse);
            _grade.AttachGradeToCourse(tempCourse);

            // assert
            Assert.Equal(courseMock.Id, _grade.CourseId);

            // verify get course
            _courseDbMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Grade_AttachGradeToStudent_AttachesGradeToStudent()
        {
            // arrange
            // setup get student
            var studentMock = new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500);
            _studentDbMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(studentMock);

            // act
            // attach grade to student get from db
            var tempStudent = await _studentDbMock.Object.Get(studentMock.Id);
            Assert.NotNull(tempStudent);
            _grade.AttachGradeToStudent(tempStudent);

            // assert
            Assert.Equal(studentMock.Id, _grade.StudentId);

            // verify get student
            _studentDbMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }
    }
}
