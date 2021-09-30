using System;
using System.Collections.Generic;

namespace Absence.DataAccess.Entities
{
    public class DaySchedule
    {
        public int DayScheduleId { get; set; }
        public int FKWeekScheduleId { get; set; }
        public DateTime Date { get; set; }
        public WeekSchedule WeekSchedule { get; set; }
        public List<HourSchedule> HourSchedules { get; set; }
    }
}
