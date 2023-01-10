using LabsAndCoursesManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories
{
    public class EnrollmentRepository : Repository<Enrollment>
    {
        public EnrollmentRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<List<Student>> GetStudentsForCourse(Guid courseId)
        {
            var enrollments = await context.Enrollments
                                        .Where(d => d.CourseId == courseId)
                                        .Include(d => d.Student)
                                        .ToListAsync();
            return enrollments.Select(d => d.Student).ToList();
        }

        public async Task<List<Course>> GetCoursesForStudent (Guid studentId)
        {
            var enrollments = await context.Enrollments
                                        .Where(d => d.StudentId == studentId)
                                        .Include(d => d.Course)
                                        .ToListAsync();
            return enrollments.Select(d => d.Course).ToList();
        }

        public async Task<Enrollment> DeleteStudentFromCourse(Guid studentId, Guid courseId)
        {
            var enrollment = await context.Enrollments
            .FirstOrDefaultAsync(d => d.StudentId == studentId && d.CourseId == courseId);
            if (enrollment != null)
            {
                context.Enrollments.Remove(enrollment);
                await context.SaveChangesAsync();
                return enrollment;
            }
            return null;
        }

        override
        public async Task<IEnumerable<Enrollment>> All()
        {
            return await context.Enrollments.Include(e => e.Course).Include(e => e.Student).ToListAsync();
        }
    }
}
