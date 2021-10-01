using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.WeekScheduleService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;

namespace Absence.Service.WeekScheduleService
{
    public class WeekScheduleService : GenericService<FullWeekScheduleDTO, IWeekScheduleRepository, WeekSchedule>, IWeekScheduleService
    {
        private readonly IWeekScheduleRepository _weekScheduleRepository;
        public WeekScheduleService(IWeekScheduleRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _weekScheduleRepository = GenericRepository;
        }
    }
}