using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LabsAndCoursesManagement.Infrastructure.Generics
{
    public abstract class Repository<T>
        : IRepository<T> where T : class
    {
        protected DatabaseContext context;

        protected Repository(DatabaseContext context)
        {
            this.context = context;
        }

        public virtual async Task<T> Add(T entity)
        {
            return (await context
                    .AddAsync(entity))
                .Entity;
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>()
                .AsQueryable()
                .Where(predicate).ToListAsync();
        }

        public virtual async Task<T?> Get(Guid id)
        {
            return await context.FindAsync<T>(id);
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> Update(Guid id, T entity)
        {
            var entityToUpdate = await context.FindAsync<T>(id);
            if (entityToUpdate == null)
            {
                return null;
            }

            context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            return entityToUpdate;
        }

        // delete async
        public virtual async Task<T?> Delete(Guid id)
        {
            var entityToDelete = await context.FindAsync<T>(id);
            if (entityToDelete == null)
            {
                return null;
            }

            context.Set<T>().Remove(entityToDelete);
            await context.SaveChangesAsync();

            return entityToDelete;
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
