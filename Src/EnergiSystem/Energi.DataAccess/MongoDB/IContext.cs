using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Energi.DataAccess.MongoDB
{
    public interface IContext<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task RemoveAsync(int id);
        Task UpdateAsync(T entity);
    }
}
