using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;

namespace Absence.DataAccess.Repositories
{
    public class WeekScheduleRepository : GenericRepository<WeekSchedule>, IWeekScheduleRepository
    {
        private readonly AbsenceContext _dbContext;
        public WeekScheduleRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }
    }
}