using Absence.Service.SchoolService;
using Absence.Service.StudentClassService;
using Absence.Service.SubjectService;
using System.Collections.Generic;

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
        public List<FullStudentClassDTO> StudentClasses { get; set; }
    }
}
