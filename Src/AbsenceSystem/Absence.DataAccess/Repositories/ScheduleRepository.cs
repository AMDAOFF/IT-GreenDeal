using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Absence.DataAccess.Repositories
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        private readonly AbsenceContext _dbContext;
        public ScheduleRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }

        public async Task<Schedule> GetById(int scheduleId)
        {
            return await _dbContext.Schedules.SingleAsync(o => o.ScheduleId == scheduleId);
        }

        public async Task<Schedule> GetSchedule(int classroomId, int subjectId, DateTime currentTime)
        {
            return await _dbContext.Schedules
                .Include(o => o.StudentClass)
                .ThenInclude(o => o.Students)
                .Include(o => o.Classroom)
                .ThenInclude(o => o.Camera)
                .AsNoTracking()
                .SingleAsync(o => o.FKClassroomId == classroomId && o.FKSubjectId == subjectId && o.StartTime > currentTime && o.EndTime < currentTime);
        }
    }
}