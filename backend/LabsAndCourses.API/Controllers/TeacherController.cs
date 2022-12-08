using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;
using LabsAndCoursesManagement.API.DTOs;
using LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories;


namespace LabsAndCoursesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IRepository<Teacher> teacherRepository;

        public TeachersController(IRepository<Teacher> teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateTeacherDto dto)
        {
            var teacher = new Teacher(dto.FirstName, dto.LastName, dto.Email, dto.TeachingDegree);
            teacherRepository.Add(teacher);
            teacherRepository.SaveChanges();
            return Created(nameof(Get), teacher);
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(teacherRepository.All());
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(teacherRepository.Get(id));
        }
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            teacherRepository.Delete(id);
            teacherRepository.SaveChanges();
            return Ok("Teacher deleted succesfully");
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] CreateTeacherDto dto)
        {
            var teacher = teacherRepository.Get(id);

            if (teacher == null)
            {
                return NotFound();
            }

            teacher.Update(dto.FirstName, dto.LastName, dto.Email, dto.TeachingDegree);

            teacherRepository.Update(id, teacher);
            teacherRepository.SaveChanges();
            return Ok("Teacher updated succesfully");
        }

    }
}
