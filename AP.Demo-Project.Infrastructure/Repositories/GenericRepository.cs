using AP.Demo_Project.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<IEnumerable<City>> GetAll(int pageNr, int pageSize, string sortBy, string sortOrder, params Expression<Func<City, object>>[] includes)
        {
            IQueryable<City> query = (IQueryable<City>)this.dbSet;

            
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
  
            query = (sortBy?.ToLower(), sortOrder?.ToLower()) switch
            {
                ("population", "asc") => query.OrderBy(c => c.Population),
                ("population", "desc") => query.OrderByDescending(c => c.Population),
                ("name", "asc") => query.OrderBy(c => c.Name),
                ("name", "desc") => query.OrderByDescending(c => c.Name),
                _ => query.OrderBy(c => c.Id)
            };

            query = query.Skip((pageNr - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetCitiesAll()
        {
            return await dbSet.ToListAsync();
        }



        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
