using Absence.Service.TeacherService;
using Absence.Service.GenericService;
using System.Threading.Tasks;

namespace Absence.Service.TeacherService
{
    public interface ITeacherService : IGenericService<FullTeacherDTO>
    {
        /// <summary>
        /// Gets a <see cref="FullTeacherDTO"/> by <paramref name="teacherId"/>
        /// </summary>
        Task<FullTeacherDTO> GetById(int teacherId);
    }
}