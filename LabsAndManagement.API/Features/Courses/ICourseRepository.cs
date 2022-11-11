using LabsAndManagement.Business;

namespace LabsAndManagement.API.Features.Courses
{
    public interface ICourseRepository
    {
        void Add(Course course);
        void Delete(string id);
        void Update(Course course);
    }
}