using AP.Demo_Project.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public IQueryable<T> GetAll()
        {
            return dbSet.AsQueryable();
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<City> GetByIdAsync(int id, params Expression<Func<City, object>>[] includes)
        {
            IQueryable<City> query = (IQueryable<City>)dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
