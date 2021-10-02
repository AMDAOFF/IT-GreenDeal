using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Absence.DataAccess.Repositories
{
    public class ClassroomRepository : GenericRepository<Classroom>, IClassroomRepository
    {
        private readonly AbsenceContext _dbContext;
        public ClassroomRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }

        public async Task<Classroom> GetById(int classroomId)
        {
            return await _dbContext.Classrooms.SingleAsync(o => o.ClassroomId == classroomId);
        }
    }
}