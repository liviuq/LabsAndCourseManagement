using LabsAndCoursesManagement.Domain;
using System.Diagnostics;

namespace LabsAndCoursesManagement.NUnit
{
    public class Tests
    {
        [Test]
        public void Test_Grade_Constructor()
        {
            // Arrange
            var value = 80;
            var gradeDate = new DateTime(2022, 1, 1);
            var isLabGrade = true;
            var isExamGrade = false;

            // Act
            var grade = new Grade(value, gradeDate, isLabGrade, isExamGrade);

            // Assert
            Assert.AreEqual(value, grade.Value);
            Assert.AreEqual(gradeDate, grade.GradeDate);
            Assert.AreEqual(isLabGrade, grade.IsLabGrade);
            Assert.AreEqual(isExamGrade, grade.IsExamGrade);
        }

        [Test]
        public void Test_AttachGradeToCourse()
        {
            // Arrange
            var course = new Course("Math", 2, 5);
            var grade = new Grade(80, new DateTime(2022, 1, 1), true, false);

            // Act
            grade.AttachGradeToCourse(course);

            // Assert
            Assert.AreEqual(course.Id, grade.CourseId);
        }

        [Test]
        public void Test_AttachGradeToStudent()
        {
            // Arrange
            var student = new Student("mockEmail", "mockFirstName", "mockLastName", 2, "2B4", 500);
            var grade = new Grade(80, new DateTime(2022, 1, 1), true, false);

            // Act
            grade.AttachGradeToStudent(student);

            // Assert
            Assert.AreEqual(student.Id, grade.StudentId);
        }

        [Test]
        public void Test_Update()
        {
            // Arrange
            var grade = new Grade(80, new DateTime(2022, 1, 1), true, false);

            // Act
            grade.Update(85, new DateTime(2022, 2, 1), false, true);

            // Assert
            Assert.AreEqual(85, grade.Value);
            Assert.AreEqual(new DateTime(2022, 2, 1), grade.GradeDate);
            Assert.AreEqual(false, grade.IsLabGrade);
            Assert.AreEqual(true, grade.IsExamGrade);
        }
    }
}