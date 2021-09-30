using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;

namespace Absence.DataAccess.Repositories
{
    public class SchoolRepository : GenericRepository<School>, ISchoolRepository
    {
        private readonly AbsenceContext _dbContext;
        public SchoolRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }
    }
}