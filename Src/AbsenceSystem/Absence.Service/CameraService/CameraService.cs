using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.CameraService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;

namespace Absence.Service.CameraService
{
    public class CameraService : GenericService<FullCameraDTO, ICameraRepository, Camera>, ICameraService
    {
        private readonly ICameraRepository _cameraRepository;
        public CameraService(ICameraRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _cameraRepository = GenericRepository;
        }
    }
}