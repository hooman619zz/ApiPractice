using FirstApiProject.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FirstApiProject.Repository
{
    public class BaseRepository<T> :IBaseRepository<T> where T : class
    {
        private CityContext context;
        private DbSet<T> dbSet;
        public BaseRepository(CityContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task InsertAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task Update(T entity)
        {
            dbSet.Update(entity);
            await context.SaveChangesAsync();


        }

        public virtual async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }
    }
}
