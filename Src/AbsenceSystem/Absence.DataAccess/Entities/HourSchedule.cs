using System.Collections.Generic;

namespace Absence.DataAccess.Entities
{
    public class HourSchedule
    {
        public int HourScheduleId { get; set; }
        public int FKSubjectId { get; set; }
        public int FKDayScheduleId { get; set; }
        public int FKStudentClassId { get; set; }
        public Subject Subject { get; set; }
        public DaySchedule DaySchedule { get; set; }
        public List<AbsenceReport> AbsenceReports { get; set; }
        public StudentClass StudentClass { get; set; }
    }
}
