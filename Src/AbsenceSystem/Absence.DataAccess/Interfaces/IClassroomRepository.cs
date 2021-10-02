using Absence.DataAccess.Entities;
using System.Threading.Tasks;

namespace Absence.DataAccess.Interfaces
{
    public interface IClassroomRepository : IGenericRepository<Classroom>
    {
        /// <summary>
        /// Gets a <see cref="Classroom"/> by <paramref name="classroomId"/>
        /// </summary>
        Task<Classroom> GetById(int classroomId);

    }
}
