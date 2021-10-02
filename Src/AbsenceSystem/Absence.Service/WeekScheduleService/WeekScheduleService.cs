using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.WeekScheduleService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;
using System.Threading.Tasks;
using System;

namespace Absence.Service.WeekScheduleService
{
    public class WeekScheduleService : GenericService<FullWeekScheduleDTO, IWeekScheduleRepository, WeekSchedule>, IWeekScheduleService
    {
        private readonly IWeekScheduleRepository _weekScheduleRepository;
        private readonly MappingService _mappingService;
        public WeekScheduleService(IWeekScheduleRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _weekScheduleRepository = GenericRepository;
            _mappingService = mappingService;
        }

        public async Task<FullWeekScheduleDTO> GetById(int weekScheduleId)
        {
            if (weekScheduleId == 0)
            {
                LogWarning($"weekScheduleId was 0, so the operation was skipped");
                return null;
            }
            try
            {
                FullWeekScheduleDTO weekSchedule = _mappingService._mapper.Map<FullWeekScheduleDTO>(await _weekScheduleRepository.GetById(weekScheduleId));
                LogInformation($"Successfully fetched the WeekSchedule with the weekScheduleId: " + weekScheduleId);
                return weekSchedule;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the WeekSchedule with the weekScheduleId: " + weekScheduleId, e);
                return null;
            }
        }
    }
}