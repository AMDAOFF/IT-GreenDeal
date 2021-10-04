using Absence.Service.AbsenceReportService;
using System.Collections.Generic;

namespace Absence.Service.StudentService
{
    public class FullStudentDTO
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public List<FullAbsenceReportDTO> AbsenceReports { get; set; }
    }
}
