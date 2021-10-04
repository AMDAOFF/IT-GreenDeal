using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.SchoolService;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;
using System.Threading.Tasks;
using System;

namespace Absence.Service.SchoolService
{
    public class SchoolService : GenericService<FullSchoolDTO, ISchoolRepository, School>, ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly MappingService _mappingService;
        public SchoolService(ISchoolRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _schoolRepository = GenericRepository;
            _mappingService = mappingService;
        }

        public async Task<FullSchoolDTO> GetById(int schoolId)
        {
            if (schoolId == 0)
            {
                LogWarning($"schoolId was 0, so the operation was skipped");
                return null;
            }
            try
            {
                FullSchoolDTO school = _mappingService._mapper.Map<FullSchoolDTO>(await _schoolRepository.GetById(schoolId));
                LogInformation($"Successfully fetched the School with the schoolId: " + schoolId);
                return school;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the School with the schoolId: " + schoolId, e);
                return null;
            }
        }
    }
}