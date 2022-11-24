using LabsAndCoursesManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories
{
    public class StudentRepository : Repository<Student>
    {
        public StudentRepository(DatabaseContext context) : base(context)
        {

        }

        override
        public Student Get(Guid id)
        {
            return context.Students.Include(student => student.Grades).FirstOrDefault(x => x.Id == id);
        }

        override
        public IEnumerable<Student> All()
        {
            return context.Students.Include(student => student.Grades).ToList();
        }


    }
}
