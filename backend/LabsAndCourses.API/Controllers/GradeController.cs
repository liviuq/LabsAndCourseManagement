using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;
using LabsAndCoursesManagement.API.DTOs;
using LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories;


namespace LabsAndCoursesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IRepository<Student> studentRepository;
        private readonly IRepository<Grade> gradeRepository;
        private readonly IRepository<Course> courseRepository;

        public GradesController(IRepository<Student> studentRepository, IRepository<Grade> gradeRepository, IRepository<Course> courseRepository)
        {
            this.studentRepository = studentRepository;
            this.gradeRepository = gradeRepository;
            this.courseRepository = courseRepository;
        }

        [HttpPost("{studentId:guid}/{courseId:guid}")]
        public IActionResult Create(Guid studentId, Guid courseId,
            [FromBody] CreateGradeDto dto)
        {
            var student = studentRepository.Get(studentId);
            var course = courseRepository.Get(courseId);

            if (student == null || course == null)
            {
                return NotFound();
            }

            Grade tempGrade = new Grade(dto.Value, dto.GradeDate, dto.IsLabGrade, dto.IsExamGrade);
            tempGrade.AttachGradeToCourse(course);
            tempGrade.AttachGradeToStudent(student);

            student.RegisterGradesToStudent(new List<Grade>{tempGrade});

            gradeRepository.Add(tempGrade);
            gradeRepository.SaveChanges();
            return Created(nameof(Get), tempGrade);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(gradeRepository.All());
        }
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            gradeRepository.Delete(id);
            gradeRepository.SaveChanges();
            return Ok("Grade deleted succesfully");
        }
    }
}
