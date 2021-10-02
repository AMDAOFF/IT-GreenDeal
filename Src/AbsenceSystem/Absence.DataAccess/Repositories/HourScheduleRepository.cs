using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Absence.DataAccess.Repositories
{
    public class HourScheduleRepository : GenericRepository<HourSchedule>, IHourScheduleRepository
    {
        private readonly AbsenceContext _dbContext;
        public HourScheduleRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }

        public async Task<HourSchedule> GetById(int hourScheduleId)
        {
            return await _dbContext.HourSchedules.SingleAsync(o => o.HourScheduleId == hourScheduleId);
        }
    }
}