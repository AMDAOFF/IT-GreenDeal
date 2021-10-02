using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.HourScheduleService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;
using System.Threading.Tasks;
using System;

namespace Absence.Service.HourScheduleService
{
    public class HourScheduleService : GenericService<FullHourScheduleDTO, IHourScheduleRepository, HourSchedule>, IHourScheduleService
    {
        private readonly IHourScheduleRepository _hourScheduleRepository;
        private readonly MappingService _mappingService;
        public HourScheduleService(IHourScheduleRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _hourScheduleRepository = GenericRepository;
            _mappingService = mappingService;
        }

        public async Task<FullHourScheduleDTO> GetById(int hourScheduleId)
        {
            if (hourScheduleId == 0)
            {
                LogWarning($"hourScheduleId was 0, so the operation was skipped");
                return null;
            }
            try
            {
                FullHourScheduleDTO hourSchedule = _mappingService._mapper.Map<FullHourScheduleDTO>(await _hourScheduleRepository.GetById(hourScheduleId));
                LogInformation($"Successfully fetched the HourSchedule with the hourScheduleId: " + hourScheduleId);
                return hourSchedule;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the HourSchedule with the hourScheduleId: " + hourScheduleId, e);
                return null;
            }
        }
    }
}