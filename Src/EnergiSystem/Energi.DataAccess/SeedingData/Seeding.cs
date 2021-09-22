using Energi.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Energi.DataAccess;
using Energi.DataAccess.MongoDB;

namespace Energi.DataAccess.SeedingData
{
    public static class Seeding
    {
        public static IReadOnlyCollection<Device> GetSeedingData()
        {
            return new List<Device>()
            {
                new Device() { Id = Guid.NewGuid(), Name = "Device 1", Description = "This is the test device 1", FanOneStatus = false, FanTwoStatus = false, IP = "192.168.0.1", MAC = "xxxx", Classroom = "52.100", OnlineStatus = false, Temperature = 0, VentilationValveStatus = false},
                new Device() { Id = Guid.NewGuid(), Name = "Device 2", Description = "This is the test device 2", FanOneStatus = false, FanTwoStatus = false, IP = "192.168.0.2", MAC = "xxxx", Classroom = "52.101", OnlineStatus = false, Temperature = 0, VentilationValveStatus = false},
                new Device() { Id = Guid.NewGuid(), Name = "Device 3", Description = "This is the test device 3", FanOneStatus = false, FanTwoStatus = false, IP = "192.168.0.3", MAC = "xxxx", Classroom = "52.102", OnlineStatus = false, Temperature = 0, VentilationValveStatus = false},
                new Device() { Id = Guid.NewGuid(), Name = "Device 4", Description = "This is the test device 4", FanOneStatus = false, FanTwoStatus = false, IP = "192.168.0.4", MAC = "xxxx", Classroom = "52.103", OnlineStatus = false, Temperature = 0, VentilationValveStatus = false},
            };
        }
    }
}
