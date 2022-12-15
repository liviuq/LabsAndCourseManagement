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
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
        {
            var student = mapper.Map<Student>(dto);
            await studentRepository.Add(student);
            await studentRepository.SaveChanges();
            return Created(nameof(Get), student);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await studentRepository.All());
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await studentRepository.Get(id));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await studentRepository.Delete(id);
            await studentRepository.SaveChanges();
            
            return Ok("Student deleted successfully");
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateStudentDto dto)
        {
            var student = await studentRepository.Get(id);

            if (student == null)
            {
                return NotFound();
            }
            var updatedStudent = mapper.Map<Student>(dto);
            student.Update(updatedStudent);

            await studentRepository.Update(id, student);
            await studentRepository.SaveChanges();
            
            return Ok("Student updated successfully");
        }
    }
}
