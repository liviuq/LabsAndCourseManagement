using LabsAndCoursesManagement.Domain;

namespace LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories
{
    public class DidacticRepository : Repository<Didactic>
    {
        public DidacticRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
