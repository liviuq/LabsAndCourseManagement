﻿using LabsAndCoursesManagement.Domain;
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

    public class GradesController : ControllerBase
    {
        private readonly IRepository<Student> studentRepository;
        private readonly IRepository<Grade> gradeRepository;
        private readonly IRepository<Course> courseRepository;
        private readonly IMapper mapper;

        public GradesController(IRepository<Student> studentRepository, IRepository<Grade> gradeRepository, IRepository<Course> courseRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.gradeRepository = gradeRepository;
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }

        [HttpPost("student/{studentId:guid}/course/{courseId:guid}")]
        public async Task<IActionResult> Create(Guid studentId, Guid courseId,
            [FromBody] CreateGradeDto dto)
        {
            var student = await studentRepository.Get(studentId);
            var course = await courseRepository.Get(courseId);

            if (student == null || course == null)
            {
                return NotFound();
            }

            var tempGrade = mapper.Map<Grade>(dto);

            tempGrade.AttachGradeToCourse(course);
            tempGrade.AttachGradeToStudent(student);

            student.RegisterGradesToStudent(new List<Grade>{tempGrade});

            await gradeRepository.Add(tempGrade);
            await gradeRepository.SaveChanges();
            return Created(nameof(Get), tempGrade);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await gradeRepository.All());
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var grade = await gradeRepository.Get(id);
            if (grade == null)
            {
                return NotFound();
            }
            return Ok(grade);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await gradeRepository.Delete(id);
            await gradeRepository.SaveChanges();
            return Ok("Grade deleted successfully");
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateGradeDto dto)
        {
            var grade = await gradeRepository.Get(id);

            if (grade == null)
            {
                return NotFound();
            }

            var updatedGrade = mapper.Map<Grade>(dto);
            grade.Update(updatedGrade);

            await gradeRepository.Update(id, grade);
            await gradeRepository.SaveChanges();
            return Ok(grade);
        }
    }
}
