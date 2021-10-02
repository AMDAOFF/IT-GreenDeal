using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.TeacherService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;
using System.Threading.Tasks;
using System;

namespace Absence.Service.TeacherService
{
    public class TeacherService : GenericService<FullTeacherDTO, ITeacherRepository, Teacher>, ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly MappingService _mappingService;
        public TeacherService(ITeacherRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _teacherRepository = GenericRepository;
            _mappingService = mappingService;
        }

        public async Task<FullTeacherDTO> GetById(int teacherId)
        {
            if (teacherId == 0)
            {
                LogWarning($"teacherId was 0, so the operation was skipped");
                return null;
            }
            try
            {
                FullTeacherDTO teacher = _mappingService._mapper.Map<FullTeacherDTO>(await _teacherRepository.GetById(teacherId));
                LogInformation($"Successfully fetched the Teacher with the teacherId: " + teacherId);
                return teacher;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the Teacher with the teacherId: " + teacherId, e);
                return null;
            }
        }
    }
}