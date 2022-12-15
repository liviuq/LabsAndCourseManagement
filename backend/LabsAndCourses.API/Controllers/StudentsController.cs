using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;
using LabsAndCoursesManagement.API.DTOs;
using LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories;
using Microsoft.AspNetCore.Cors;
using AutoMapper;

namespace LabsAndCoursesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class StudentsController : ControllerBase
    {
        private readonly IRepository<Student> studentRepository;
        private readonly IMapper mapper;

        public StudentsController(IRepository<Student> studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper; 
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateStudentDto dto)
        {
            var student = mapper.Map<Student>(dto);
            studentRepository.Add(student);
            studentRepository.SaveChanges();
            return Created(nameof(Get), student);
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(studentRepository.All());
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(studentRepository.Get(id));
        }
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            studentRepository.Delete(id);
            studentRepository.SaveChanges();
            return Ok("Student deleted succesfully");
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] CreateStudentDto dto)
        {
            var student = studentRepository.Get(id);

            if (student == null)
            {
                return NotFound();
            }
            var updatedStudent = mapper.Map<Student>(dto);
            student.Update(updatedStudent);

            studentRepository.Update(id, student);
            studentRepository.SaveChanges();
            return Ok("Student updated succesfully");
        }

    }
}
