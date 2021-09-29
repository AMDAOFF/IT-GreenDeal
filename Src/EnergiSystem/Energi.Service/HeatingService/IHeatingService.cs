using Energi.Service.DeviceService.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Energi.Service.HeatingService
{
    public interface IHeatingService
    {
        Task<List<StatusDeviceDTO>> HeadControl(StatusDeviceDTO device);
    }
}