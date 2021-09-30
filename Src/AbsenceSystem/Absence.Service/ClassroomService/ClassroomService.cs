using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.ClassroomService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;

namespace Absence.Service.ClassroomService
{
    public class ClassroomService : GenericService<FullClassroomDTO, IClassroomRepository, Classroom>, IClassroomService
    {
        private readonly IClassroomRepository _classroomRepository;
        public ClassroomService(IClassroomRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _classroomRepository = GenericRepository;
        }
    }
}