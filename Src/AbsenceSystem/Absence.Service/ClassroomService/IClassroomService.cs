using Absence.Service.ClassroomService;
using Absence.Service.GenericService;
using System.Threading.Tasks;

namespace Absence.Service.ClassroomService
{
    public interface IClassroomService : IGenericService<FullClassroomDTO>
    {
        /// <summary>
        /// Gets a <see cref="FullClassroomDTO"/> by <paramref name="classroomId"/>
        /// </summary>
        Task<FullClassroomDTO> GetById(int classroomId);
    }
}
