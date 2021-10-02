using Absence.Service.CameraService.DataTransferObjects;
using Absence.Service.GenericService;
using System.Threading.Tasks;

namespace Absence.Service.CameraService
{
    public interface ICameraService : IGenericService<FullCameraDTO>
    {
        /// <summary>
        /// Gets a <see cref="FullCameraDTO"/> by <paramref name="IP"/>
        /// </summary>
        Task<FullCameraDTO> GetByIP(string IP);
    }
}