using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.SchoolService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;

namespace Absence.Service.SchoolService
{
    public class SchoolService : GenericService<FullSchoolDTO, ISchoolRepository, School>, ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        public SchoolService(ISchoolRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _schoolRepository = GenericRepository;
        }
    }
}