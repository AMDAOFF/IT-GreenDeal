using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.TeacherService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;

namespace Absence.Service.TeacherService
{
    public class TeacherService : GenericService<FullTeacherDTO, ITeacherRepository, Teacher>, ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        public TeacherService(ITeacherRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _teacherRepository = GenericRepository;
        }
    }
}