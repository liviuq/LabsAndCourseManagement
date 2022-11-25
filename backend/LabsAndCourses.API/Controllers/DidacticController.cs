using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;
using LabsAndCoursesManagement.API.DTOs;
using LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories;


namespace LabsAndCoursesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DidacticController : ControllerBase
    {
        private readonly IRepository<Teacher> teacherRepository;
        private readonly IRepository<Didactic> didacticRepository;
        private readonly IRepository<Course> courseRepository;

        public DidacticController(IRepository<Teacher> teacherRepository, IRepository<Didactic> didacticRepository, IRepository<Course> courseRepository)
        {
            this.teacherRepository = teacherRepository;
            this.didacticRepository = didacticRepository;
            this.courseRepository = courseRepository;
        }

        [HttpPost("{teacherId:guid}/{courseId:guid}")]
        public IActionResult Create(Guid teacherId, Guid courseId)
        {
            var teacher = teacherRepository.Get(teacherId);
            var course = courseRepository.Get(courseId);

            if (teacher == null || course == null)
            {
                return NotFound();
            }

            Didactic tempDidactic = new Didactic();
            tempDidactic.AttachDidacticToTeacher(teacher);
            tempDidactic.AttachDidacticToCourse(course);

            didacticRepository.Add(tempDidactic);
            didacticRepository.SaveChanges();
            return Created(nameof(Get), tempDidactic);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(didacticRepository.All());
        }
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            didacticRepository.Delete(id);
            didacticRepository.SaveChanges();
            return Ok("Didactic deleted succesfully");
        }
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, Guid teacherId, Guid courseId)
        {
            var teacher = teacherRepository.Get(teacherId);
            var course = courseRepository.Get(courseId);

            if (teacher == null || course == null)
            {
                return NotFound();
            }

            Didactic tempDidactic = new Didactic();
            tempDidactic.AttachDidacticToTeacher(teacher);
            tempDidactic.AttachDidacticToCourse(course);
            
            didacticRepository.Update(id, tempDidactic);
            didacticRepository.SaveChanges();
            return Ok("Didactic updated succesfully");
        }
    }
}
