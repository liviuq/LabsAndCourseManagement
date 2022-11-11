using LabsAndManagement.API.Data;
using LabsAndManagement.Business;
using System;

namespace LabsAndManagement.API.Features.Students
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DatabaseContext context;

        public StudentRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public void Add(Student student)
        {
            this.context.Students.Add(student);
            this.context.SaveChanges();
        }

        public void Update(Student student)
        {
            this.context.Students.Update(student);
            this.context.SaveChanges();
        }

        public void Delete(string NrMatricol)
        {
            var student = this.context.Students.FirstOrDefault(s => s.NumarMatricol == NrMatricol);
            if (student == null)
            {
                return;
            }
            this.context.Students.Remove(student);
            this.context.SaveChanges();
        }

        public IEnumerable<Student> GetAll()
        {
            return this.context.Students.ToList();
        }
    }
}
