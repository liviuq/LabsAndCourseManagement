
using LabsAndCoursesManagement.Domain;

namespace LabsAndCoursesManagement.UnitTests
{
    public class StudentsUnitTests
    {
        private readonly Student _student;

        public StudentsUnitTests()
        {
            _student = new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500);
        }


        [Fact]
        public void StudentContructorTests()
        {
            // Arrange
            var email = "mockemail";
            var firstName = "mockName";
            var lastName = "mockLastName";
            var semester = 2;
            var group = "2B4";
            var scholarship = 500;

            // Act
            var student = new Student(email, firstName, lastName, semester, group, scholarship);

            // Assert
            Assert.Equal(email, student.Email);
            Assert.Equal(firstName, student.FirstName);
            Assert.Equal(lastName, student.LastName);
            Assert.Equal(semester, student.Semester);
            Assert.Equal(group, student.Group);
            Assert.Equal(scholarship, student.Scholarship);
        }


        [Fact]
        public void Student_Update_UpdatesStudent()
        {
            // arrange
            Student sameStudent = new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500);

            // act
            _student.Update(sameStudent);

            // assert
            Assert.NotEqual(Guid.Empty, _student.Id);
            Assert.Equal(sameStudent.Email, _student.Email);
            Assert.Equal(sameStudent.FirstName, _student.FirstName);
            Assert.Equal(sameStudent.LastName, _student.LastName);
            Assert.Equal(sameStudent.Semester, _student.Semester);
            Assert.Equal(sameStudent.Group, _student.Group);
            Assert.Equal(sameStudent.Scholarship, _student.Scholarship);
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
            var result = _student.RegisterGradesToStudent(grades);

            // assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, _student.Grades.Count);
            Assert.All(grades, grade => Assert.Contains(grade, _student.Grades));
        }

        [Fact]
        public void Student_RegisterGradesToStudent_AddNoGrades()
        {
            // arrange
            var grades = new List<Grade>();

            // act
            var result = _student.RegisterGradesToStudent(grades);

            // assert
            Assert.False(result.IsSuccess);
            Assert.Equal(0, _student.Grades.Count);
            Assert.All(grades, grade => Assert.DoesNotContain(grade, _student.Grades));
        }
           
    }
}