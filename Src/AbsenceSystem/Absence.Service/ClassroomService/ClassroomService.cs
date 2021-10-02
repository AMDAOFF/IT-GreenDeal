using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.ClassroomService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;
using System.Threading.Tasks;
using System;

namespace Absence.Service.ClassroomService
{
    public class ClassroomService : GenericService<FullClassroomDTO, IClassroomRepository, Classroom>, IClassroomService
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly MappingService _mappingService;
        public ClassroomService(IClassroomRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _classroomRepository = GenericRepository;
            _mappingService = mappingService;
        }

        public async Task<FullClassroomDTO> GetById(int classroomId)
        {
            if (classroomId == 0)
            {
                LogWarning($"classroomId was 0, so the operation was skipped");
                return null;
            }
            try
            {
                FullClassroomDTO classroom = _mappingService._mapper.Map<FullClassroomDTO>(await _classroomRepository.GetById(classroomId));
                LogInformation($"Successfully fetched the Classroom with the classroomId: " + classroomId);
                return classroom;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the Classroom with the classroomId: " + classroomId, e);
                return null;
            }
        }
    }
}