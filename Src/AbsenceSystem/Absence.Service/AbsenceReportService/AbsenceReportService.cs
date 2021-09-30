using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.AbsenceReportService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;

namespace Absence.Service.AbsenceReportService
{
    public class AbsenceReportService : GenericService<FullAbsenceReportDTO, IAbsenceReportRepository, AbsenceReport>, IAbsenceReportService
    {
        private readonly IAbsenceReportRepository _absenceReportRepository;
        public AbsenceReportService(IAbsenceReportRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _absenceReportRepository = GenericRepository;
        }
    }
}