using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.StudentService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;

namespace Absence.Service.StudentService
{
    public class StudentService : GenericService<FullStudentDTO, IStudentRepository, Student>, IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _studentRepository = GenericRepository;
        }
    }
}