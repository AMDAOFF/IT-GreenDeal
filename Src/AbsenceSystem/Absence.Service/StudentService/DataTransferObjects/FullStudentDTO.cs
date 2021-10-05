using Absence.Service.AbsenceReportService;
using Absence.Service.StudentClassService;
using System.Collections.Generic;

namespace Absence.Service.StudentService
{
    public class FullStudentDTO
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public int FKStudentClassId { get; set; }
        public List<FullAbsenceReportDTO> AbsenceReports { get; set; }
        public FullStudentClassDTO StudentClass { get; set; }
    }
}
