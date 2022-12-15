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
        private readonly IRepository<Grade> gradeRepository;
        private readonly IRepository<Course> courseRepository;
        private readonly IMapper mapper;


        public CoursesController(IRepository<Grade> gradeRepository, IRepository<Course> courseRepository, IMapper mapper)
        {
            this.gradeRepository = gradeRepository;
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCourseDto dto)
        {
            var course = mapper.Map<Course>(dto);
            courseRepository.Add(course);
            courseRepository.SaveChanges();
            return Created(nameof(Get), course);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(courseRepository.All());
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(courseRepository.Get(id));
        }
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            courseRepository.Delete(id);
            courseRepository.SaveChanges();
            return Ok("Course deleted succesfully");
        }
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] CreateCourseDto dto)
        {
            var course = courseRepository.Get(id);
            
            if (course == null)
            {
                return NotFound();
            }
            
            var updatedCourse = mapper.Map<Course>(dto);
            course.Update(updatedCourse);

            courseRepository.Update(id, course);
            courseRepository.SaveChanges();
            return Ok("Course updated succesfully");
        }

    }
}
