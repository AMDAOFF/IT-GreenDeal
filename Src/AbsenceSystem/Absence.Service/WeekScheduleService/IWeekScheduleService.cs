using Absence.Service.WeekScheduleService.DataTransferObjects;
using Absence.Service.GenericService;
using System.Threading.Tasks;

namespace Absence.Service.WeekScheduleService
{
    public interface IWeekScheduleService : IGenericService<FullWeekScheduleDTO>
    {
        /// <summary>
        /// Gets a <see cref="FullWeekScheduleDTO"/> by <paramref name="weekScheduleId"/>
        /// </summary>
        Task<FullWeekScheduleDTO> GetById(int weekScheduleId);
    }
}