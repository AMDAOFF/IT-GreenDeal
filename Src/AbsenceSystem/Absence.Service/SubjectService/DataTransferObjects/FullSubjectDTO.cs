using Absence.Service.ScheduleService;
using Absence.Service.TeacherService;
using System.Collections.Generic;

namespace Absence.Service.SubjectService
{
    public class FullSubjectDTO
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public List<FullScheduleDTO> Schedules { get; set; }
        public List<FullTeacherDTO> Teachers { get; set; }
    }
}
