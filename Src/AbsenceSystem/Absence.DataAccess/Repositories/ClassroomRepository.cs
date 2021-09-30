using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;

namespace Absence.DataAccess.Repositories
{
    public class ClassroomRepository : GenericRepository<Classroom>, IClassroomRepository
    {
        private readonly AbsenceContext _dbContext;
        public ClassroomRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }
    }
}