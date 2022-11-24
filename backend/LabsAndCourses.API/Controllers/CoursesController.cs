using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;
using LabsAndCoursesManagement.API.DTOs;


namespace LabsAndCoursesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IRepository<Grade> gradeRepository;
        private readonly IRepository<Course> courseRepository;


        public CoursesController(IRepository<Grade> gradeRepository, IRepository<Course> courseRepository)
        {
            this.gradeRepository = gradeRepository;
            this.courseRepository = courseRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCourseDto dto)
        {
            var course = new Course(dto.Title,dto.Semester,dto.Credits);
            courseRepository.Add(course);
            courseRepository.SaveChanges();
            return Created(nameof(Get), course);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(courseRepository.All());
        }

    }
}
