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

        public Task<AbsenceReport> GetById(string studentId, int? hourScheduleId)
        {
            throw new System.NotImplementedException();
        }
    }
}