using Absence.Service.HourScheduleService.DataTransferObjects;
using Absence.Service.GenericService;
using System.Threading.Tasks;

namespace Absence.Service.HourScheduleService
{
    public interface IHourScheduleService : IGenericService<FullHourScheduleDTO>
    {
        /// <summary>
        /// Gets a <see cref="FullHourScheduleDTO"/> by <paramref name="hourScheduleId"/>
        /// </summary>
        Task<FullHourScheduleDTO> GetById(int hourScheduleId);
    }
}