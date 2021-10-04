using Energi.DataAccess.Entity;
using Energi.DataAccess.Enums;
using Energi.DataAccess.MongoDB;
using Energi.Service.DeviceService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi.Service.DeviceService
{
    public class DeviceService : IDeviceService
    {
        private readonly DataAccess.MongoDB.IContext<Device> _context;

        public DeviceService(IContext<Device> context)
        {
            _context = context;
        }

        public async Task<StatusDeviceDTO> GetDeviceById(int Id)
        {
            Device device = await _context.GetAsync(Id);

            return DeviceAsStatusDevice(device);
        }

        public async Task<StatusDeviceDTO> GetDeviceByClassNumber(string ClassNr)
        {
            Device device = await _context.GetAsync(x => x.Classroom == ClassNr);

            StatusDeviceDTO deviceStatus = DeviceAsStatusDevice(device);

            return deviceStatus;
        }

        public async Task UpdateDevice(StatusDeviceDTO device)
        {            
            await _context.UpdateAsync(StatusDeviceAsDevice(device));
        }

        public async Task<StatusDeviceDTO> GetProvider()
        {
            Device device = await _context.GetAsync(x => x.EnvirementStatus == EnvirementStatus.NeedConsumer);

            if (device == null)
            {
                return default;
            }

            return DeviceAsStatusDevice(device);
        }

        public async Task UpdateClasseRoom(ClassInfoDTO classroomInfo)
        {
            Device device = await _context.GetAsync(classroomInfo.Id);

            device.PeopleCount = classroomInfo.PeopleCount;
            device.TimeStamp = classroomInfo.TimeStamp;

            await _context.UpdateAsync(device);
        }

        public async Task<List<StatusDeviceDTO>> GetAllDevicesStatusAsync()
        {
            IReadOnlyCollection<Device> deviceList;
            List<StatusDeviceDTO> deviceDtoList = new List<StatusDeviceDTO>();

            try
            {
                deviceList = await _context.GetAllAsync();

                foreach (var device in deviceList)
                {
                    deviceDtoList.Add(DeviceAsStatusDevice(device));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return deviceDtoList;
        }

        public async Task<List<SlimDeviceDTO>> GetAllDevicesSlimAsync()
        {
            IReadOnlyCollection<Device> deviceList;
            List<SlimDeviceDTO> deviceDtoList = new List<SlimDeviceDTO>();

            try
            {
                deviceList = await _context.GetAllAsync();

                foreach (var device in deviceList)
                {
                    deviceDtoList.Add(new SlimDeviceDTO() { Id = device.Id, Name = device.Name });
                }
            }
            catch (Exception)
            {
                throw;
            }

            return deviceDtoList;
        }
        

        public static StatusDeviceDTO DeviceAsStatusDevice(Device device)
        {
            if (device == null)
            {
                return default;
            }

            return new StatusDeviceDTO()
            {
                Id = device.Id,
                Name = device.Name,
                Classroom = device.Classroom,
                TimeStamp = device.TimeStamp,
                DeviceType = device.DeviceType,
                PeopleCount = device.PeopleCount,
                RecyclingFan = device.RecyclingFan,
                VentilationFan = device.VentilationFan,
                OnlineStatus = device.OnlineStatus,
                Temperature = device.Temperature,
                Radiator = device.Radiator,
                HeatingStatus = device.EnvirementStatus == EnvirementStatus.Consumer ? true : false,
                VentilationValveStatus = device.VentilationValveStatus,
                EnvirementStatus = Enum.GetName(typeof(EnvirementStatus), device.EnvirementStatus)
            };            
        }

        public Device StatusDeviceAsDevice(StatusDeviceDTO device)
        {
            if (device == null)
            {
                return default;
            }

            return new Device()
            {
                Id = device.Id,
                Name = device.Name,
                Classroom = device.Classroom,
                TimeStamp = device.TimeStamp,
                DeviceType = device.DeviceType,
                PeopleCount = device.PeopleCount,
                RecyclingFan = device.RecyclingFan,
                VentilationFan = device.VentilationFan,
                OnlineStatus = device.OnlineStatus,
                Temperature = device.Temperature,
                Radiator = device.Radiator,
                VentilationValveStatus = device.VentilationValveStatus,
                EnvirementStatus = (EnvirementStatus)Enum.Parse(typeof(EnvirementStatus), device.EnvirementStatus)
            };
        }
    }
}
