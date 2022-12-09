﻿using LabsAndCoursesManagement.Domain;
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
    public class StudentsController : ControllerBase
    {
        private readonly IRepository<Student> studentRepository;

        public StudentsController(IRepository<Student> studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateStudentDto dto)
        {
            var student = new Student(dto.Email,dto.FirstName,dto.LastName,dto.Semester,dto.Group,dto.Scholarship);
            studentRepository.Add(student);
            studentRepository.SaveChanges();
            return Created(nameof(Get), student);
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(studentRepository.All());
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(studentRepository.Get(id));
        }
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            studentRepository.Delete(id);
            studentRepository.SaveChanges();
            return Ok("Student deleted succesfully");
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] CreateStudentDto dto)
        {
            var student = studentRepository.Get(id);

            if (student == null)
            {
                return NotFound();
            }

            student.Update(dto.Email, dto.FirstName, dto.LastName, dto.Semester, dto.Group, dto.Scholarship);

            studentRepository.Update(id, student);
            studentRepository.SaveChanges();
            return Ok("Student updated succesfully");
        }

    }
}
