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
        // moq database
        private readonly Mock<IRepository<Grade>> _gradeRepositoryMock = new Mock<IRepository<Grade>>();

        // sut
        private readonly Grade _sut;
        
        public GradesUnitTests()
        {
            // get a grade from mock db
            _sut = _gradeRepositoryMock.Object.All().FirstOrDefault();

            if (_sut == null)
            {
                // if there is no grade in mock db, create a new one
                _sut = new Grade(10, gradeDate: DateTime.Now, false, true);
            }
        }

        [Fact]
        public void Grade_Update_UpdatesGrade()
        {
            // arrange
            int value = 10;
            DateTime gradeDate = DateTime.Now;
            bool isLabGrade = false;
            bool isExamGrade = true;

            // act
            _sut.Update(value, gradeDate, isLabGrade, isExamGrade);

            // assert
            Assert.Equal(value, _sut.Value);
            Assert.Equal(gradeDate, _sut.GradeDate);
            Assert.Equal(isLabGrade, _sut.IsLabGrade);
            Assert.Equal(isExamGrade, _sut.IsExamGrade);
        }
    }
}
