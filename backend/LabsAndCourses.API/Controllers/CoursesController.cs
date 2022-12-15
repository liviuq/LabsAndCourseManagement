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
    public class CoursesController : ControllerBase
    {
        private readonly IRepository<Course> courseRepository;
        private readonly IMapper mapper;

        public CoursesController(IRepository<Course> courseRepository, IMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
        {
            var course = mapper.Map<Course>(dto);
            await courseRepository.Add(course);
            await courseRepository.SaveChanges();
            return Created(nameof(Get), course);
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await courseRepository.All());
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await courseRepository.Get(id));
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await courseRepository.Delete(id);
            await courseRepository.SaveChanges();
            return Ok("Course deleted successfully");
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateCourseDto dto)
        {
            var course = await courseRepository.Get(id);
            
            if (course == null)
            {
                return NotFound();
            }
            
            var updatedCourse = mapper.Map<Course>(dto);
            course.Update(updatedCourse);

            await courseRepository.Update(id, course);
            await courseRepository.SaveChanges();
            return Ok("Course updated successfully");
        }

    }
}
