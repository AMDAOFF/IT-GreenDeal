using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;

namespace Absence.DataAccess.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly AbsenceContext _dbContext;
        public StudentRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }
    }
}