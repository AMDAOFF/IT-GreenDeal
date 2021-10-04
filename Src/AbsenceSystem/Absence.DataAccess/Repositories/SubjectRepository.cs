using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Absence.DataAccess.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        private readonly AbsenceContext _dbContext;
        public SubjectRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }

        public async Task<Subject> GetById(int subjectId)
        {
            return await _dbContext.Subjects.SingleAsync(o => o.SubjectId == subjectId);
        }
    }
}