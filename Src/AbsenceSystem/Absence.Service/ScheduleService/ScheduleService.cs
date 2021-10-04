using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.ScheduleService;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;
using System.Threading.Tasks;
using System;

namespace Absence.Service.ScheduleService
{
    public class ScheduleService : GenericService<FullScheduleDTO, IScheduleRepository, Schedule>, IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly MappingService _mappingService;
        public ScheduleService(IScheduleRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _scheduleRepository = GenericRepository;
            _mappingService = mappingService;
        }

        public async Task<FullScheduleDTO> GetById(int scheduleId)
        {
            if (scheduleId == 0)
            {
                LogWarning($"scheduleId was 0, so the operation was skipped");
                return null;
            }
            try
            {
                FullScheduleDTO schedule = _mappingService._mapper.Map<FullScheduleDTO>(await _scheduleRepository.GetById(scheduleId));
                LogInformation($"Successfully fetched the Schedule with the scheduleId: " + scheduleId);
                return schedule;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the Schedule with the scheduleId: " + scheduleId, e);
                return null;
            }
        }

        public async Task<FullScheduleDTO> GetSchedule(int classroomId, int subjectId, DateTime currentTime)
        {
            if (classroomId == 0 || subjectId == 0)
            {
                LogWarning($"One of the parameters was 0, so the operation was skipped");
                return null;
            }
            try
            {
                FullScheduleDTO schedule = _mappingService._mapper.Map<FullScheduleDTO>(await _scheduleRepository.GetSchedule(classroomId, subjectId, currentTime));
                LogInformation($"Successfully fetched the Schedule with the classroomId: {classroomId} and subjectId: {subjectId}");
                return schedule;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the Schedule with the classroomId: {classroomId} and subjectId: {subjectId}", e);
                return null;
            }
        }

        public async Task<FullScheduleDTO> GetSchedule(DateTime currentTime)
        {
            try
            {
                FullScheduleDTO schedule = _mappingService._mapper.Map<FullScheduleDTO>(await _scheduleRepository.GetSchedule(currentTime));
                LogInformation($"Successfully fetched the Schedule with the currentTime: {currentTime}");
                return schedule;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the Schedule with the currentTime: {currentTime}", e);
                return null;
            }
        }
    }
}