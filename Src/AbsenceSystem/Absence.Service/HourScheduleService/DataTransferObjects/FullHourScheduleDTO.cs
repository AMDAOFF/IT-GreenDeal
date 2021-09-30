using Absence.Service.AbsenceReportService.DataTransferObjects;
using Absence.Service.DayScheduleService.DataTransferObjects;
using Absence.Service.SubjectService.DataTransferObjects;
using System.Collections.Generic;

namespace Absence.Service.HourScheduleService.DataTransferObjects
{
    public class FullHourScheduleDTO
    {
        public int HourScheduleId { get; set; }
        public int FKSubjectId { get; set; }
        public int FKDayScheduleId { get; set; }
        public FullSubjectDTO Subject { get; set; }
        public FullDayScheduleDTO DaySchedule { get; set; }
        public List<FullAbsenceReportDTO> AbsenceReports { get; set; }
    }
}
