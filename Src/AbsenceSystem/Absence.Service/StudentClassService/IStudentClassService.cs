using Absence.Service.GenericService;
using Absence.Service.StudentClassService.DataTransferObjects;
using System.Threading.Tasks;

namespace Absence.Service.StudentClassService
{
    public interface IStudentClassService : IGenericService<FullStudentClassDTO>
    {
        /// <summary>
        /// Gets a <see cref="FullStudentClassDTO"/> by <paramref name="studentClassId"/>
        /// </summary>
        Task<FullStudentClassDTO> GetById(int studentClassId);
    }
}
