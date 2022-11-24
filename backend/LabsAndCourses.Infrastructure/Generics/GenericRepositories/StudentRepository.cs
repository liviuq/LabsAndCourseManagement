using LabsAndCoursesManagement.Domain;

namespace LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories
{
    public class StudentRepository : Repository<Student>
    {
        public StudentRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
