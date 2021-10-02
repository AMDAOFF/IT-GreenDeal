using Absence.DataAccess.Entities;
using System.Threading.Tasks;

namespace Absence.DataAccess.Interfaces
{
    public interface IWeekScheduleRepository : IGenericRepository<WeekSchedule>
    {
        /// <summary>
        /// Gets a <see cref="WeekSchedule"/> by <paramref name="weekScheduleId"/>
        /// </summary>
        Task<WeekSchedule> GetById(int weekScheduleId);
    }
}