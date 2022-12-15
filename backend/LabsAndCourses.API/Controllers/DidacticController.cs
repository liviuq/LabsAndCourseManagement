using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;
using LabsAndCoursesManagement.API.DTOs;
using LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories;
using Microsoft.AspNetCore.Cors;

namespace LabsAndCoursesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]

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

        [HttpPost("teacher/{teacherId:guid}/course/{courseId:guid}")]
        public async Task<IActionResult> Create(Guid teacherId, Guid courseId)
        {
            var teacher = await teacherRepository.Get(teacherId);
            var course = await courseRepository.Get(courseId);

            if (teacher == null || course == null)
            {
                return NotFound();
            }

            Didactic tempDidactic = new Didactic();
            tempDidactic.AttachDidacticToTeacher(teacher);
            tempDidactic.AttachDidacticToCourse(course);

            await didacticRepository.Add(tempDidactic);
            await didacticRepository.SaveChanges();
            return Created(nameof(Get), tempDidactic);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await didacticRepository.All());
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await didacticRepository.Delete(id);
            await didacticRepository.SaveChanges();
            return Ok("Didactic deleted successfully");
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, Guid teacherId, Guid courseId)
        {
            var tempDidactic = await didacticRepository.Get(id);
            
            if (tempDidactic == null)
            {
                return NotFound();
            }
            
            var teacher = await teacherRepository.Get(teacherId);
            var course = await courseRepository.Get(courseId);

            if (teacher == null || course == null)
            {
                return NotFound();
            }
            
            

            tempDidactic.AttachDidacticToTeacher(teacher);
            tempDidactic.AttachDidacticToCourse(course);
            
            await didacticRepository.Update(tempDidactic.Id, tempDidactic);
            await didacticRepository.SaveChanges();
            return Ok("Didactic updated successfully");
        }
    }
}
