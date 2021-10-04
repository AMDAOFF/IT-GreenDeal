using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Absence.DataAccess.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        private readonly AbsenceContext _dbContext;
        public TeacherRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }
        public async Task<Teacher> GetById(int teacherId)
        {
            return await _dbContext.Teachers.SingleAsync(o => o.TeacherId == teacherId);
        }
    }
}