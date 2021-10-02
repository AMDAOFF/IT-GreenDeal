using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.AbsenceReportService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;
using System.Threading.Tasks;
using System;

namespace Absence.Service.AbsenceReportService
{
    public class AbsenceReportService : GenericService<FullAbsenceReportDTO, IAbsenceReportRepository, AbsenceReport>, IAbsenceReportService
    {
        private readonly IAbsenceReportRepository _absenceReportRepository;
        private readonly MappingService _mappingService;
        public AbsenceReportService(IAbsenceReportRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _absenceReportRepository = GenericRepository;
            _mappingService = mappingService;
        }

        public async Task<FullAbsenceReportDTO> GetById(string studentId, int? hourScheduleId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
            {
                LogWarning($"Id was null, so the operation was skipped");
                return null;
            }
            try
            {
                FullAbsenceReportDTO absenceReport = _mappingService._mapper.Map<FullAbsenceReportDTO>(await _absenceReportRepository.GetById(studentId, hourScheduleId));
                LogInformation($"Successfully fetched the Item with the Id: " + studentId);
                return absenceReport;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the Item with the Id: " + studentId, e);
                return null;
            }
        }
    }
}