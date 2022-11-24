using LabsAndCoursesManagement.Domain;

namespace LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories
{
    public class CourseRepository : Repository<Course>
    {
        public CourseRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
