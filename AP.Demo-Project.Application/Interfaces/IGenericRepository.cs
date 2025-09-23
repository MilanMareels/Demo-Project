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
        Task<IEnumerable<T>> GetAll(int pageNr, int pageSize, string SortBy, string SortOrder, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetCitiesAll();
    }
}
