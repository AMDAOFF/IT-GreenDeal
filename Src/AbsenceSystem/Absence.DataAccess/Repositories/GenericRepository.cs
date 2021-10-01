using Absence.DataAccess.EFCore;
using Absence.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absence.DataAccess.Repositories
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="T">The class type</typeparam>
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AbsenceContext _dbContext;
        public GenericRepository(AbsenceContext absenceContext)
        {
            _dbContext = absenceContext;
        }

        /// <summary>
        /// Adds a new <paramref name="entity"/> to the database
        /// </summary>
        public async Task Create(T entity)
        {

            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing <paramref name="entity"/>  in the database
        /// </summary>
        public async Task Update(T entity)
        {

            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes an existing <paramref name="entity"/> from the database
        /// </summary>
        public async Task Delete(T entity)
        {

            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Gets a list of all the <typeparamref name="T"/> entities from the database
        /// </summary>
        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }
    }
}
