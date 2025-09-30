using AP.Demo_Project.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Application.Interfaces
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id, params Expression<Func<City, object>>[] includes);
    }
}
