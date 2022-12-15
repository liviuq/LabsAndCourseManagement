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
        public async Task<Student?> Get(Guid id)
        {
            return await context.Students.Include(student => student.Grades).FirstOrDefaultAsync(x => x.Id == id);
        }

        override
        public async Task<IEnumerable<Student>> All()
        {
            return await context.Students.Include(student => student.Grades).ToListAsync();
        }
    }
}
