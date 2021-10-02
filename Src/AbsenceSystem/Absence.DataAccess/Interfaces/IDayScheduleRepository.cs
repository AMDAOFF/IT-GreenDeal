using Absence.DataAccess.Entities;
using System.Threading.Tasks;

namespace Absence.DataAccess.Interfaces
{
    public interface IDayScheduleRepository : IGenericRepository<DaySchedule>
    {
        /// <summary>
        /// Gets a <see cref="DaySchedule"/> by <paramref name="dayScheduleId"/>
        /// </summary>
        Task<DaySchedule> GetById(int dayScheduleId);
    }
}
