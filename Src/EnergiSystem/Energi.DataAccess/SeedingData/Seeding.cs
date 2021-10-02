using Energi.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Energi.DataAccess;
using Energi.DataAccess.MongoDB;
using Energi.DataAccess.Enums;

namespace Energi.DataAccess.SeedingData
{
    public static class Seeding
    {
        public static IReadOnlyCollection<Device> GetSeedingData()
        {
            return new List<Device>()
            {
                new Device() { Id = 1, Name = "Device 1", DeviceType = "Classroom device", PeopleCount = 0, Description = "This is the test device 1", RecyclingFan = true, VentilationFan = false, IP = "192.168.0.1", MAC = "xxxx", Classroom = "52.100", TimeStamp = DateTime.Now, OnlineStatus = false, Temperature = 0, Radiator = false, VentilationValveStatus = false, EnvirementStatus = EnvirementStatus.Idle},
                new Device() { Id = 2, Name = "Device 2", DeviceType = "Classroom device", PeopleCount = 0, Description = "This is the test device 2", RecyclingFan = true, VentilationFan = false, IP = "192.168.0.2", MAC = "xxxx", Classroom = "52.101", TimeStamp = DateTime.Now,  OnlineStatus = false, Temperature = 0, Radiator = false, VentilationValveStatus = false, EnvirementStatus = EnvirementStatus.NeedConsumer},
                new Device() { Id = 3, Name = "Device 3", DeviceType = "Technical room", PeopleCount = 0, Description = "This is the test device 3", RecyclingFan = true, VentilationFan = false, IP = "192.168.0.3", MAC = "xxxx", Classroom = "52.102", TimeStamp = DateTime.Now,  OnlineStatus = false, Temperature = 0, Radiator = false, VentilationValveStatus = false, EnvirementStatus = EnvirementStatus.Idle},
                new Device() { Id = 4, Name = "Device 4", DeviceType = "Corridor area", PeopleCount = 0, Description = "This is the test device 4", RecyclingFan = true, VentilationFan = false, IP = "192.168.0.4", MAC = "xxxx", Classroom = "52.103", TimeStamp = DateTime.Now,  OnlineStatus = false, Temperature = 0, Radiator = false, VentilationValveStatus = false, EnvirementStatus = EnvirementStatus.Idle},
            };
        }
    }
}
