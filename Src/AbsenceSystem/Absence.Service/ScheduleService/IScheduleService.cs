using Absence.Service.ScheduleService;
using Absence.Service.GenericService;
using System.Threading.Tasks;
using System;

namespace Absence.Service.ScheduleService
{
    public interface IScheduleService : IGenericService<FullScheduleDTO>
    {
        /// <summary>
        /// Gets a <see cref="FullScheduleDTO"/> by <paramref name="scheduleId"/>
        /// </summary>
        Task<FullScheduleDTO> GetById(int scheduleId);

        /// <summary>
        /// Gets a <see cref="FullScheduleDTO"/> by <paramref name="classroomId"/>, <paramref name="subjectId"/> and <paramref name="currentTime"/>
        /// </summary>
        Task<FullScheduleDTO> GetSchedule(int classroomId, int subjectId, DateTime currentTime);

        /// <summary>
        /// Gets a <see cref="FullScheduleDTO"/> by <paramref name="currentTime"/>
        /// </summary>
        Task<FullScheduleDTO> GetSchedule(DateTime currentTime);
    }
}