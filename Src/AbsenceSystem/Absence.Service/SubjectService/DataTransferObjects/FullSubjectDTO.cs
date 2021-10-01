using Absence.Service.HourScheduleService.DataTransferObjects;
using Absence.Service.TeacherService.DataTransferObjects;
using System.Collections.Generic;

namespace Absence.Service.SubjectService.DataTransferObjects
{
    public class FullSubjectDTO
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public List<FullHourScheduleDTO> HourSchedules { get; set; }
        public List<FullTeacherDTO> Teachers { get; set; }
    }
}
