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

    public class TeachersController : ControllerBase
    {
        private readonly IRepository<Teacher> teacherRepository;
        private readonly IMapper mapper;

        public TeachersController(IRepository<Teacher> teacherRepository, IMapper mapper)
        {
            this.teacherRepository = teacherRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateTeacherDto dto)
        {
            var teacher = mapper.Map<Teacher>(dto);
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

            var updatedTeacher = mapper.Map<Teacher>(dto);
            teacher.Update(updatedTeacher);

            teacherRepository.Update(id, teacher);
            teacherRepository.SaveChanges();
            return Ok("Teacher updated succesfully");
        }

    }
}
