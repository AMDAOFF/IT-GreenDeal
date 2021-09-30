using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;

namespace Absence.DataAccess.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        private readonly AbsenceContext _dbContext;
        public SubjectRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }
    }
}