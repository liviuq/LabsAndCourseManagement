using LabsAndCoursesManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsAndCoursesManagement.UnitTests
{
    public class CourseUnitTests
    {
        // unit tests for Course class
        // sut
        private readonly Course _sut;

        public CourseUnitTests()
        {
            _sut = new Course("mockTitle", 1, 5);

        }

        [Fact]
        public void Course_Update_UpdatesCourse()
        {
            // arrange
            Course sameCourse = new Course("mockTitle", 1, 5);

            // act
            _sut.Update(sameCourse.Title, sameCourse.Semester, sameCourse.Credits);

            // assert
            Assert.NotEqual(Guid.Empty, _sut.Id);
            Assert.Equal(sameCourse.Title, _sut.Title);
            Assert.Equal(sameCourse.Semester, _sut.Semester);
            Assert.Equal(sameCourse.Credits, _sut.Credits);
        }

    }
}
