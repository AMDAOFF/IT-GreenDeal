using Absence.DataAccess.Entities;
using System.Threading.Tasks;

namespace Absence.DataAccess.Interfaces
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        /// <summary>
        /// Gets a <see cref="Subject"/> by <paramref name="subjectId"/>
        /// </summary>
        Task<Subject> GetById(int subjectId);
    }
}