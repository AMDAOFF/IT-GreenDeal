using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Absence.DataAccess.Repositories
{
    public class DayScheduleRepository : GenericRepository<DaySchedule>, IDayScheduleRepository
    {
        private readonly AbsenceContext _dbContext;
        public DayScheduleRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }

        public async Task<DaySchedule> GetById(int dayScheduleId)
        {
            return await _dbContext.DaySchedules.SingleAsync(o => o.DayScheduleId == dayScheduleId);
        }
    }
}