using Absence.Service.SchoolService.DataTransferObjects;
using Absence.Service.SubjectService.DataTransferObjects;

namespace Absence.Service.TeacherService.DataTransferObjects
{
    public class FullTeacherDTO
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public int FKSchoolId { get; set; }
        public int FKSubjectId { get; set; }
        public FullSchoolDTO School { get; set; }
        public FullSubjectDTO Subject { get; set; }
    }
}
