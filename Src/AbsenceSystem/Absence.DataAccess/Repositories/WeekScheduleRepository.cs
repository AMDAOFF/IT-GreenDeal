using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Absence.DataAccess.Repositories
{
    public class WeekScheduleRepository : GenericRepository<WeekSchedule>, IWeekScheduleRepository
    {
        private readonly AbsenceContext _dbContext;
        public WeekScheduleRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }

        public async Task<WeekSchedule> GetById(int weekScheduleId)
        {
            return await _dbContext.WeekSchedules.SingleAsync(o => o.WeekScheduleId == weekScheduleId);
        }
    }
}