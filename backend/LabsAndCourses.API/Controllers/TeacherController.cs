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
        public async Task<IActionResult> Create([FromBody] CreateTeacherDto dto)
        {
            var teacher = mapper.Map<Teacher>(dto);
            await teacherRepository.Add(teacher);
            await teacherRepository.SaveChanges();
            return Created(nameof(Get), teacher);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await teacherRepository.All());
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await teacherRepository.Get(id));
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await teacherRepository.Delete(id);
            await teacherRepository.SaveChanges();
            return Ok("Teacher deleted successfully");
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateTeacherDto dto)
        {
            var teacher = await teacherRepository.Get(id);

            if (teacher == null)
            {
                return NotFound();
            }

            var updatedTeacher = mapper.Map<Teacher>(dto);
            teacher.Update(updatedTeacher);

            await teacherRepository.Update(id, teacher);
            await teacherRepository.SaveChanges();
            return Ok("Teacher updated successfully");
        }

    }
}
