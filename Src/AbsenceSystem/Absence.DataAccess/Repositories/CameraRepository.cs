using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;

namespace Absence.DataAccess.Repositories
{
    public class CameraRepository : GenericRepository<Camera>, ICameraRepository
    {
        private readonly AbsenceContext _dbContext;
        public CameraRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }
    }
}