using Energi.DataAccess.Entity;
using Energi.DataAccess.Enums;
using Energi.Service.DeviceService;
using Energi.Service.DeviceService.DTO;
using Energi.Service.MQTTService;
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

        public async Task<List<StatusDeviceDTO>> HeadControl(StatusDeviceDTO device, IMqttService mqttService)
        {
            
            List<StatusDeviceDTO> deviceChangeList = new List<StatusDeviceDTO>();

            // Find provider.
            if (device.PeopleCount > 0 && device.Temperature < (double)30.0)
            {
                // Already consumer.
                if (device.EnvirementStatus == Enum.GetName(typeof(EnvirementStatus), EnvirementStatus.Consumer))
                {
                    return deviceChangeList;
                }

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
                device.VentilationValveStatus = true;
                device.RecyclingFan = true;
                device.HeatingStatus = true;
                device.RecyclingStatus = true;
                device.VentilationFan = false;                

                deviceChangeList.Add(provider);                
                provider.EnvirementStatus = Enum.GetName(typeof(EnvirementStatus), EnvirementStatus.Provider);
                provider.VentilationFan = true;
                provider.RecyclingFan = true;
                provider.RecyclingStatus = true;
                provider.VentilationValveStatus = false;
                device.HeatingStatus = false;

                await _deviceService.UpdateDevice(device);
                await _deviceService.UpdateDevice(provider);

                // Device config.                
                ConfigMessage config = new ConfigMessage();

                // Consumer.
                config.Id = device.Id;
                config.VentilationValveStatus = device.VentilationValveStatus;
                config.RecyclingFan = device.RecyclingFan;
                config.Radiator = device.Radiator;
                config.VentilationFan = device.VentilationFan;
                await mqttService.SendConfig(config);

                // Provider.
                config.Id = provider.Id;
                config.VentilationValveStatus = provider.VentilationValveStatus;
                config.RecyclingFan = provider.RecyclingFan;
                config.Radiator = device.Radiator;
                config.VentilationFan = provider.VentilationFan;
                await mqttService.SendConfig(config);

                return deviceChangeList;
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

            return default;
        }

        public async Task SendLastConfig(IMqttService mqttService, int id)
        {
            StatusDeviceDTO device = await _deviceService.GetDeviceById(id);

            ConfigMessage config = new ConfigMessage();

            // Consumer.
            config.Id = device.Id;
            config.VentilationValveStatus = device.VentilationValveStatus;
            config.RecyclingFan = device.RecyclingFan;
            config.Radiator = device.Radiator;
            config.VentilationFan = device.VentilationFan;
            await mqttService.SendConfig(config);
        }

    }
}
