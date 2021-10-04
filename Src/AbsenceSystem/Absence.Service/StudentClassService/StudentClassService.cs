using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.AutoMappingService;
using Absence.Service.GenericService;
using Absence.Service.StudentClassService;
using System;
using System.Threading.Tasks;

namespace Absence.Service.StudentClassService
{
    public class StudentClassService : GenericService<FullStudentClassDTO, IStudentClassRepository, StudentClass>, IStudentClassService
    {
        private readonly IStudentClassRepository _studentClassRepository;
        private readonly MappingService _mappingService;
        public StudentClassService(IStudentClassRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _studentClassRepository = GenericRepository;
            _mappingService = mappingService;
        }

        public async Task<FullStudentClassDTO> GetById(int studentClassId)
        {
            if (studentClassId == 0)
            {
                LogWarning($"studentClassId was 0, so the operation was skipped");
                return null;
            }
            try
            {
                FullStudentClassDTO studentClass = _mappingService._mapper.Map<FullStudentClassDTO>(await _studentClassRepository.GetById(studentClassId));
                LogInformation($"Successfully fetched the StudentClass with the studentClassId: " + studentClassId);
                return studentClass;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the StudentClass with the studentClassId: " + studentClassId, e);
                return null;
            }
        }
    }
}
