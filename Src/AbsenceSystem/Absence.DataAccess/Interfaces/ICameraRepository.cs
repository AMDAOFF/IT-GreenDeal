using Absence.DataAccess.Entities;
using System.Threading.Tasks;

namespace Absence.DataAccess.Interfaces
{
    public interface ICameraRepository : IGenericRepository<Camera>
    {
        /// <summary>
        /// Gets a <see cref="Camera"/> by <paramref name="IP"/>
        /// </summary>
        Task<Camera> GetByIP(string IP);
    }
}
