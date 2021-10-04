using Absence.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace Absence.DataAccess.Interfaces
{
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        /// <summary>
        /// Gets a <see cref="Schedule"/> by <paramref name="scheduleId"/>
        /// </summary>
        Task<Schedule> GetById(int scheduleId);

        /// <summary>
        /// Gets a <see cref="Schedule"/> by <paramref name="classroomId"/>, <paramref name="subjectId"/> and <paramref name="currentTime"/>
        /// </summary>
        Task<Schedule> GetSchedule(int classroomId, int subjectId, DateTime currentTime);

        /// <summary>
        /// Gets a <see cref="Schedule"/> by <paramref name="currentTime"/>
        /// </summary>
        Task<Schedule> GetSchedule(DateTime currentTime);

    }
}