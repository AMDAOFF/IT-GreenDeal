using Absence.DataAccess.Entities;
using System.Threading.Tasks;

namespace Absence.DataAccess.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        /// <summary>
        /// Gets a <see cref="Student"/> by <paramref name="studentId"/>
        /// </summary>
        Task<Student> GetById(string studentId);
    }
}