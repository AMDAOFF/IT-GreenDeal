using Absence.DataAccess.Entities;
using System.Threading.Tasks;

namespace Absence.DataAccess.Interfaces
{
    public interface IHourScheduleRepository : IGenericRepository<HourSchedule>
    {
        /// <summary>
        /// Gets a <see cref="HourSchedule"/> by <paramref name="hourScheduleId"/>
        /// </summary>
        Task<HourSchedule> GetById(int hourScheduleId);
    }
}