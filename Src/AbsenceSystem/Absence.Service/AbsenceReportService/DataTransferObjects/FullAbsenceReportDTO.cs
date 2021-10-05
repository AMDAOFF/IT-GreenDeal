using Absence.Service.ScheduleService;
using Absence.Service.StudentService;

namespace Absence.Service.AbsenceReportService
{
    public class FullAbsenceReportDTO
    {
        public string FKStudentId { get; set; }
        public int FKScheduleId { get; set; }
        public bool Attended { get; set; }
        public FullStudentDTO Student { get; set; }
        public FullScheduleDTO Schedule { get; set; }
    }
}
