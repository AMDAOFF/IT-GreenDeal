using Absence.Service.StudentService;
using Absence.Service.GenericService;
using System.Threading.Tasks;

namespace Absence.Service.StudentService
{
    public interface IStudentService : IGenericService<FullStudentDTO>
    {
        /// <summary>
        /// Gets a <see cref="FullStudentDTO"/> by <paramref name="studentId"/>
        /// </summary>
        Task<FullStudentDTO> GetById(string studentId);
    }
}