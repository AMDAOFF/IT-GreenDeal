using Absence.Service.HourScheduleService.DataTransferObjects;
using Absence.Service.StudentService.DataTransferObjects;

namespace Absence.Service.AbsenceReportService.DataTransferObjects
{
    public class FullAbsenceReportDTO
    {
        public int FKStudentId { get; set; }
        public int FKHourScheduleId { get; set; }
        public string Attended { get; set; }
        public FullStudentDTO Student { get; set; }
        public FullHourScheduleDTO HourSchedule { get; set; }
    }
}
