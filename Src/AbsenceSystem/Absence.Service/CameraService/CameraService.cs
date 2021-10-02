using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.CameraService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;
using System.Threading.Tasks;
using System;

namespace Absence.Service.CameraService
{
    public class CameraService : GenericService<FullCameraDTO, ICameraRepository, Camera>, ICameraService
    {
        private readonly ICameraRepository _cameraRepository;
        private readonly MappingService _mappingService;
        public CameraService(ICameraRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _cameraRepository = GenericRepository;
            _mappingService = mappingService;
        }

        public async Task<FullCameraDTO> GetByIP(string IP)
        {
            if (string.IsNullOrWhiteSpace(IP))
            {
                LogWarning($"IP was null, so the operation was skipped");
                return null;
            }
            try
            {
                FullCameraDTO camera = _mappingService._mapper.Map<FullCameraDTO>(await _cameraRepository.GetByIP(IP));
                LogInformation($"Successfully fetched the Camera with the IP: " + IP);
                return camera;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the Camera with the Id: " + IP, e);
                return null;
            }
        }
    }
}