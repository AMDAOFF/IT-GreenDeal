using Absence.Service.AbsenceReportService.DataTransferObjects;
using Absence.Service.GenericService;
using System.Threading.Tasks;

namespace Absence.Service.AbsenceReportService
{
    public interface IAbsenceReportService : IGenericService<FullAbsenceReportDTO>
    {
        /// <summary>
        /// Gets a <see cref="FullAbsenceReportDTO"/> by <paramref name="studentId"/> and if not null by <paramref name="hourScheduleId"/>
        /// </summary>
        Task<FullAbsenceReportDTO> GetById(string studentId, int? hourScheduleId);
    }
}
