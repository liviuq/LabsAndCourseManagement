using LabsAndCoursesManagement.Domain;
using System.Linq.Expressions;

namespace LabsAndCoursesManagement.Infrastructure.Generics
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(Guid id, T entity);
        T Get(Guid id);
        IEnumerable<T> All();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Delete(Guid id);
        void SaveChanges();
    }
}
