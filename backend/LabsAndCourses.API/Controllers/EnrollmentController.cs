using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;
using LabsAndCoursesManagement.API.DTOs;
using LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

namespace LabsAndCoursesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]

    public class EnrollmentController : ControllerBase
    {
        private readonly IRepository<Student> studentRepository;
        private readonly EnrollmentRepository enrollmentRepository;
        private readonly IRepository<Course> courseRepository;

        public EnrollmentController(IRepository<Student> studentRepository, EnrollmentRepository enrollmentRepository, IRepository<Course> courseRepository)
        {
            this.studentRepository = studentRepository;
            this.enrollmentRepository = enrollmentRepository;
            this.courseRepository = courseRepository;
        }

        [HttpPost("student/{studentId:guid}/course/{courseId:guid}")]
        public async Task<IActionResult> Create(Guid studentId, Guid courseId)
        {
            var student = await studentRepository.Get(studentId);
            var course = await courseRepository.Get(courseId);

            if (student == null || course == null)
            {
                return NotFound();
            }

            Enrollment tempEnrollment = new Enrollment();
            tempEnrollment.AttachEnrollmentToStudent(student);
            tempEnrollment.AttachEnrollmentToCourse(course);

            await enrollmentRepository.Add(tempEnrollment);
            await enrollmentRepository.SaveChanges();
            return Created(nameof(Get), tempEnrollment);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await enrollmentRepository.All());
        }

        [HttpGet("course/{courseId}/students")]
        public async Task<ActionResult<List<Student>>> GetStudentsForCourse(Guid courseId)
        {
            var students = await enrollmentRepository.GetStudentsForCourse(courseId);
            if (students == null)
            {
                return NotFound();
            }

            return Ok(students);
        }

        [HttpGet("student/{studentId}/courses")]
        public async Task<ActionResult<List<Course>>> GetCoursesForStudent(Guid studentId)
        {
            var courses = await enrollmentRepository.GetCoursesForStudent(studentId);
            if (courses == null)
            {
                return NotFound();
            }

            return Ok(courses);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await enrollmentRepository.Delete(id);
            await enrollmentRepository.SaveChanges();
            return Ok("Enrollment deleted successfully");
        }

        [HttpDelete("{studentId}/{courseId}")]
        public async Task<IActionResult> DeleteStudentFromCourse(Guid studentId, Guid courseId)
        {
            var enrollment = await enrollmentRepository.DeleteStudentFromCourse(studentId, courseId);
            if (enrollment == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, Guid studentId, Guid courseId)
        {
            var tempEnrollment = await enrollmentRepository.Get(id);
            
            if (tempEnrollment == null)
            {
                return NotFound();
            }
            
            var student = await studentRepository.Get(studentId);
            var course = await courseRepository.Get(courseId);

            if (student == null || course == null)
            {
                return NotFound();
            }



            tempEnrollment.AttachEnrollmentToStudent(student);
            tempEnrollment.AttachEnrollmentToCourse(course);
            
            await enrollmentRepository.Update(tempEnrollment.Id, tempEnrollment);
            await enrollmentRepository.SaveChanges();
            return Ok("Enrollment updated successfully");
        }
    }
}
