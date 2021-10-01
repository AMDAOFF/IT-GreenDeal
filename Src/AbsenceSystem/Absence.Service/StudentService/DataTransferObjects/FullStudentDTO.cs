using Absence.Service.AbsenceReportService.DataTransferObjects;
using System.Collections.Generic;

namespace Absence.Service.StudentService.DataTransferObjects
{
    public class FullStudentDTO
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public List<FullAbsenceReportDTO> AbsenceReports { get; set; }
    }
}
