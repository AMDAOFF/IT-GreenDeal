using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.StudentService;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;
using System.Threading.Tasks;
using System;

namespace Absence.Service.StudentService
{
    public class StudentService : GenericService<FullStudentDTO, IStudentRepository, Student>, IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly MappingService _mappingService;
        public StudentService(IStudentRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _studentRepository = GenericRepository;
            _mappingService = mappingService;
        }

        public async Task<FullStudentDTO> GetById(string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
            {
                LogWarning($"studentId was null, so the operation was skipped");
                return null;
            }
            try
            {
                FullStudentDTO student = _mappingService._mapper.Map<FullStudentDTO>(await _studentRepository.GetById(studentId));
                LogInformation($"Successfully fetched the Student with the studentId: " + studentId);
                return student;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the Student with the studentId: " + studentId, e);
                return null;
            }
        }
    }
}