using Energi.DataAccess.Entity;
using Energi.DataAccess.Enums;
using Energi.Service.DeviceService;
using Energi.Service.DeviceService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi.Service.HeatingService
{
    public class HeatingService : IHeatingService
    {
        private readonly IDeviceService _deviceService;

        public HeatingService(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public async Task<List<StatusDeviceDTO>> HeadControl(StatusDeviceDTO device)
        {
            
            List<StatusDeviceDTO> deviceChangeList = new List<StatusDeviceDTO>();

            // Find provider.
            if (device.PeopleCount > 0 && device.Temperature < 15)
            {
                deviceChangeList.Add(device);

                StatusDeviceDTO provider = await _deviceService.GetProvider();

                // Consumer has no provider.
                if (provider == default)
                {
                    device.EnvirementStatus = Enum.GetName(typeof(EnvirementStatus), EnvirementStatus.NeedProvider);

                    return deviceChangeList;
                }

                // Consumer has provider.
                deviceChangeList.Add(provider);
                device.EnvirementStatus = Enum.GetName(typeof(EnvirementStatus), EnvirementStatus.Consumer);
                provider.EnvirementStatus = Enum.GetName(typeof(EnvirementStatus), EnvirementStatus.Provider);

                await _deviceService.UpdateDevice(device);
                await _deviceService.UpdateDevice(provider);

                // SEND INSTRUCTION HERE
            }

            // Be provider
            if (device.PeopleCount == 0 && device.Temperature > 15)
            {
                deviceChangeList.Add(device);

                StatusDeviceDTO provider = await _deviceService.GetProvider();

                // Consumer has no provider.
                if (provider == default)
                {
                    device.EnvirementStatus = Enum.GetName(typeof(EnvirementStatus), EnvirementStatus.NeedProvider);

                    return deviceChangeList;
                }

                // Consumer has provider.

                device.EnvirementStatus = Enum.GetName(typeof(EnvirementStatus), EnvirementStatus.Consumer);
                provider.EnvirementStatus = Enum.GetName(typeof(EnvirementStatus), EnvirementStatus.Provider);

                // SEND INSTRUCTION HERE
            }

            // Be provider.

            // Provider har no consumer.

            // Provider has consumer.

            return default; // deviceChangeList;
        }

    }
}
