using Energi.DataAccess.Entity;
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

        public async Task<List<SlimDeviceDTO>> GetAllAsync()
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

        public async Task SeedData()
        {


            try
            {
                IReadOnlyCollection<Device> holder = await _context.GetAllAsync();

                if (holder.Count() == 0 || holder == null)
                {
                    foreach (var device in Energi.DataAccess.SeedingData.Seeding.GetSeedingData())
                    {
                        await _context.CreateAsync(device);
                    }
                }                
            }
            catch (Exception)
            {

                throw;
            }

            return;
        }
    }
}
