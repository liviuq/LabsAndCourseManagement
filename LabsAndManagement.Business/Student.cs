using LabsAndManagement.Business.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LabsAndManagement.Business
{
    public class Student
    {
        [Key]
        public string NumarMatricol { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public char InitialaTata { get; set; }

        public string CourseId { get; set; }
        public string LabId { get; set; }
        public Course Course { get; set; } = null!;
        public Lab Lab { get; set; } = null!;


        public static Result<Student> Create(string nrMatricol, string nume, string prenume, char initialaTata)
        {

            var student = new Student
            {
                NumarMatricol = nrMatricol,
                Nume = nume,
                Prenume = prenume,
                InitialaTata = initialaTata,
            };

            return Result<Student>.Success(student);
        }
    }
}
