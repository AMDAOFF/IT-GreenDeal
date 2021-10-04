using Absence.DataAccess.Entities;
using System.Threading.Tasks;

namespace Absence.DataAccess.Interfaces
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        /// <summary>
        /// Gets a <see cref="Teacher"/> by <paramref name="teacherId"/>
        /// </summary>
        Task<Teacher> GetById(int teacherId);
    }
}