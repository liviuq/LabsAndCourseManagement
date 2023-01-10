using LabsAndCoursesManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories
{
    public class DidacticRepository : Repository<Didactic>
    {
        public DidacticRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<List<Teacher>> GetTeachersForCourse(Guid courseId)
        {
            var didactics = await context.Didactics
                                        .Where(d => d.CourseId == courseId)
                                        .Include(d => d.Teacher)
                                        .ToListAsync();
            return didactics.Select(d => d.Teacher).ToList();
        }

        public async Task<List<Course>> GetCoursesForTeacher (Guid teacherId)
        {
            var didactics = await context.Didactics
                                        .Where(d => d.TeacherId == teacherId)
                                        .Include(d => d.Course)
                                        .ToListAsync();
            return didactics.Select(d => d.Course).ToList();
        }

        public async Task<Didactic> DeleteTeacherFromCourse(Guid teacherId, Guid courseId)
        {
            var didactic = await context.Didactics
            .FirstOrDefaultAsync(d => d.TeacherId == teacherId && d.CourseId == courseId);
            if (didactic != null)
            {
                context.Didactics.Remove(didactic);
                await context.SaveChangesAsync();
                return didactic;
            }
            return null;
        }

        override
        public async Task<IEnumerable<Didactic>> All()
        {
            return await context.Didactics.Include(d => d.Course).Include(d => d.Teacher).ToListAsync();
        }
    }
}
