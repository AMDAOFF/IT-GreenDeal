using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.DayScheduleService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;

namespace Absence.Service.DayScheduleService
{
    public class DayScheduleService : GenericService<FullDayScheduleDTO, IDayScheduleRepository, DaySchedule>, IDayScheduleService
    {
        private readonly IDayScheduleRepository _dayScheduleRepository;
        public DayScheduleService(IDayScheduleRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _dayScheduleRepository = GenericRepository;
        }
    }
}