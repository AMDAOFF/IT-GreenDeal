using Absence.DataAccess.Entities;
using System.Threading.Tasks;

namespace Absence.DataAccess.Interfaces
{
    public interface ISchoolRepository : IGenericRepository<School>
    {
        /// <summary>
        /// Gets a <see cref="School"/> by <paramref name="schoolId"/>
        /// </summary>
        Task<School> GetById(int schoolId);
    }
}