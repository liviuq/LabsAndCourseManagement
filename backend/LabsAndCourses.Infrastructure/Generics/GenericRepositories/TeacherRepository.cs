using LabsAndCoursesManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories
{
    public class TeacherRepository : Repository<Teacher>
    {
        public TeacherRepository(DatabaseContext context) : base(context)
        {
        }

    }
}
