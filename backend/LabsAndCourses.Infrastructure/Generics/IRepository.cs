using LabsAndCoursesManagement.Domain;
using System.Linq.Expressions;

namespace LabsAndCoursesManagement.Infrastructure.Generics
{
    public interface IRepository<T>
    {
        Task<T> Add(T entity);
        Task<T?> Update(Guid id, T entity);
        Task<T?> Get(Guid id);
        Task<IEnumerable<T>> All();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<T?> Delete(Guid id);
        Task SaveChanges();
    }
}
