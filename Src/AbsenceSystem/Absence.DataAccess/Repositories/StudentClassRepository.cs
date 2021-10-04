using Absence.DataAccess.EFCore;
using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absence.DataAccess.Repositories
{
    public class StudentClassRepository : GenericRepository<StudentClass>, IStudentClassRepository
    {
        private readonly AbsenceContext _dbContext;
        public StudentClassRepository(AbsenceContext absenceContext) : base(absenceContext)
        {
            _dbContext = absenceContext;
        }

        public async Task<StudentClass> GetById(int studentClassId)
        {
            return await _dbContext.StudentClasses.SingleAsync(o => o.StudentClassId == studentClassId);
        }
    }
}
