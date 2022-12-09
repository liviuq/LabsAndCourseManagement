using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure.Generics;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsAndCoursesManagement.UnitTests
{
    public class GradesUnitTests
    {
        // sut
        private readonly Grade _sut;

        public GradesUnitTests()
        {
            _sut = new Grade(10, gradeDate: DateTime.Now, false, true);
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
            var course = new Course("mockTitle", 1, 5);

            // act
             _sut.AttachGradeToCourse(course);

            // assert
            Assert.Equal(course.Id, _sut.CourseId);
        }

        [Fact]
        public void Grade_AttachGradeToStudent_AttachesGradeToStudent()
        {
            // arrange
            var student = new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500);

            // act
            _sut.AttachGradeToStudent(student);

            // assert
            Assert.Equal(student.Id, _sut.StudentId);
        }
    }
}
