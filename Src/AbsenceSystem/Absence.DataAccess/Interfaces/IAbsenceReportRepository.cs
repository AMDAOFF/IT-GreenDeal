using Absence.DataAccess.Entities;
using System.Threading.Tasks;

namespace Absence.DataAccess.Interfaces
{
    public interface IAbsenceReportRepository : IGenericRepository<AbsenceReport>
    {
        /// <summary>
        /// Gets a <see cref="AbsenceReport"/> by <paramref name="studentId"/> and if not null by <paramref name="hourScheduleId"/>
        /// </summary>
        Task<AbsenceReport> GetById(string studentId, int? hourScheduleId);
    }
}
