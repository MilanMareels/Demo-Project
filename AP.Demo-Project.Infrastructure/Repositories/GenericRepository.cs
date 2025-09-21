using Microsoft.EntityFrameworkCore;

namespace AP.Demo_Project.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> where T : class
    {
        private readonly DbContext context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll(int pageNr, int pageSize)
        {
            return await dbSet.Skip((pageNr - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
