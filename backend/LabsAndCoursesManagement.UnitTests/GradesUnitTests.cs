using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure.Generics;
using Moq;

namespace LabsAndCoursesManagement.UnitTests
{
    public class GradesUnitTests
    {
        // sut
        private readonly Grade _sut;
        // moq course db
        private readonly Mock<IRepository<Course>> _courseDbMock;
        // moq student db
        private readonly Mock<IRepository<Student>> _studentDbMock;

        public GradesUnitTests()
        {
            _sut = new Grade(10, gradeDate: DateTime.Now, false, true);
            _courseDbMock = new Mock<IRepository<Course>>();
            _studentDbMock = new Mock<IRepository<Student>>();
        }

        // unit test for Grade constructor
        [Fact]
        public void GradeConstructorTest()
        {
            // arrange
            var grade = new Grade(10, gradeDate: DateTime.Now, false, true);
            // act
            // assert
            Assert.Equal(10, grade.Value);
        }

        [Fact]
        public void Grade_Update_UpdatesGrade()
        {
            // arrange
            Grade sameGrade = new Grade(10, DateTime.Now, false, true);

            // act
            _sut.Update(sameGrade.Value, sameGrade.GradeDate, sameGrade.IsLabGrade, sameGrade.IsExamGrade);

            // assert
            Assert.NotEqual(Guid.Empty, _sut.Id);
            Assert.Equal(sameGrade.Value, _sut.Value);
            Assert.Equal(sameGrade.GradeDate, _sut.GradeDate);
            Assert.Equal(sameGrade.IsLabGrade, _sut.IsLabGrade);
            Assert.Equal(sameGrade.IsExamGrade, _sut.IsExamGrade);
        }

        [Fact]
        public void Grade_AttachGradeToCourse_AttachesGradeToCourse()
        {
            // arrange
            // setup get course
            var courseMock = new Course("mockCourse", 3, 4);
            _courseDbMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(courseMock);

            // act
            // attach grade to course get from db
            _sut.AttachGradeToCourse(_courseDbMock.Object.Get(courseMock.Id));

            // assert
            Assert.Equal(courseMock.Id, _sut.CourseId);

            // verify get course
            _courseDbMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Grade_AttachGradeToStudent_AttachesGradeToStudent()
        {
            // arrange
            // setup get student
            var studentMock = new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500);
            _studentDbMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(studentMock);

            // act
            // attach grade to student get from db
            _sut.AttachGradeToStudent(_studentDbMock.Object.Get(studentMock.Id));

            // assert
            Assert.Equal(studentMock.Id, _sut.StudentId);

            // verify get student
            _studentDbMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }
    }
}
