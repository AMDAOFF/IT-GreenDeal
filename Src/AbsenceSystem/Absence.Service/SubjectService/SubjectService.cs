using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.SubjectService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;

namespace Absence.Service.SubjectService
{
    public class SubjectService : GenericService<FullSubjectDTO, ISubjectRepository, Subject>, ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService(ISubjectRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _subjectRepository = GenericRepository;
        }
    }
}