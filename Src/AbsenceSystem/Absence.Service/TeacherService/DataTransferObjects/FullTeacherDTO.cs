using Absence.Service.SchoolService;
using Absence.Service.SubjectService;

namespace Absence.Service.TeacherService
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
