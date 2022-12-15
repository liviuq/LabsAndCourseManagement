
namespace LabsAndCoursesManagement.Domain
{
    public class Course
    {
        public Course( string title, int semester, int credits)
        {
            Id = Guid.NewGuid();
            Title = title;
            Semester = semester;
            Credits = credits;
        }

    
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public int Semester { get; private set; }
        public int Credits { get; private set; }

        // update
        public void Update(Course updatedCourse)
        {
            Title = updatedCourse.Title;
            Semester = updatedCourse.Semester;
            Credits = updatedCourse.Credits;
        }

    }
}
