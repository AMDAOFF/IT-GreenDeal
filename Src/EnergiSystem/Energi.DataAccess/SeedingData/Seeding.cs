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
                new Device() { Id = Guid.NewGuid(), Name = "Device 1", Description = "This is the test device 1"},
                new Device() { Id = Guid.NewGuid(), Name = "Device 2", Description = "This is the test device 2"},
                new Device() { Id = Guid.NewGuid(), Name = "Device 3", Description = "This is the test device 3"},
                new Device() { Id = Guid.NewGuid(), Name = "Device 4", Description = "This is the test device 4"}
            };
        }
    }
}
