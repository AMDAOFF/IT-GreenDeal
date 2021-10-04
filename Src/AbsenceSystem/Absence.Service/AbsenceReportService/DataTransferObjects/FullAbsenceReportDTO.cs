using Absence.Service.ScheduleService;
using Absence.Service.StudentService;

namespace Absence.Service.AbsenceReportService
{
    public class FullAbsenceReportDTO
    {
        public int FKStudentId { get; set; }
        public int FKScheduleId { get; set; }
        public string Attended { get; set; }
        public FullStudentDTO Student { get; set; }
        public FullScheduleDTO Schedule { get; set; }
    }
}
