using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;

namespace Absence.DataAccess.Repositories
{
    public class DayScheduleRepository : GenericRepository<DaySchedule>, IDayScheduleRepository
    {
        private readonly AbsenceContext _dbContext;
        public DayScheduleRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }
    }
}