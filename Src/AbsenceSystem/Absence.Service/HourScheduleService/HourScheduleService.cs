using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.HourScheduleService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;

namespace Absence.Service.HourScheduleService
{
    public class HourScheduleService : GenericService<FullHourScheduleDTO, IHourScheduleRepository, HourSchedule>, IHourScheduleService
    {
        private readonly IHourScheduleRepository _hourScheduleRepository;
        public HourScheduleService(IHourScheduleRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _hourScheduleRepository = GenericRepository;
        }
    }
}