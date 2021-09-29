using System.Threading.Tasks;

namespace Energi.Service.DatabaseService
{
    public interface IDatabaseService
    {
        Task DeleteDatabase();
        Task SeedData();
    }
}