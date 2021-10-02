using Absence.DataAccess.Entities;
using Absence.DataAccess.Interfaces;
using Absence.Service.SubjectService.DataTransferObjects;
using Absence.Service.GenericService;
using Absence.Service.AutoMappingService;
using System.Threading.Tasks;
using System;

namespace Absence.Service.SubjectService
{
    public class SubjectService : GenericService<FullSubjectDTO, ISubjectRepository, Subject>, ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly MappingService _mappingService;
        public SubjectService(ISubjectRepository GenericRepository, MappingService mappingService) : base(GenericRepository, mappingService)
        {
            _subjectRepository = GenericRepository;
            _mappingService = mappingService;
        }

        public async Task<FullSubjectDTO> GetById(int subjectId)
        {
            if (subjectId == 0)
            {
                LogWarning($"subjectId was 0, so the operation was skipped");
                return null;
            }
            try
            {
                FullSubjectDTO subject = _mappingService._mapper.Map<FullSubjectDTO>(await _subjectRepository.GetById(subjectId));
                LogInformation($"Successfully fetched the Subject with the subjectId: " + subjectId);
                return subject;
            }
            catch (Exception e)
            {
                LogError($"Failed to fetch the Subject with the subjectId: " + subjectId, e);
                return null;
            }
        }
    }
}