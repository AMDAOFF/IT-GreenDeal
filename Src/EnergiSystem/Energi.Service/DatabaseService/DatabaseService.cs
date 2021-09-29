using Energi.DataAccess.Entity;
using Energi.DataAccess.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi.Service.DatabaseService
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IContext<Device> _context;

        public DatabaseService(IContext<Device> context)
        {
            _context = context;
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

        public async Task DeleteDatabase()
        {
            try
            {
                _context.DropDatabase();
            }
            catch (Exception)
            {

                throw;
            }

            return;
        }
    }
}
