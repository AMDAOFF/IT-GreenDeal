using Absence.Service.HourScheduleService.DataTransferObjects;
using Absence.Service.WeekScheduleService.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace Absence.Service.DayScheduleService.DataTransferObjects
{
    public class FullDayScheduleDTO
    {
        public int DayScheduleId { get; set; }
        public int FKWeekScheduleId { get; set; }
        public DateTime Date { get; set; }
        public FullWeekScheduleDTO WeekSchedule { get; set; }
        public List<FullHourScheduleDTO> HourSchedules { get; set; }
    }
}
