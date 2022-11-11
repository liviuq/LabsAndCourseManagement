using LabsAndManagement.API.Data;
using LabsAndManagement.Business;

namespace LabsAndManagement.API.Features.Courses
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DatabaseContext context;

        public CourseRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void Add(Course course)
        {
            this.context.Courses.Add(course);
            this.context.SaveChanges();
        }

        public void Update(Course course)
        {
            this.context.Courses.Update(course);
            this.context.SaveChanges();
        }

        public void Delete(string id)
        {
            var course = this.context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return;
            }
            this.context.Courses.Remove(course);
            this.context.SaveChanges();
        }
    }
}
