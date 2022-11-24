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
        public IEnumerable<Student> All()
        {
            return context.Students.Include(student => student.Grades)
                .ToList();
        }
    }
}
