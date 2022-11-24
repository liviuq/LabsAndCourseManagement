using LabsAndCoursesManagement.Domain;

namespace LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories
{
    public class GradeRepository : Repository<Grade>
    {
        public GradeRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
