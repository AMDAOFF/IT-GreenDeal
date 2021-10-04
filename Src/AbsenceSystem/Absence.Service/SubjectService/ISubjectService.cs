using Absence.Service.SubjectService;
using Absence.Service.GenericService;
using System.Threading.Tasks;

namespace Absence.Service.SubjectService
{
    public interface ISubjectService : IGenericService<FullSubjectDTO>
    {
        /// <summary>
        /// Gets a <see cref="FullSubjectDTO"/> by <paramref name="subjectId"/>
        /// </summary>
        Task<FullSubjectDTO> GetById(int subjectId);
    }
}