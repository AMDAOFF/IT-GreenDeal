using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;

namespace Absence.DataAccess.Repositories
{
    public class HourScheduleRepository : GenericRepository<HourSchedule>, IHourScheduleRepository
    {
        private readonly AbsenceContext _dbContext;
        public HourScheduleRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }
    }
}