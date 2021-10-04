using Absence.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absence.DataAccess.Interfaces
{
    public interface IStudentClassRepository : IGenericRepository<StudentClass>
    {
        /// <summary>
        /// Gets a <see cref="StudentClass"/> by <paramref name="studentClassId"/>
        /// </summary>
        Task<StudentClass> GetById(int studentClassId);

    }
}
