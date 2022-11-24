using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;
using LabsAndCoursesManagement.API.DTOs;


namespace LabsAndCoursesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IRepository<Student> studentRepository;
        private readonly IRepository<Grade> gradeRepository;

        //private readonly ISamuraiRepository samuraiRepository;
        //private readonly IQuoteRepository quoteRepository;

        public StudentsController(IRepository<Student> studentRepository, IRepository<Grade> gradeRepository)
        {
            this.studentRepository = studentRepository;
            this.gradeRepository = gradeRepository;
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

        [HttpPost("{samuraiId:guid}/quotes")]
        public IActionResult RegisterQuotes(Guid SID, 
            [FromBody]List<CreateGradeDto> dtos)
        {
            var student = studentRepository.Get(SID);
            if (student == null)
            {
                return NotFound();
            }

            List<Grade> grades = dtos.Select(d => new Grade(d.Value,d.GradeDate,d.IsLabGrade,d.IsExamGrade)).ToList();

            student.RegisterGradesToStudent(grades);

            grades.ForEach(q=> gradeRepository.Add(q));
            gradeRepository.SaveChanges();
            return NoContent();
        }
    }
}
