using LabsAndManagement.Business;
using Microsoft.AspNetCore.Mvc;

namespace LabsAndManagement.API.Features.Students
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository repository;

        public StudentsController(IStudentRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        public IActionResult Create([FromBody] StudentDto studentDto)
        {
            var student = Student.Create(studentDto.NumarMatricol,studentDto.Nume, studentDto.Prenume, studentDto.InitialaTata);
            if (student.IsSuccess)
            {
                repository.Add(student.Entity);
                return Created(nameof(GetAllStudents), student);
            }
            return BadRequest(student.Error);
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = repository.GetAll().Select
                (
                    s => new StudentDto
                    {
                        NumarMatricol = s.NumarMatricol,
                        Nume = s.Nume,
                        Prenume = s.Prenume,
                        InitialaTata = s.InitialaTata,
                    }
                );
            return Ok(students);
        }
    }
}
