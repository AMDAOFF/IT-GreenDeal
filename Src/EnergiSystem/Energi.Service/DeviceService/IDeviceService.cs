using Energi.DataAccess.Entity;
using Energi.Service.DeviceService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi.Service.DeviceService
{
    public interface IDeviceService
    {
        Task<List<SlimDeviceDTO>> GetAllDevicesSlimAsync();
        Task<List<StatusDeviceDTO>> GetAllDevicesStatusAsync();
        Task<StatusDeviceDTO> GetDeviceByClassNumber(string ClassNr);
        Task UpdateDevice(StatusDeviceDTO device);
        Task UpdateClasseRoom(ClassInfoDTO classInfo);
        Task SeedData();
        Task<StatusDeviceDTO> GetProvider();
    }
}
