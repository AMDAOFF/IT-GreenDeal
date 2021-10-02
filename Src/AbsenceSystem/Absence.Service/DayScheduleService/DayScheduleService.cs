using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.DayScheduleService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;
using System.Threading.Tasks;
using System;

namespace Absence.Service.DayScheduleService
{
    public class DayScheduleService : GenericService<FullDayScheduleDTO, IDayScheduleRepository, DaySchedule>, IDayScheduleService
    {
        private readonly IDayScheduleRepository _dayScheduleRepository;
        private readonly MappingService _mappingService;
        public DayScheduleService(IDayScheduleRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _dayScheduleRepository = GenericRepository;
            _mappingService = mappingService;
        }


        public async Task<FullDayScheduleDTO> GetById(int dayScheduleId)
        {
            if (dayScheduleId == 0)
            {
                LogWarning($"dayScheduleId was 0, so the operation was skipped");
                return null;
            }
            try
            {
                FullDayScheduleDTO daySchedule = _mappingService._mapper.Map<FullDayScheduleDTO>(await _dayScheduleRepository.GetById(dayScheduleId));
                LogInformation($"Successfully fetched the DaySchedule with the dayScheduleId: " + dayScheduleId);
                return daySchedule;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the DaySchedule with the dayScheduleId: " + dayScheduleId, e);
                return null;
            }
        }
    }
}