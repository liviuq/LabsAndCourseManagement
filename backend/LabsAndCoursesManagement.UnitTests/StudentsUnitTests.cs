
using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure.Generics;
using Moq;

namespace LabsAndCoursesManagement.UnitTests
{
    public class StudentsUnitTests
    {
        // sut
        private readonly Student _sut;

        public StudentsUnitTests()
        {
            _sut = new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500);
        }

        [Fact]
        public void Student_Update_UpdatesStudent()
        {
            // arrange
            Student sameStudent = new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500);

            // act
            _sut.Update(sameStudent.Email, sameStudent.FirstName, sameStudent.LastName, sameStudent.Semester, sameStudent.Group, sameStudent.Scholarship);

            // assert
            Assert.NotEqual(Guid.Empty, _sut.Id);
            Assert.Equal(sameStudent.Email, _sut.Email);
            Assert.Equal(sameStudent.FirstName, _sut.FirstName);
            Assert.Equal(sameStudent.LastName, _sut.LastName);
            Assert.Equal(sameStudent.Semester, _sut.Semester);
            Assert.Equal(sameStudent.Group, _sut.Group);
            Assert.Equal(sameStudent.Scholarship, _sut.Scholarship);
        }

        [Fact]
        public void Student_RegisterGradesToStudent_AddsGradesToStudent()
        {
            // arrange
            var grades = new List<Grade>
            {
                new Grade(10, gradeDate: DateTime.Now, false, true),
                new Grade(9, gradeDate: DateTime.Now, false, true)
            };

            // act
            var result = _sut.RegisterGradesToStudent(grades);

            // assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, _sut.Grades.Count);
        }
    }
}