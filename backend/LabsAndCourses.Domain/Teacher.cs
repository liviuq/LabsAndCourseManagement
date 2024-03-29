﻿
using System.Text.RegularExpressions;

namespace LabsAndCoursesManagement.Domain
{
    public class Teacher
    {
        public Teacher( string firstName, string lastName, string email, string teachingDegree)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TeachingDegree = teachingDegree;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string TeachingDegree { get; private set; }

        // update
        public void Update(Teacher updatedTeacher)
        {
            FirstName = updatedTeacher.FirstName;
            LastName = updatedTeacher.LastName;
            Email = updatedTeacher.Email;
            TeachingDegree = updatedTeacher.TeachingDegree;
        }

    }
}
