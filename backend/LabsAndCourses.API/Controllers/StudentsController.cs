using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;
using LabsAndCoursesManagement.API.DTOs;
using LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories;


namespace LabsAndCoursesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IRepository<Student> studentRepository;
        private readonly IRepository<Grade> gradeRepository;
        private readonly IRepository<Course> courseRepository;

        //private readonly ISamuraiRepository samuraiRepository;
        //private readonly IQuoteRepository quoteRepository;

        public StudentsController(IRepository<Student> studentRepository, IRepository<Grade> gradeRepository, IRepository<Course> courseRepository)
        {
            this.studentRepository = studentRepository;
            this.gradeRepository = gradeRepository;
            this.courseRepository = courseRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStudentDto dto)
        {
            var student = new Student(dto.Email,dto.FirstName,dto.LastName,dto.Semester,dto.Group,dto.Scholarship);
            studentRepository.Add(student);
            studentRepository.SaveChanges();
            return Created(nameof(Get), student);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(studentRepository.All());
        }

        [HttpPost("{studentId:guid}/{courseId:guid}/grade")]
        public IActionResult RegisterGrade(Guid studentId, Guid courseId,
            [FromBody]CreateGradeDto dto)
        {
            var student = studentRepository.Get(studentId);
            if (student == null)
            {
                return NotFound();
            }

            var course = courseRepository.Get(courseId);
            if (student == null)
            {
                return NotFound();
            }

            Grade tempGrade = new Grade(dto.Value, dto.GradeDate, dto.IsLabGrade, dto.IsExamGrade);
            tempGrade.AttachGradeToCourse(course);
            tempGrade.AttachGradeToStudent(student);

            gradeRepository.Add(tempGrade);
            

            List<Grade> gradeList = new List<Grade>();
            gradeList.Add(tempGrade);
            student.RegisterGradesToStudent(gradeList);
            
            gradeRepository.SaveChanges();
            return Created(nameof(Get), course);
        }
    }
}
