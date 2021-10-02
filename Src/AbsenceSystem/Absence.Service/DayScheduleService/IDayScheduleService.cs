using Absence.Service.DayScheduleService.DataTransferObjects;
using Absence.Service.GenericService;
using System.Threading.Tasks;

namespace Absence.Service.DayScheduleService
{
    public interface IDayScheduleService : IGenericService<FullDayScheduleDTO>
    {
        /// <summary>
        /// Gets a <see cref="FullDayScheduleDTO"/> by <paramref name="dayScheduleId"/>
        /// </summary>
        Task<FullDayScheduleDTO> GetById(int dayScheduleId);
    }
}
