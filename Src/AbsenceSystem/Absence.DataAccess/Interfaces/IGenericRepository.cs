using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absence.DataAccess.Interfaces
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T">The class type</typeparam>
    public interface IGenericRepository<T> where T : class
    {

        /// <summary>
        /// Adds a new <paramref name="entity"/> to the database
        /// </summary>
        Task Create(T entity);

        /// <summary>
        /// Updates an existing <paramref name="entity"/>  in the database
        /// </summary>
        Task Update(T entity);

        /// <summary>
        /// Removes an existing <paramref name="entity"/> from the database
        /// </summary>
        Task Delete(T entity);

        /// <summary>
        /// Gets a list of all the <typeparamref name="T"/> entities from the database
        /// </summary>
        Task<List<T>> GetAll();
    }
}
