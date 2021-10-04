using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Absence.DataAccess.Repositories
{
    public class AbsenceReportRepository : GenericRepository<AbsenceReport>, IAbsenceReportRepository
    {
        private readonly AbsenceContext _dbContext;
        public AbsenceReportRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }

        public async Task<AbsenceReport> GetById(string studentId, int? scheduleId)
        {
            return scheduleId.HasValue ? await _dbContext.AbsenceReports.SingleAsync(o => o.FKStudentId == studentId && o.FKScheduleId == scheduleId) : await _dbContext.AbsenceReports.SingleAsync(o => o.FKStudentId == studentId);
        }
    }
}