using Absence.Service.SchoolService.DataTransferObjects;
using Absence.Service.GenericService;
using System.Threading.Tasks;

namespace Absence.Service.SchoolService
{
    public interface ISchoolService : IGenericService<FullSchoolDTO>
    {
        /// <summary>
        /// Gets a <see cref="FullSchoolDTO"/> by <paramref name="schoolId"/>
        /// </summary>
        Task<FullSchoolDTO> GetById(int schoolId);
    }
}